using AgendaUpc.Models.Responses;
using AgendaUpc.Models.Requests;
using AgendaUpc.Responses;

namespace AgendaUpc.Services;

public interface IScheduleService
{
    public ServerResponse<List<ScheduleResponse>> GetAllSchedule(int idUsuario);
    public ServerResponse<ScheduleResponse> GetSchedule(int id);
    public ServerResponse<ScheduleResponse> PostSchedule(ScheduleRequest request);
    public ServerResponse<ScheduleResponse> UpdateSchedule(int id, ScheduleRequest request);
    public bool DeleteSchedule(int id);
}