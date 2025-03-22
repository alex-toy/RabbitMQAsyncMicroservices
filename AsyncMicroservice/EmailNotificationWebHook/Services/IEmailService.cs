using Shared.DTOs;

namespace EmailNotificationWebHook.Services;

public interface IEmailService
{
    string SendEmail(EmailDto email);
}
