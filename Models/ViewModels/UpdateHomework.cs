namespace AgendaUpc.Models.ViewModels;

public class UpdateHomework
{
    public int IdTarea { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdMateria { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaLimite { get; set; }
}