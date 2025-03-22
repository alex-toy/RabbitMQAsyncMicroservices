using EmailNotificationWebHook.Consumers;
using EmailNotificationWebHook.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IEmailService, EmailService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WebHookConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq://localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
        config.ReceiveEndpoint("email-webhook");
    });
});




var app = builder.Build();

app.MapPost("/email-webhook", ([FromBody]EmailDto email, IEmailService emailService) => 
{
    string result = emailService.SendEmail(email);
    return Task.FromResult(result);
});

app.Run();;
