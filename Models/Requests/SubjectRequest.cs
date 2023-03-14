namespace AgendaUpc.Models.Requests;

public class SubjectRequest 
{
    public string Nombre { get; set; } = null!;

    public string? Grupo { get; set; }

    public string? Generacion { get; set; }
}