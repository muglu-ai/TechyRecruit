using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Syncfusion.Blazor;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TechyRecruitContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TechyRecruitContext") ?? throw new InvalidOperationException("Connection string 'TechyRecruitContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TechyRecruitContext>();

// Add services to the container.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
builder.Services.AddControllersWithViews();

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX5YcXZXR2BaWUV0Ww==");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Recruit}/{action=Index}/{id?}");

app.Run();