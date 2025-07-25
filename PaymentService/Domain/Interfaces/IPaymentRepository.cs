using PaymantService.Domain.Entities;

namespace PaymantService.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(string id);
    Task AddAsync(Payment payment);
}
