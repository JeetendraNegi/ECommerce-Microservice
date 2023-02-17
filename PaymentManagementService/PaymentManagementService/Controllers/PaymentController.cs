using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentManagementService.Models;
using PaymentManagementService.Services;

namespace PaymentManagementService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : Controller
{
    private readonly IDataAccessService<PaymentDetails> _dataAccessService;

    public PaymentController(IDataAccessService<PaymentDetails> dataAccessService)
    {
        _dataAccessService = dataAccessService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPamentsDetail()
    {
        return Ok(await _dataAccessService.GetAllData());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPamentById(string id)
    {
        if (id == null)
            return BadRequest();

        return Ok(await _dataAccessService.GetDataById(id));
    }

    [HttpPost]
    public async Task<IActionResult> NewPaments([FromBody]PaymentDetails paymentDetails)
    {
        if(paymentDetails == null)
            return BadRequest(paymentDetails);

        paymentDetails.Id = Guid.NewGuid().ToString();
        await _dataAccessService.AddData(paymentDetails);
        return Ok(paymentDetails); 
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePaments([FromBody]PaymentDetails paymentDetails, string id)
    {
        if(paymentDetails == null || _dataAccessService.GetDataById(id).Result == null)
            return NotFound();

        await _dataAccessService.UpdateData(paymentDetails, id);
        return Ok(paymentDetails);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePaments(string id)
    {
        await _dataAccessService.DeleteData(id);
        return Ok();
    }
}
