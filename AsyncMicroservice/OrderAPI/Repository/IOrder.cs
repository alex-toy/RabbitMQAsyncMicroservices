using Shared.DTOs;
using Shared.Models;

namespace OrderAPI.Repository;

public interface IOrder
{
    Task<ServiceResponseDto> AddAsync(Order order);
    Task<List<Order>> GetAllAsync();
    Task<OrderSummaryDto> GetOrderSummaryAsync();
}
