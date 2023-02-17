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
    public class AddressController : Controller
    {
        private readonly IDataAccessService<AddressDetails> _dataAccessService;
        private readonly IMessageSender<AddressDetails> _messageSender;
        private readonly IConfiguration _configuration;
        private readonly string queueName;
        public AddressController(IDataAccessService<AddressDetails> dataAccessService,
            IMessageSender<AddressDetails> messageSender,
            IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _messageSender = messageSender;
            _configuration = configuration;

            queueName = _configuration.GetValue<string>("RabbitMqQueueName:AddressQueue");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddress()
        {
            return Ok(await _dataAccessService.GetAllData());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressByID(string id)
        {
            return Ok(await _dataAccessService.GetDataByID(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAddress([FromBody]AddressDetails addressDetails)
        {            
            if (addressDetails == null)
                return BadRequest();

            addressDetails.Id = Guid.NewGuid().ToString();
            // Send address to add in the User_Detail Collection (RabbitMQ)
            var sendMessage = await _messageSender.SendMessage(addressDetails, queueName);


            if (sendMessage)
            {
                await _dataAccessService.AddData(addressDetails);
                return Ok(addressDetails);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody]AddressDetails addressDetails, string id)
        {
            if(addressDetails == null || _dataAccessService.GetDataByID(id).Result == null)
                return NotFound();
            await _dataAccessService.UpdateData(addressDetails, id);
            return Ok(addressDetails);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(string id)
        {
            if (_dataAccessService.GetDataByID(id).Result == null)
                return NotFound();

            await _dataAccessService.DeleteData(id);
            return Ok();
        }
    }
}
