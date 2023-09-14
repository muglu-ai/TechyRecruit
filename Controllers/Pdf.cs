using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using TechyRecruit.Data;
using TechyRecruit.Models;
using TechyRecruit.Service;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace TechyRecruit.Controllers;

public class Pdf : Controller

{
    private readonly TechyRecruitContext _context;
    private readonly IRecruitService _recruitService;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public Pdf(TechyRecruitContext context,
        IRecruitService recruitService,
        IWebHostEnvironment webHostEnvironment
        
    )
    {
        _context = context;
        _recruitService = recruitService;
        _webHostEnvironment = webHostEnvironment;

    }
    [HttpGet]
    public IActionResult DownloadFilteredRecruitsPdf(string search)
    {
        var recruits = GetFilteredRecruits(search);
        var pdfStream = GeneratePdf(recruits);
        var dateTimeNow = DateTime.Now;
        var pdfName = $"FilteredRecruits_{dateTimeNow:yyyyMMdd_HHmmss}.pdf";
        return File(pdfStream, "application/pdf", pdfName);
    }

    private IQueryable<RecruitModel> GetFilteredRecruits(string searchString)
    {
        var recruits = _context.RecruitModel.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            recruits = recruits.Where(r => r.CurrentLocation.ToLower().Contains(searchString));
        }

        return recruits;
    }

    private MemoryStream GeneratePdf(IQueryable<RecruitModel> recruits)
    {
        var pdfDocument = new PdfDocument();
        var page = pdfDocument.Pages.Add();
        var graphics = page.Graphics;
        var font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
        float xPosition = 10;
        float yPosition = 10;
        float lineHeight = 20;
        string basePath = _webHostEnvironment.WebRootPath;
        string dataPath = string.Empty;
        dataPath = basePath + @"/images/techyrecruit.jpg";
        FileStream file = new FileStream(dataPath, FileMode.Open, FileAccess.Read);

        PdfImage img = new PdfBitmap(file);
            // Draw the image at the top of the page
            graphics.DrawImage(img, new RectangleF(0, 0, 151, 54));
            yPosition += 50;
            
        
        foreach (var recruit in recruits)
        {
            
            graphics.DrawString($"Recruiter: {recruit.Recruiter}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Opening Details: {recruit.OpeningDetails}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Candidate Name: {recruit.CandidateName}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Received Date: {recruit.ReceivedDate}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Contact Number: {recruit.ContactNumber}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Email: {recruit.Email}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Company: {recruit.Company}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Total Experience: {recruit.TotalExperience}", font, PdfBrushes.Black,new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Relevant Experience: {recruit.RelevantExperience}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"CCTC: {recruit.CCTC}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"ECTC: {recruit.ECTC}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Current Location: {recruit.CurrentLocation}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Preferred Location: {recruit.PreferredLocation}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Notice Period/Last Working Day: {recruit.NoticePeriodOrLastWorkingDay}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Holding Offer/Package Amount: {recruit.HoldingOfferOrPackageAmount}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Experience in Cloud Platforms: {recruit.ExperienceInCloudPlatforms}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Experience in Lead Handling: {recruit.ExperienceInLeadHandling}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Experience in Performance Testing: {recruit.ExperienceInPerformanceTesting}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            graphics.DrawString($"Contract Role Required: {recruit.ContractRoleRequired}", font, PdfBrushes.Black, new PointF(xPosition, yPosition));
            yPosition += lineHeight;
            
            yPosition+= lineHeight;
        }

        var pdfStream = new MemoryStream();
        pdfDocument.Save(pdfStream);
        pdfStream.Position = 0;

        return pdfStream;
        }
    
    }

