using PaymantService.Application.DTOs;
using PaymantService.Domain.Entities;
using PaymantService.Domain.Interfaces;

namespace PaymantService.Application.UseCases;

public class PaymentUseCase : IPaymentUseCase
{
    private readonly IPaymentRepository _repository;

    public PaymentUseCase(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _repository.GetAllAsync();
        return payments.Select(p => new PaymentDto(p));
    }

    public async Task<PaymentDto?> GetPaymentByIdAsync(string id)
    {
        var payment = await _repository.GetByIdAsync(id);
        return payment == null ? null : new PaymentDto(payment);
    }

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Payment(dto.CustomerId, dto.Amount, dto.DueDate);
        await _repository.AddAsync(payment);
        return new PaymentDto(payment);
    }
}
