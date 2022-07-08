using EmailServiceTools;
using MimeKit;
using NETCore.MailKit;
namespace UMS.Infrastructure.Abstraction.EmailSenderInterface;

public interface IEmailSender
{
    Task SendEmailAsync(Message message);
}