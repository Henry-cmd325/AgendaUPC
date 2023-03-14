namespace AgendaUpc.Models.Responses;

public class UserResponse
{
    public int IdUsuario {get; set;}
    public string Nombre { get; set; } = null!;
    public string Matricula { get; set; } = null!;
    public string ContraSiupc { get; set; } = null!;
    public string ContraUpdc { get; set; } = null!;
}