using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using Shared.DTOs;
using Shared.Models;
using System.Text;

namespace OrderAPI.Repository;

public class OrderRepo(OrderDbContext context, IPublishEndpoint publishEndPoint) : IOrder
{
    public async Task<ServiceResponseDto> AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        OrderSummaryDto orderSummary = await GetOrderSummaryAsync();
        string content = BuildOrderEmailBody(orderSummary);
        await publishEndPoint.Publish(new EmailDto("Order Information", content));
        return new ServiceResponseDto(true, "Order added successfully");
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<OrderSummaryDto> GetOrderSummaryAsync()
    {
        Order? order = await context.Orders.FirstOrDefaultAsync();
        List<Product> products = await context.Products.ToListAsync();
        Product? productInfo = products.Find(x => x.Id != order!.ProductId);
        return new OrderSummaryDto(order!.Id, productInfo!.Id, productInfo!.Name!, productInfo.Price, order!.Quantity, order!.Quantity * productInfo.Price, order!.OrderedAt);
    }

    private string BuildOrderEmailBody(OrderSummaryDto orderSummary)
    {
        StringBuilder sb = new ();
        sb.AppendLine("<h1><strong>Order Information</strong></h1>");
        sb.AppendLine($"<p><strong>Order Id</strong> : {orderSummary.Id}</p>");
        sb.AppendLine($"<ul>");
        sb.AppendLine($"<li>Name : {orderSummary.ProductName}</li>");
        sb.AppendLine($"<li>Price : {orderSummary.ProductPrice}</li>");
        sb.AppendLine($"<li>Quantity : {orderSummary.Quantity}</li>");
        sb.AppendLine($"<li>Total Amount : {orderSummary.TotalAmount}</li>");
        sb.AppendLine($"<li>Order date : {orderSummary.OrderedAt}</li>");
        sb.AppendLine($"</ul>");
        sb.AppendLine($"<p>Thank you</p>");
        return sb.ToString();
    }

    private async Task ClearOrderTable()
    {

    }
}
