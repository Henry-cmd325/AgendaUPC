using AgendaUpc.Models.Responses;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.ViewModels;

namespace AgendaUpc.Services;

public interface IUserService : IService<UserResponse, UserRequest>
{
    public ServerResponse<UserResponse> CheckLogin(LoginRequest request);
    public ServerResponse<UserResponse> Post(SigninOutMoodle request);
}