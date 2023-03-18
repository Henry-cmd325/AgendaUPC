using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;
using AgendaUpc.Models.ViewModels;

namespace AgendaUpc.Services;

public interface IHomeworkService : IService<HomeworkResponse, HomeworkRequest>
{
    public ServerResponse<List<HomeworkResponse>> GetAll(int idUsuario);
    public ServerResponse<List<HomeworkResponse>> GetAll(int idUsuario, int idMateria);
    public ServerResponse<List<HomeworkResponse>> GetAllCompletadas(int idUsuario);
    public ServerResponse<HomeworkResponse> CompletarTarea(int idTarea); 
    public ServerResponse<HomeworkResponse> DesmarcarTarea(int idTarea);
    public ServerResponse<HomeworkResponse> Get(int idTarea, int idUsuario);
    public ServerResponse<HomeworkResponse> Post(int idUsuario, HomeworkRequest request);
    public ServerResponse<HomeworkResponse> Put(UpdateHomework request);
}