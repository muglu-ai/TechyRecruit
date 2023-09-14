using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TechyRecruit.Controllers;

[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    //List all 

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole model)
    {
        if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
        {
            await _roleManager.CreateAsync(model);
        }

        return RedirectToAction("Index");

    }


    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}   