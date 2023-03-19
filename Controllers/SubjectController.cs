using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;
using AgendaUpc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.Controllers;

[Authorize]
public class SubjectController : Controller
{
    private readonly ISubjectService _service;

    private readonly int _idUsuario;

    public SubjectController(ISubjectService service, IHttpContextAccessor accessor)
    {
        _service = service;
        _idUsuario = Convert.ToInt32(accessor.HttpContext!.Session.GetInt32("idUser"));
    }

    public IActionResult Index()
    {
        return View(_service.GetAll(_idUsuario).Data);
    }

    [HttpPost]
    public IActionResult EditSubject(SubjectResponse request)
    {
        var response = _service.Put(request);

        if (!response.Success) TempData["Error"] = response.Error;

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CreateSubject(SubjectRequest request)
    {
        var response = _service.Post(_idUsuario, request);

        if (!response.Success) TempData["Error"] = response.Error;

        return RedirectToAction("Index");
    }
}