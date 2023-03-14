namespace AgendaUpc.Models.Requests;

public class HomeworkRequest
{

    public string Nombre { get; set; } = null!;
    public int IdMateria { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaLimite { get; set; }
}