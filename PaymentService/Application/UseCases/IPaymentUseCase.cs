using PaymantService.Application.DTOs;

namespace PaymantService.Application.UseCases;

public interface IPaymentUseCase
{
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto?> GetPaymentByIdAsync(string id);
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto dto);
}
