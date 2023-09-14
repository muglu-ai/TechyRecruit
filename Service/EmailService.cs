using System.Net;
using System.Net.Mail;
using System.Text;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechyRecruit.Models;
using TechyRecruit.Areas.Identity.Pages.Account;



namespace TechyRecruit.Service;

public interface IEmailService
{
    Task SendEmailAsync(UserEmailOptions emailOptions);
    Task SendEmailAsync(string to, string subject, string body);
    string ExtractSubjectFromHtml(string htmlTemplate);
}

public class EmailService : IEmailService
{
    private readonly SMTPConfigModel _emailSettings;
    private const string TemplatePath = @"EmailTemplate/{0}.html";


    public EmailService(IOptions<SMTPConfigModel> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    

    public async Task SendEmailAsync(UserEmailOptions emailOptions)
    {
        string htmlTemplate = File.ReadAllText(emailOptions.TemplatePath);

        foreach (var placeholder in emailOptions.PlaceHolders)
        {
            htmlTemplate = htmlTemplate.Replace(placeholder.Key, placeholder.Value);
        }

        using (SmtpClient client = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
            client.EnableSsl = true;

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_emailSettings.SenderAddress);
                foreach (var toEmail in emailOptions.ToEmails)
                {
                    mailMessage.To.Add(toEmail);
                }
                mailMessage.Subject = emailOptions.Subject;
                mailMessage.Body = htmlTemplate;
                mailMessage.IsBodyHtml = true;

                await client.SendMailAsync(mailMessage);
            }
        }
    }

    public string ExtractSubjectFromHtml(string htmlTemplate)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlTemplate);

        // Implement logic to extract the subject from the HTML (e.g., using XPath or other methods)
        // Example: Extract subject from the first <h1> tag in the HTML
        var subjectNode = doc.DocumentNode.SelectSingleNode("//h1");
        string subject = subjectNode?.InnerText ?? "Default Subject";

        return subject;
    }

    public Task SendEmailAsync(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }
}




