using Microsoft.AspNetCore.Mvc;
using OrderAPI.Repository;
using Shared.DTOs;
using Shared.Models;

namespace OrderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrder orderRepo) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ServiceResponseDto>> Create(Order order)
    {
        ServiceResponseDto response = await orderRepo.AddAsync(order);
        return response.Flag ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        return await orderRepo.GetAllAsync();
    }
}
