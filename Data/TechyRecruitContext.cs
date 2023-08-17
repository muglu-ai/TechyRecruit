using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechyRecruit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class TechyRecruitContext : IdentityDbContext
    {
        public TechyRecruitContext (DbContextOptions<TechyRecruitContext> options)
            : base(options)
        {
        }

        public DbSet<TechyRecruit.Models.RecruitModel> RecruitModel { get; set; } = default!;
    }
