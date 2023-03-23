using AgendaUpc.Models.Responses;

namespace AgendaUpc.Models.ViewModels;

public class DetailsHomework
{
    public List<HomeworkResponse> Homework { get; set; } = null!;
    public List<SubjectResponse> Subjects{get; set;} = null!;
}