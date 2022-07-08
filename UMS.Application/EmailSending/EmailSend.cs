using EmailServiceTools;
using UMS.Infrastructure.Abstraction.EmailSenderInterface;

namespace UMS.Application.EmailSending;

public class EmailSend
{
    private readonly IEmailSender _emailSender;
    public EmailSend(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public string SendEmail(EmailAddress receiver, string subject, string content)
    {
        var rng = new Random();
        var message = new Message(new EmailAddress [] { receiver }, subject, content);
        _emailSender.SendEmailAsync(message);
        return "Email Sent Successfully!!";
    }
}