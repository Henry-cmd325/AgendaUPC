namespace AgendaUpc.Responses;

public class ScheduleResponse
{
    public int IdSchedule {get; set;}
    public string Dia {get; set;} = string.Empty;
    public string Materia {get; set;} = string.Empty;
    public TimeOnly Hora {get; set;}
}