using PaymantService.Domain.Entities;

namespace PaymantService.Application.DTOs;

public class PaymentDto
{
    public string Id { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = null!;

    public PaymentDto(Payment payment)
    {
        Id = payment.Id;
        CustomerId = payment.CustomerId;
        Amount = payment.Amount;
        DueDate = payment.DueDate;
        Status = payment.Status;
    }
}
