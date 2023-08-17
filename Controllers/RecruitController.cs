using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechyRecruit.Models;
using OfficeOpenXml;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;


namespace TechyRecruit.Controllers
{
    [Authorize]
    public class RecruitController : Controller
    {
        private readonly TechyRecruitContext _context;

        public RecruitController(TechyRecruitContext context)
        {
            _context = context;
        }

        // GET: Recruit
        public async Task<IActionResult> Index()
        {
              return _context.RecruitModel != null ? 
                          View(await _context.RecruitModel.ToListAsync()) :
                          Problem("Entity set 'TechyRecruitContext.RecruitModel'  is null.");
        }

        // GET: Recruit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RecruitModel == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitModel == null)
            {
                return NotFound();
            }

            return View(recruitModel);
        }

        // GET: Recruit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recruit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Recruiter,OpeningDetails,CandidateName,ReceivedDate,ContactNumber,Email,Company,TotalExperience,RelevantExperience,CCTC,ECTC,CurrentLocation,PreferredLocation,NoticePeriodOrLastWorkingDay,HoldingOfferOrPackageAmount,ExperienceInCloudPlatforms,ExperienceInLeadHandling,ExperienceInPerformanceTesting,ContractRoleRequired")] RecruitModel recruitModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recruitModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recruitModel);
        }

        // GET: Recruit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RecruitModel == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel.FindAsync(id);
            if (recruitModel == null)
            {
                return NotFound();
            }
            return View(recruitModel);
        }

        // POST: Recruit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recruiter,OpeningDetails,CandidateName,ReceivedDate,ContactNumber,Email,Company,TotalExperience,RelevantExperience,CCTC,ECTC,CurrentLocation,PreferredLocation,NoticePeriodOrLastWorkingDay,HoldingOfferOrPackageAmount,ExperienceInCloudPlatforms,ExperienceInLeadHandling,ExperienceInPerformanceTesting,ContractRoleRequired")] RecruitModel recruitModel)
        {
            if (id != recruitModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recruitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecruitModelExists(recruitModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recruitModel);
        }

        // GET: Recruit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RecruitModel == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitModel == null)
            {
                return NotFound();
            }

            return View(recruitModel);
        }

        // POST: Recruit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecruitModel == null)
            {
                return Problem("Entity set 'TechyRecruitContext.RecruitModel'  is null.");
            }
            var recruitModel = await _context.RecruitModel.FindAsync(id);
            if (recruitModel != null)
            {
                _context.RecruitModel.Remove(recruitModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitModelExists(int id)
        {
          return (_context.RecruitModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
public async Task<IActionResult> UploadExcel(IFormFile excelFile)
{
    if (excelFile == null || excelFile.Length <= 0)
    {
        ModelState.AddModelError("excelFile", "Please select a valid Excel file.");
        return RedirectToAction("Index");
    }

    using (var package = new ExcelPackage(excelFile.OpenReadStream()))
    {
        var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

        for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming header row is in the first row
        {
            RecruitModel recruit = new RecruitModel();
            recruit.Recruiter = worksheet.Cells[row, 1].Value?.ToString();
            recruit.OpeningDetails = worksheet.Cells[row, 2].Value?.ToString();
            recruit.CandidateName = worksheet.Cells[row, 3].Value?.ToString();
            recruit.ReceivedDate = worksheet.Cells[row, 4].Value?.ToString();
            recruit.ContactNumber = worksheet.Cells[row, 5].Value?.ToString();
            recruit.Email = worksheet.Cells[row, 6].Value?.ToString();
            recruit.Company = worksheet.Cells[row, 7].Value?.ToString();
            recruit.TotalExperience = worksheet.Cells[row, 8].Value?.ToString();
            recruit.RelevantExperience = worksheet.Cells[row, 9].Value?.ToString();
            recruit.CCTC = decimal.TryParse(worksheet.Cells[row, 10].Value?.ToString(), out decimal cctcValue)
                ? cctcValue
                : 0;
            recruit.ECTC = decimal.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out decimal ectcValue)
                ? ectcValue
                : 0;
            recruit.CurrentLocation = worksheet.Cells[row, 12].Value?.ToString();
            recruit.PreferredLocation = worksheet.Cells[row, 13].Value?.ToString();
            recruit.NoticePeriodOrLastWorkingDay = worksheet.Cells[row, 14].Value?.ToString();
            recruit.HoldingOfferOrPackageAmount = worksheet.Cells[row, 15].Value?.ToString();
            recruit.ExperienceInCloudPlatforms = worksheet.Cells[row, 16].Value?.ToString();
            recruit.ExperienceInLeadHandling = worksheet.Cells[row, 17].Value?.ToString();
            recruit.ExperienceInPerformanceTesting = worksheet.Cells[row, 18].Value?.ToString();
            recruit.ContractRoleRequired = worksheet.Cells[row, 19].Value?.ToString();

            _context.RecruitModel.Add(recruit);
            // if (decimal.TryParse(worksheet.Cells[row, 10].Text, out decimal cctc))
            // {
            //     recruit.CCTC = cctc;
            // }
            // else
            // {
            //     ModelState.AddModelError("excelFile", $"Invalid CCTC value in row {row}");
            //     return RedirectToAction("Index");
            // }
        }
        

        await _context.SaveChangesAsync();
    }
    

    return RedirectToAction("Index");
}

        }
    
}
