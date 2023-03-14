using System.ComponentModel.DataAnnotations;

namespace AgendaUpc.Models.Requests;

public class UserRequest 
{
    public string Nombre { get; set; } = null!;
    public string Matricula { get; set; } = null!;
    public string ContraSiupc { get; set; } = null!;
    public string ContraUpdc { get; set; } = null!;
}