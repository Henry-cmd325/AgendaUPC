using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AgendaUpc.Services;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.ViewModels;
using AgendaUpc.Models.Responses;

namespace AgendaUpc.Controllers;

[Authorize]
public class HomeworkController : Controller
{
    private readonly IHomeworkService _service;
    private readonly ISubjectService _subjects;
    private readonly INotificationService _notifications;
    private readonly int _idUsuario;

    public HomeworkController(IHomeworkService service, IHttpContextAccessor accessor, ISubjectService subjects, INotificationService notifications)
    {
        _service = service;
        _idUsuario = Convert.ToInt32(accessor.HttpContext!.Session.GetInt32("idUser"));
        _subjects = subjects;
        _notifications = notifications;
    }

    public IActionResult Index(int id)
    {
        _notifications.CheckNotifications(_idUsuario);

        var model = new DetailsHomework()
        {
            Homework = _service.GetAll(_idUsuario).Data!,
            Subjects = _subjects.GetAll(_idUsuario).Data!
        };

        return View("Views/Homework/Index.cshtml", model);
    }

    [Route("Subject/{id:int}")]
    public IActionResult Subject(int id)
    {
        var model = new DetailsHomework()
        {
            Homework = _service.GetAll(_idUsuario, id).Data!,
            Subjects = new()
        };
        model.Subjects.Add(_subjects.Get(id).Data!);

        return View("Views/Homework/Index.cshtml", model);
    }

    public IActionResult Completed()
    {
        return View(_service.GetAllCompletadas(_idUsuario).Data);
    }

    public IActionResult Create()
    {
        return View(_subjects.GetAll(_idUsuario).Data);
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

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PutHomework(UpdateHomework request)
    {
        var response = _service.Put(request);

        if (!response.Success)
        {
            TempData["Error"] = response.Error;

            return RedirectToAction("Details/" + request.IdTarea.ToString());
        }

        return RedirectToAction("Index");
    }

    [Route("Complete/{id:int}")]
    public IActionResult CompleteHomework(int id)
    {
        var tareaUsuario = _service.Get(id, _idUsuario);

        if (!tareaUsuario.Success) return RedirectToAction("Index");

        var response = _service.CompletarTarea(id);
        _notifications.CheckNotifications(_idUsuario);

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