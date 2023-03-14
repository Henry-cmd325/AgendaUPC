using AgendaUpc.Models.Responses;

namespace AgendaUpc.Services;

public interface IService<TResponse, TRequest>
{
    public ServerResponse<TResponse> Get(int id);
    public ServerResponse<List<TResponse>> GetAll();
    public bool Delete(int id);
    public ServerResponse<TResponse> Post(TRequest request);
    public ServerResponse<TResponse> Put(TRequest request, int id);
}