using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementService.Models;
using ProductManagementService.Services;

namespace ProductManagementService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : Controller
{
    private readonly IProductDataAccessService<ProductDetail> _dataAccessService;

    public ProductsController(IProductDataAccessService<ProductDetail> dataAccessService)
    {
        _dataAccessService = dataAccessService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _dataAccessService.GetAllData());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByID(string id)
    {
        return Ok(await _dataAccessService.GetDataByID(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody]ProductDetail productDetail)
    {
        if (productDetail == null)
            return BadRequest();

        productDetail.Id = Guid.NewGuid().ToString();
        await _dataAccessService.AddData(productDetail);
        return Ok(productDetail);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody]ProductDetail productDetail,string id)
    {
        if(productDetail == null || _dataAccessService.GetDataByID(id).Result == null)
            return NotFound(id);

        await _dataAccessService.UpdateData(productDetail,id);
        return Ok(productDetail);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        if(_dataAccessService.GetDataByID(id).Result == null)
            return NotFound(id);

        await _dataAccessService.DeleteData(id);
        return Ok();
    }
}
