using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementService.Models;
using ProductManagementService.Services;

namespace ProductManagementService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class SallersController : Controller
{
    private readonly IDataAccessService<SallersDetail> _dataAccessService;
    private readonly IProductDataAccessService<ProductDetail> _productService;

    public SallersController(IDataAccessService<SallersDetail> dataAccessService,
        IProductDataAccessService<ProductDetail> productService)
    {
        _dataAccessService = dataAccessService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSallers()
    {
        var sallers = await _dataAccessService.GetAllData();
        if(sallers.Count > 0)
        {
            foreach(var saller in sallers)
            {
                saller.Products = await _productService.GetProductBySallerID(saller.Id);
            }
        }
        return Ok(sallers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSallerByID(string id)
    {
        var saller = await _dataAccessService.GetDataByID(id);
        saller.Products = await _productService.GetProductBySallerID(id);
        return Ok(saller);
    }

    [HttpPost]
    public async Task<IActionResult> AddSaller([FromBody]SallersDetail detail)
    {
        if(detail == null)
            return BadRequest();

        detail.Id = Guid.NewGuid().ToString();
        await _dataAccessService.AddData(detail);
        return Ok(detail);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSaller([FromBody]SallersDetail detail, string id)
    {
        if(detail == null || _dataAccessService.GetDataByID(id).Result == null)
            return NotFound();

        await _dataAccessService.UpdateData(detail, id);
        return Ok(detail);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSaller(string id)
    {
        if (_dataAccessService.GetDataByID(id).Result == null)
            return NotFound();

        await _dataAccessService.DeleteData(id);
        return Ok();
    }
}
