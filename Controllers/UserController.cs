using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgendaUpc.Context;
using AgendaUpc.Models.Requests;
using AgendaUpc.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AgendaUpc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AgendaUpc.Controllers;

public class UserController : Controller 
{
    private readonly IUserService _service;
    private readonly IHttpContextAccessor _accessor;
    public UserController(IUserService service, IHttpContextAccessor accessor)
    {
        _service = service;
        _accessor = accessor;
    }

    public IActionResult Login()
    {
        ClaimsPrincipal c = HttpContext.User;

        if(c.Identity != null)
        {
            if (c.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Homework");
        }

        ViewBag.Message = TempData["Message"];
        
        return View("Views/User/Login.cshtml");
    }

    public IActionResult Signin()
    {
        return View("Views/User/Signin.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", request);
        }

        var response = _service.CheckLogin(request);

        if (!response.Success)
        {
            TempData["Message"] = response.Error;
            return RedirectToAction("Login");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, response.Data!.Nombre)
        };

        ClaimsIdentity ci = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties p = new();

        p.AllowRefresh = true;
        p.IsPersistent = false;

        p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

        _accessor.HttpContext!.Session.SetInt32("idUser", response.Data.IdUsuario);

        return RedirectToAction("Index", "Homework");
    }

    [HttpPost]
    public IActionResult Signin(SigninOutMoodle request)
    {
        if (!ModelState.IsValid)
        {
            return View("Signin", request);
        }

        var response = _service.Post(request);

        if (!response.Success) return RedirectToAction("Signin");

        return RedirectToAction("Login");
    }

    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}