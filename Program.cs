using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using ProvaPub.Application.Interfaces;
using ProvaPub.Application.Services;
using ProvaPub.Domain.Services;
using ProvaPub.Domain.Interfaces;
using ProvaPub.Infrastructure.Payments.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure Services
builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();

// Domain Services
builder.Services.AddScoped<CustomerDomainService>();

// Application Services
builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlite("Data Source=teste.db"));
var app = builder.Build();

// Ensure database is created and up to date
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();
    context.Database.Migrate();
}

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
