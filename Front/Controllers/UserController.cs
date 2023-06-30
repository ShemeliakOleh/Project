using Front.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Front.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDBContext _context;

    public UserController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { Username = model.Username, Password = model.Password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "home");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                // Login successful, store user ID in session and redirect to home page
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("index", "home");
            }
            else
            {
                // Login failed, show error
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Login");
    }


}
