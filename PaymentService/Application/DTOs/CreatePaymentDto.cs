namespace PaymantService.Application.DTOs;

public class CreatePaymentDto
{
    public string CustomerId { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}
