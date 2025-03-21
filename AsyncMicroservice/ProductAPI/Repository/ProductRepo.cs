using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Repository;

public class ProductRepo(ProductDbContext context, IPublishEndpoint publishEndpoint) : IProduct
{
    public async Task<ServiceResponseDto> AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        await publishEndpoint.Publish(product);

        return new ServiceResponseDto(true, "Product added successfully");
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }
}
