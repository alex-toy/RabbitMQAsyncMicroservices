using Microsoft.AspNetCore.Mvc;
using ProductAPI.Repository;
using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProduct productRepo) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ServiceResponseDto>> Create(Product product)
    {
        ServiceResponseDto response = await productRepo.AddAsync(product);
        return response.Flag ?  Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return await productRepo.GetAllAsync();
    }
}
