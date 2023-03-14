namespace AgendaUpc.Models.Requests;

public class LoginRequest
{
    public string Nombre {get; set;} = null!;
    public string Password {get; set;} = null!;
}