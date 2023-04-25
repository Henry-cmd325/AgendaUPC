using AgendaUpc.Models.Requests;
using AgendaUpc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _service;
    private readonly int _idUsuario;
    public NotificationController(INotificationService service, IHttpContextAccessor accessor)
    {
        _service = service;
        _idUsuario = Convert.ToInt32(accessor.HttpContext!.Session.GetInt32("idUser"));
    }

    [HttpGet]
    public IActionResult GetAllNotifications()
    {
        return Ok(_service.GetAllNotifications(_idUsuario));
    }

    [HttpGet("{id}")]
    public IActionResult GetNotification(int id)
    {
        return Ok(_service.GetNotification(_idUsuario, id));
    }

    [HttpPut("{id}")]
    public IActionResult PutNotified(int id)
    {
        var response = _service.PutNotified(id);

        if (!response.Success) return NotFound(response);

        return Ok(response);
    }

    [HttpPost("{id}")]
    public IActionResult PutNotification(int id, NotificationRequest request)
    {
        var response = _service.PutNotification(_idUsuario, id, request);

        if (!response.Success) return NotFound(response);

        return CreatedAtRoute(nameof(GetNotification), new { Id = id, response.Data});
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNotification(int id)
    {
        var response = _service.DeleteNotification(_idUsuario, id);

        if (!response.Success) return NotFound(response);

        return NoContent();
    }
}