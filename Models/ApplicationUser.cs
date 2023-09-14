using Microsoft.AspNetCore.Identity;

namespace TechyRecruit.Models;

public class ApplicationUser:IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
}