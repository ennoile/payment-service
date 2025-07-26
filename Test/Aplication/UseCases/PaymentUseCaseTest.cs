using Moq;
using PaymantService.Application.DTOs;
using PaymantService.Application.UseCases;
using PaymantService.Domain.Entities;
using PaymantService.Domain.Interfaces;

namespace Test.Aplication.UseCases;

public class PaymentUseCaseTest
{
    private Mock<IPaymentRepository> _repositoryMock = null!;
    private PaymentUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPaymentRepository>();
        _useCase = new PaymentUseCase(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllPaymentsAsync_ReturnsMappedDtos()
    {
        // Arrange
        var payments = new List<Payment>
            {
                new Payment("customer1", 100.0m, DateTime.Today),
                new Payment("customer2", 200.0m, DateTime.Today.AddDays(1))
            };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(payments);

        // Act
        var result = (await _useCase.GetAllPaymentsAsync()).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].CustomerId, Is.EqualTo("customer1"));
        Assert.That(result[0].Amount, Is.EqualTo(100.0m));
    }

    [Test]
    public async Task GetPaymentByIdAsync_WhenFound_ReturnsDto()
    {
        // Arrange
        var payment = new Payment("customer1", 100.0m, DateTime.Today);
        _repositoryMock.Setup(r => r.GetByIdAsync("id123")).ReturnsAsync(payment);

        // Act
        var result = await _useCase.GetPaymentByIdAsync("id123");

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result!.CustomerId, Is.EqualTo("customer1"));
    }

    [Test]
    public async Task GetPaymentByIdAsync_WhenNotFound_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync("invalid")).ReturnsAsync((Payment?)null);

        // Act
        var result = await _useCase.GetPaymentByIdAsync("invalid");

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task CreatePaymentAsync_AddsPaymentAndReturnsDto()
    {
        // Arrange
        var dto = new CreatePaymentDto
        {
            CustomerId = "customer1",
            Amount = 150.0m,
            DueDate = DateTime.Today.AddDays(5)
        };

        // Act
        var result = await _useCase.CreatePaymentAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Payment>()), Times.Once);
        Assert.That(result.CustomerId, Is.EqualTo("customer1"));
        Assert.That(result.Amount, Is.EqualTo(150.0m));
    }
}
