using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechyRecruit.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;
using TechyRecruit.Data;
using TechyRecruit.Service;



namespace TechyRecruit.Controllers
{
    [Authorize]
    public class RecruitController : Controller
    {
        private readonly TechyRecruitContext _context;
        private readonly IEmailService _emailService;
        private readonly IRecruitService _recruitService;
        
        public RecruitController(TechyRecruitContext context,
            IEmailService emailService,
            IRecruitService recruitService
        )
        {
            _context = context;
            _emailService = emailService;
            _recruitService = recruitService;
        }
        

        // GET: Recruit
        public IActionResult Index()
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            ViewBag.ControllerName = controllerName;
            var recruits = _context.RecruitModel.ToList();
            ViewBag.recruits = recruits;
            return View();
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
        public async Task<IActionResult> Create(
            [Bind(
                "Id,Recruiter,OpeningDetails,CandidateName,ReceivedDate,ContactNumber,Email,Company,TotalExperience,RelevantExperience,CCTC,ECTC,CurrentLocation,PreferredLocation,NoticePeriodOrLastWorkingDay,HoldingOfferOrPackageAmount,ExperienceInCloudPlatforms,ExperienceInLeadHandling,ExperienceInPerformanceTesting,ContractRoleRequired")]
            RecruitModel recruitModel)
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
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "Id,Recruiter,OpeningDetails,CandidateName,ReceivedDate,ContactNumber,Email,Company,TotalExperience,RelevantExperience,CCTC,ECTC,CurrentLocation,PreferredLocation,NoticePeriodOrLastWorkingDay,HoldingOfferOrPackageAmount,ExperienceInCloudPlatforms,ExperienceInLeadHandling,ExperienceInPerformanceTesting,ContractRoleRequired")]
            RecruitModel recruitModel)
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
        public async Task<IActionResult> UploadExcel(IFormFile? excelFile)
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
                    var recruit = new RecruitModel
                    {
                        Recruiter = worksheet.Cells[row, 1]?.Value?.ToString(),
                        OpeningDetails = worksheet.Cells[row, 2]?.Value?.ToString(),
                        CandidateName = worksheet.Cells[row, 3]?.Value?.ToString(),
                        ReceivedDate = ParseExcelDate(worksheet.Cells[row, 4]?.Value?.ToString()),
                        ContactNumber = worksheet.Cells[row, 5]?.Value?.ToString(),
                        Email = worksheet.Cells[row, 6]?.Value?.ToString(),
                        Company = worksheet.Cells[row, 7]?.Value?.ToString(),
                        TotalExperience = worksheet.Cells[row, 8]?.Value?.ToString(),
                        RelevantExperience = worksheet.Cells[row, 9]?.Value?.ToString(),
                        CCTC = worksheet.Cells[row, 10]?.Value?.ToString(),
                        ECTC = worksheet.Cells[row, 11]?.Value?.ToString(),
                        CurrentLocation = worksheet.Cells[row, 12]?.Value?.ToString(),
                        PreferredLocation = worksheet.Cells[row, 13]?.Value?.ToString(),
                        NoticePeriodOrLastWorkingDay = worksheet.Cells[row, 14]?.Value?.ToString(),
                        HoldingOfferOrPackageAmount = worksheet.Cells[row, 15]?.Value?.ToString(),
                        ExperienceInCloudPlatforms = worksheet.Cells[row, 16]?.Value?.ToString(),
                        ExperienceInLeadHandling = worksheet.Cells[row, 17]?.Value?.ToString(),
                        ExperienceInPerformanceTesting = worksheet.Cells[row, 18]?.Value?.ToString(),
                        ContractRoleRequired = worksheet.Cells[row, 19]?.Value?.ToString(),
                    };
                        _context.RecruitModel.Add(recruit);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        private string ParseExcelDate(string dateString)
        {
            if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                return parsedDateTime.ToString("dd-MM-yyyy");
            }
            else
            {
                return string.Empty;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var recruits = from r in _context.RecruitModel
                select r;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower(); // Convert the search string to lowercase

                recruits = recruits.Where(r => r.CurrentLocation!.ToLower().Contains(searchString));
            }

            return View(await recruits.ToListAsync().ConfigureAwait(false));
            return RedirectToAction("DownloadFilteredRecruitsPdf", "Pdf", new { searchString });
        }
        [HttpGet]
        public IActionResult FilteredData(string? searchString)
        {
            var recruits = GetFilteredRecruits(searchString);
            return View(recruits);
        }
        
        [HttpGet]
        public IActionResult DownloadFilteredRecruitsPdf(string searchString)
        {
            return RedirectToAction("DownloadFilteredRecruitsPdf", "Pdf", new { search = searchString });
        }
        
        
        private IQueryable<RecruitModel> GetFilteredRecruits(string? searchString)
        {
            var recruits = _context.RecruitModel.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                recruits = recruits.Where(r => r.CurrentLocation!.ToLower().Contains(searchString));
            }

            return recruits;
        }
        
        
        
    }
}
