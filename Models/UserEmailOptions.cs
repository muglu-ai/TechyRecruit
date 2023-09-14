using TechyRecruit.Areas.Identity.Pages.Account;
namespace TechyRecruit.Models;

public class UserEmailOptions
{
    public List<string>? ToEmails { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<KeyValuePair<string, string>>? PlaceHolders { get; set; }
    public string TemplatePath { get; set; }
}