namespace AgendaUpc.Models.Requests;

public class ScheduleRequest 
{
    public int IdDia {get; set;}
    public int IdMateria {get; set;}
    public TimeOnly Hora {get; set;}
}