using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace TechyRecruit.Models;

public class RecruitModel
{
    public int Id { get; set; }
    [Display(Name = "Recruiter")]
    public string Recruiter { get; set; }

    [Display(Name = "Opening Details")]
    public string OpeningDetails { get; set; }

    [Display(Name = "Candidate Name")]
    public string CandidateName { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = true)]
    public string? ReceivedDate { get; set; }

    [Display(Name = "Contact No.")]
    public string ContactNumber { get; set; }

    [Display(Name = "Email ID")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Company")]
    public string Company { get; set; }

    [Display(Name = "Total Exp")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Total Experience must be a valid number.")]
    public string TotalExperience { get; set; }

    [Display(Name = "Relevant Exp?")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Relevant Experience must be a valid number.")]
    public string RelevantExperience { get; set; }

    [Display(Name = "CCTC")]
    [DisplayFormat(DataFormatString = "{0:N} lac")]
    public decimal CCTC { get; set; }

    [Display(Name = "ECTC")]
    [DisplayFormat(DataFormatString = "{0:N} lac")]
    public decimal ECTC { get; set; }

    [Display(Name = "Current Location")]
    public string CurrentLocation { get; set; }

    [Display(Name = "Preferred Location")]
    public string PreferredLocation { get; set; }

    [Display(Name = "Notice Period")]
    public string NoticePeriodOrLastWorkingDay { get; set; }

    [Display(Name = "Holding Offer/Package Amount?")]
    public string HoldingOfferOrPackageAmount { get; set; }

    [Display(Name = "Exp in AWS, Azure, GCP??")]
    public string ExperienceInCloudPlatforms { get; set; }

    [Display(Name = "Lead Handling Exp?")]
    public string ExperienceInLeadHandling { get; set; }

    [Display(Name = "Exp in Performance")]
    public string ExperienceInPerformanceTesting { get; set; }

    [Display(Name = "Contract Role?")]
    public string ContractRoleRequired { get; set; }
    
    [NotMapped] // This attribute tells EF Core not to map this property to the database
    public DateTime ReceivedDatestr{
        get
        {   
            if (!string.IsNullOrEmpty(ReceivedDate) &&
                DateTime.TryParseExact(ReceivedDate, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            return DateTime.MinValue;
        }
        set
        {
            ReceivedDate = value.ToString("dd-MMM-yy");
        }

}
}