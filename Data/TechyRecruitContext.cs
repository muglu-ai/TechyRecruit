using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechyRecruit.Models;

namespace TechyRecruit.Data;

public class TechyRecruitContext : IdentityDbContext
{
    public TechyRecruitContext (DbContextOptions<TechyRecruitContext> options)
        : base(options)
    {
    }

    public DbSet<TechyRecruit.Models.RecruitModel> RecruitModel { get; set; } = default!;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}