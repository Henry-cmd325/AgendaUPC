namespace AgendaUpc.Models.Responses;

public class NotificationResponse
{
    public int IdNotication { get; set; }
    public int IdUsuarios { get; set; }
    public string Mensaje { get; set; } = null!;
    public bool Notificado {get; set; }
    public DateTime FechaHora { get; set; }
}