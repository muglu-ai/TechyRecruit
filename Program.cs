using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TechyRecruit.Models;
using TechyRecruit.Service;
using TechyRecruit.Data;
using Syncfusion.Licensing;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TechyRecruitContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TechyRecruitContext") ?? throw new InvalidOperationException("Connection string 'TechyRecruitContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TechyRecruitContext>();
builder.Services.Configure<SMTPConfigModel>(options => builder.Configuration.GetSection("SMTPConfig").Bind(options));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRecruitService, RecruitService>();
builder.Services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2V1hhQlJNfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5UdEdjW3tbc3VVQmRb");
// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

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
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Recruit}/{action=Index}/{id?}");

app.UseEndpoints(Endpoint =>
{
    Endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Recruit}/{action=Index}/{id?}");
    Endpoint.MapControllerRoute(
        name: "TestRoute",
        pattern: "{controller=Test}/{action=PdfTemplate}/{id?}");
});


app.Run();