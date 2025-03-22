using MassTransit;
using OrderAPI.Data;
using Shared.Models;

namespace OrderAPI.Consumers;

public class ProductConsumer(OrderDbContext orderContext) : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> context)
    {
        orderContext.Products.Add(context.Message);
        await orderContext.SaveChangesAsync();
    }
}
