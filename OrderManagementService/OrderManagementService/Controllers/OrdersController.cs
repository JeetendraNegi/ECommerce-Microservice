using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementService.Models;
using OrderManagementService.Services;

namespace OrderManagementService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : Controller
{
    private readonly IDataAccessService<OrdersDetail> _dataAccessService;

    public OrdersController(IDataAccessService<OrdersDetail> dataAccessService)
    {
        _dataAccessService = dataAccessService;
    }


    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await _dataAccessService.GetAllData());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrdersById(string id)
    {
        return Ok(await _dataAccessService.GetDataById(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddOrders([FromBody]OrdersDetail ordersDetail)
    {
        if (ordersDetail == null)
            return BadRequest();

        ordersDetail.Id = Guid.NewGuid().ToString();
        var generate = new Random();
        ordersDetail.OrderNo = generate.Next(0,11232123).ToString("D6");
        await _dataAccessService.AddData(ordersDetail);
        return Ok(ordersDetail);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrders([FromBody]OrdersDetail ordersDetail, string id)
    {
        if(ordersDetail == null || _dataAccessService.GetDataById(id).Result == null)
            return NotFound();

        await _dataAccessService.UpdateData(ordersDetail, id);
        return Ok(ordersDetail);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrdersById(string id)
    {
        if (_dataAccessService.GetDataById(id).Result == null)
            return NotFound();

        await _dataAccessService.DeleteData(id);
        return Ok();
    }
}
