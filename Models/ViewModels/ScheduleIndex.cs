using AgendaUpc.Models.Responses;

namespace AgendaUpc.Models.ViewModels;

public class ScheduleIndex 
{
    public List<ScheduleResponse> Schedules { get; set; } = null!;
    public List<SubjectResponse> Subjects { get; set; } = null!;
}