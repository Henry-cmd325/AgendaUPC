namespace AgendaUpc.Models.Responses;

public class HomeworkResponse
{
    public int IdTarea { get; set; }
    public int IdUsuario { get; set; }
    public string Materia { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime FechaLimite {get; set;}
}