namespace Shared.DTOs;

public record OrderSummaryDto(int Id, int ProductId, string ProductName, decimal ProductPrice, int Quantity, decimal TotalAmount, DateTime OrderedAt)
{
}
