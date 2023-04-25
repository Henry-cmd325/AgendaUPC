namespace AgendaUpc.Models.Requests;

public class NotificationRequest
{
    public int IdUsuarios { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime FechaHora { get; set; }
}