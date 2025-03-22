using MassTransit;
using Shared.DTOs;

namespace EmailNotificationWebHook.Consumers;

public class WebHookConsumer(HttpClient httpClient) : IConsumer<EmailDto>
{
    public async Task Consume(ConsumeContext<EmailDto> context)
    {
        var email = new EmailDto(context.Message.Title, context.Message.Content);
        HttpResponseMessage result = await httpClient.PostAsJsonAsync("https://localhost:7099/email-webhook", email);
        
    }
}
