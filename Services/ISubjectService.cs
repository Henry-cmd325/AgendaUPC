using AgendaUpc.Services;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;

namespace AgendaUpc.Services;

public interface ISubjectService : IService<SubjectResponse, SubjectRequest>
{
    public ServerResponse<SubjectResponse> Post(int idUsuario, SubjectRequest request);
    public ServerResponse<List<SubjectResponse>> GetAll(int idUsuario);
    public ServerResponse<int> GetIdByName(string nombre);
    public ServerResponse<SubjectResponse> Put(SubjectResponse request);
}