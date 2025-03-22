using MassTransit;
using OrderAPI.Data;
using Shared.Models;

namespace OrderAPI.Consumers;

public class ProductConsumer(OrderDbContext orderContext) : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> context)
    {
        orderContext.Products.Add(new Product { Name = context.Message.Name, Price = context.Message.Price});
        await orderContext.SaveChangesAsync();
    }
}
