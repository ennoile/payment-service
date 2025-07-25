using MongoDB.Driver;
using PaymantService.Domain.Entities;
using PaymantService.Domain.Interfaces;

namespace PaymantService.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly IMongoCollection<Payment> _collection;

    public PaymentRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Payment>("Payments");
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        var result = await _collection.FindAsync(_ => true);
        return result.ToList();
    }

    public async Task<Payment?> GetByIdAsync(string id)
    {
        var result = await _collection.FindAsync(p => p.Id == id);
        return await result.FirstOrDefaultAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _collection.InsertOneAsync(payment);
    }
}
