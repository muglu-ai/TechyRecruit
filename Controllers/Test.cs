using Microsoft.AspNetCore.Mvc;
using TechyRecruit.Models;
using TechyRecruit.Data;
using TechyRecruit.Helpers;


namespace TechyRecruit.Controllers;

public class Test : Controller
{
    private readonly TechyRecruitContext _context;

    public Test (TechyRecruitContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult PdfTemplate()
    {
        var recruits = new List<RecruitModel>
        {
            // new RecruitModel
            // {
            //     Recruiter = "John Doe",
            //     OpeningDetails = "Software Engineer",
            //     CandidateName = "Alice Smith",
            //     ReceivedDate = "2023-08-01", // Replace with an appropriate date format
            //     ContactNumber = "123-456-7890",
            //     Email = "alice@example.com",
            //     Company = "TechyCorp",
            //     TotalExperience = "5",
            //     RelevantExperience = "3",
            //     CCTC = "$100,000",
            //     ECTC = "$120,000",
            //     CurrentLocation = "New York",
            //     PreferredLocation = "San Francisco",
            //     NoticePeriodOrLastWorkingDay = "30 days",
            //     HoldingOfferOrPackageAmount = "Yes",
            //     ExperienceInCloudPlatforms = "AWS",
            //     ExperienceInLeadHandling = "Yes",
            //     ExperienceInPerformanceTesting = "No",
            //     ContractRoleRequired = "No"
            // }
            // Add more recruits with similar properties
        };

        var htmlContent = RazorViewToStringRenderer.RenderViewToStringAsync(this, "PdfTemplate", recruits).GetAwaiter()
            .GetResult();


        // ReSharper disable once SuspiciousTypeConversion.Global
        return View("PdfTemplate", recruits as IQueryable<RecruitModel>);
    }
}