using MongoDB.Driver;
using PaymantService.Application.UseCases;
using PaymantService.Domain.Interfaces;
using PaymantService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoConnectionString = builder.Configuration["MongoDB:ConnectionString"];
    return new MongoClient(mongoConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);
});

// Injetar repositório e use case
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentUseCase, PaymentUseCase>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
