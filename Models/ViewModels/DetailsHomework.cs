using AgendaUpc.Models.Responses;

namespace AgendaUpc.Models.ViewModels;

public class DetailsHomework
{
    public int IdTarea { get; set; }
    public string Materia { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime FechaLimite {get; set;}
    public List<SubjectResponse> Subjects{get; set;} = null!;
}