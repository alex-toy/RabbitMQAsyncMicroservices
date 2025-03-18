using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Repository;

public interface IProduct
{
    Task<ServiceResponseDto> AddAsync(Product product);
    Task<List<Product>> GetAllAsync();
}
