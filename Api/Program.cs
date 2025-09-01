using Infraestructure.Data;
using Infraestructure.Repositories.NoTransactional;
using Infraestructure.UnitOfWork.NoTransactional;
using Core.Service.NoTransactional;
using Domain.Interfaces.Repositories.NoTransactional;
using Domain.Interfaces.UnitOfWork.NoTransactional;
using Domain.Interfaces.Services.NoTransactional;

using Infraestructure.Repositories.Transactional;
using Infraestructure.UnitOfWork.Transactional;
using Core.Service.Transactional;
using Domain.Interfaces.Repositories.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;
using Domain.Interfaces.Services.Transactional;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración del DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para Repositories (NoTransactional)
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Inyección de dependencias para UnitOfWork (NoTransactional)
builder.Services.AddScoped<IClientUnitOfWork, ClientUnitOfWork>();
builder.Services.AddScoped<IProductUnitOfWork, ProductUnitOfWork>();

// Inyección de dependencias para Services (NoTransactional)
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Inyección de dependencias para Repositories (Transactional)
builder.Services.AddScoped<IClientRepositoryTransactional, ClientRepositoryTransactional>();
builder.Services.AddScoped<IProductRepositoryTransactional, ProductRepositoryTransactional>();
builder.Services.AddScoped<IOrderRepositoryTransactional, OrderRepositoryTransactional>();

// Inyección de dependencias para UnitOfWork (Transactional)
builder.Services.AddScoped<IClientUnitOfWorkTransactional, ClientUnitOfWorkTransactional>();
builder.Services.AddScoped<IProductUnitOfWorkTransactional, ProductUnitOfWorkTransactional>();
builder.Services.AddScoped<IOrderUnitOfWorkTransactional, OrderUnitOfWorkTransactional>();

// Inyección de dependencias para Services (Transactional)
builder.Services.AddScoped<IClientServiceTransactional, ClientServiceTransactional>();
builder.Services.AddScoped<IProductServiceTransactional, ProductServiceTransactional>();
builder.Services.AddScoped<IOrderServiceTransactional, OrderServiceTransactional>();

builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI
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