namespace AgendaUpc.Models.Responses;

public class SubjectResponse
{
    public int IdMateria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Grupo { get; set; }

    public string? Generacion { get; set; } = null!;
}