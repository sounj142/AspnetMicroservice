using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

internal class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(EmailSettings settings, ILogger<EmailService> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<bool> SendEmail(Email email)
    {
        try
        {
            var emailClient = new SendGridClient(_settings.ApiKey);
            var message = new SendGridMessage
            {
                From = new EmailAddress(_settings.FromEmail, _settings.FromName),
                Subject = email.Subject,
                HtmlContent = email.Body
            };
            message.AddTo(email.To);
            message.SetClickTracking(false, false);

            var response = await emailClient.SendEmailAsync(message);
            _logger.LogInformation($"Send email to {email.To}, Success: {response.IsSuccessStatusCode}.");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error happend when sending email.");
            return false;
        }
    }
}