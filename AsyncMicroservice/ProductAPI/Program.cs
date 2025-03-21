using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("default")!;
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IProduct, ProductRepo>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq://localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
    });
});

builder.Services.AddScoped<IProduct, ProductRepo>();

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
