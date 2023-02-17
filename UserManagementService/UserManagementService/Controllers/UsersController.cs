using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserManagementService.Models;
using UserManagementService.Services;

namespace UserManagementService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        private readonly IDataAccessService<UserDetails> _dataAccessService;

        public UsersController(IDataAccessService<UserDetails> dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _dataAccessService.GetAllData();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _dataAccessService.GetDataByID(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] UserDetails user)
        {
            user.Id = Guid.NewGuid().ToString();
            await _dataAccessService.AddData(user);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsers([FromBody] UserDetails user, string id)
        {
            if(_dataAccessService.GetDataByID(id).Result.FirstName != null)
            {
                await _dataAccessService.UpdateData(user, id);
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            if(_dataAccessService.GetDataByID(id).Result.FirstName == null)
            {
                return NotFound();
            }

            await _dataAccessService.DeleteData(id);
            return Ok();
        }

        //Loadblancer test
        [HttpPost("{test}")]
        public async Task<IActionResult> GetTestData(string test)
        {
            return Ok( new { Server = "from 5144", Data = test});
        }


        // This endpoint is used by background worker to update the Address
        [HttpPost]
        [Route("UpdateUser/{type}")]
        public async Task<IActionResult> ReceiveMessage(string type, [FromBody]string message)
        {
            if (!string.IsNullOrEmpty(message) && message.Length > 0)
            {
                switch (type.ToLower())
                {
                    case "address":
                        var addressDetail = JsonSerializer.Deserialize<AddressDetails>(message);
                        var user = await _dataAccessService.GetDataByID(addressDetail.UserID);
                        if (user != null)
                        {
                            if (user.UserAddress == null)
                                user.UserAddress = new List<AddressDetails>();

                            user.UserAddress.Add(addressDetail);
                            await _dataAccessService.UpdateData(user, user.Id);
                            return Ok(user);
                        }
                        break;
                    case "card":
                        var cardDetail = JsonSerializer.Deserialize<CardDetails>(message);
                        var users = await _dataAccessService.GetDataByID(cardDetail.UserID);
                        if (users != null)
                        {
                            if(users.CardDetail == null)
                                users.CardDetail = new List<CardDetails>();

                            users.CardDetail.Add(cardDetail);
                            await _dataAccessService.UpdateData(users, users.Id);
                            return Ok(users);
                        }
                        break;

                }
            }
            return BadRequest();
        }

    }
}
