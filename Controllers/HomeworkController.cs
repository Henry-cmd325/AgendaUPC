using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AgendaUpc.Services;
using AgendaUpc.Models.Requests;

namespace AgendaUpc.Controllers;

[Authorize]
public class HomeworkController : Controller
{
    private readonly IHomeworkService _service;
    private readonly ISubjectService _subjects;
    private readonly IHttpContextAccessor _accessor;
    private readonly int _idUsuario;

    public HomeworkController(IHomeworkService service, IHttpContextAccessor accessor, ISubjectService subjects)
    {
        _service = service;
        _accessor = accessor;
        _idUsuario = Convert.ToInt32(_accessor.HttpContext!.Session.GetInt32("idUser"));
        _subjects = subjects;
    }
    public IActionResult Index()
    {
        return View(_service.GetAll(_idUsuario).Data);
    }

    public IActionResult Completed()
    {
        return View(_service.GetAllCompletadas(_idUsuario).Data);
    }

    public IActionResult Create()
    {
        return View(_subjects.GetAll(_idUsuario).Data);
    }

    [Route("Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var response = _service.Get(id, _idUsuario);

        if (!response.Success)
        {
            TempData["Error"] = response.Error;

            return RedirectToAction("Index");
        }

        return View(response.Data);
    }

    [HttpPost]
    public IActionResult PostHomework(HomeworkRequest request)
    {
        var response = _service.Post(_idUsuario, request);

        if (!response.Success)
        {
            TempData["Error"] = response.Error;

            return RedirectToAction("Create");
        }

        TempData["Created"] = "La tarea fue creada exitosamente";

        return RedirectToAction("Create");
    }

    [Route("Complete/{id:int}")]
    public IActionResult CompleteHomework(int id)
    {
        var tareaUsuario = _service.Get(id, _idUsuario);

        if (!tareaUsuario.Success) return RedirectToAction("Index");

        var response = _service.CompletarTarea(id);

        if (!response.Success)
        {
            TempData["Error"] = response.Error;

            return RedirectToAction("Index");
        }

        TempData["Success"] = "La tarea ha sido marcado como completada";

        return RedirectToAction("Index");
    }

    [Route("Uncheck/{id:int}")]
    public IActionResult UncheckHomework(int id)
    {
        var tareaUsuario = _service.Get(id, _idUsuario);

        if (!tareaUsuario.Success) return RedirectToAction("Index");

        var response = _service.DesmarcarTarea(id);

        if (!response.Success)
        {
            TempData["Error"] = response.Error;

            return RedirectToAction("Index");
        }

        TempData["Success"] = "La tarea ha sido desmarcada como completada";

        return RedirectToAction("Completed");
    }
} 