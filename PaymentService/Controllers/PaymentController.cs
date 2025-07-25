using Microsoft.AspNetCore.Mvc;
using PaymantService.Application.DTOs;
using PaymantService.Application.UseCases;

namespace PaymantService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentUseCase _paymentUseCase;

    public PaymentController(IPaymentUseCase paymentUseCase)
    {
        _paymentUseCase = paymentUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _paymentUseCase.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(string id)
    {
        var payment = await _paymentUseCase.GetPaymentByIdAsync(id);

        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
    {
        var payment = await _paymentUseCase.CreatePaymentAsync(dto);
        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
    }
}
