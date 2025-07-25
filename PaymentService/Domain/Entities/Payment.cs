namespace PaymantService.Domain.Entities;

public class Payment
{
    public string Id { get; private set; }
    public string CustomerId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public string Status { get; private set; }

    public Payment(string customerId, decimal amount, DateTime dueDate)
    {
        Id = Guid.NewGuid().ToString();
        CustomerId = customerId;
        Amount = amount;
        DueDate = dueDate;
        Status = "pending";
    }

    public void MarkAsPaid()
    {
        Status = "paid";
    }

    public void MarkAsFailed()
    {
        Status = "failed";
    }
}
