using MessageBrokerService.AsyncMessaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Models;
using UserManagementService.Services;

namespace UserManagementService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CardController : Controller
    {
        private readonly IDataAccessService<CardDetails> _dataAccessService;
        private readonly IMessageSender<CardDetails> _messageSender;
        private readonly IConfiguration _configuration;
        private readonly string queueName;

        public CardController(IDataAccessService<CardDetails> dataAccessService,
            IMessageSender<CardDetails> messageSender,
            IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _messageSender = messageSender;
            _configuration = configuration;

            queueName = _configuration.GetValue<string>("RabbitMqQueueName:CardQueue");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            return Ok(await _dataAccessService.GetAllData());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardByID(string id)
        {
            return Ok(await _dataAccessService.GetDataByID(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody]CardDetails card)
        {
            if (card == null)
                return BadRequest();

            card.Id = Guid.NewGuid().ToString();

            //Send the card data to RabbitMQ queue to update the User Detail
            var response = await _messageSender.SendMessage(card, queueName);

            if (response)
            {
                await _dataAccessService.AddData(card);
                return Ok(card);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCard([FromBody]CardDetails card, string id)
        {
            if(card == null || _dataAccessService.GetDataByID(id).Result == null)
                return NotFound();

            await _dataAccessService.UpdateData(card,id);
            return Ok(card);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCard(string id)
        {
            if (_dataAccessService.GetDataByID(id).Result == null)
                return NotFound();

            await _dataAccessService.DeleteData(id);
            return Ok();
        }
    }
}
