using AgendaUpc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.Controllers;

[Authorize]
public class SubjectController : Controller
{
    private readonly ISubjectService _service;

    private readonly int _idUsuario;

    public SubjectController(ISubjectService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View(_service.GetAll(_idUsuario));
    }
}