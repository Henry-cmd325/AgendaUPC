using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.Controllers;

public class ScheduleController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}