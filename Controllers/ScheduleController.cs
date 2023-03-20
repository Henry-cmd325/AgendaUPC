using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;
using AgendaUpc.Models.ViewModels;
using AgendaUpc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.Controllers;

[Authorize]
public class ScheduleController : Controller
{
    private readonly IScheduleService _service;
    private readonly ISubjectService _subjects;
    private readonly int _idUsuario;
    public ScheduleController(IScheduleService service, IHttpContextAccessor accessor, ISubjectService subjects)
    {
        _service = service;
        _idUsuario = Convert.ToInt32(accessor.HttpContext!.Session.GetInt32("idUser"));
        _subjects = subjects;
    }

    public IActionResult Index()
    {
        var model = new ScheduleIndex() 
        {
            Schedules = _service.GetAllSchedule(_idUsuario).Data!,
            Subjects = _subjects.GetAll(_idUsuario).Data!
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult UpdateSchedule(UpdateSchedule request)
    {
        var response = _service.UpdateSchedule(request);

        if (!response.Success) TempData["Error"] = response.Error;

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PostSchedule(ScheduleRequest request)
    {
        var response = _service.PostSchedule(request);

        if (!response.Success) TempData["Error"] = response.Error;

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteSchedule(DeleteRequest request)
    {
        var response = _service.DeleteNameSchedule(request);

        if (!response.Success) TempData["Error"] = response.Error;

        return RedirectToAction("Index");
    }
}