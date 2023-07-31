using ToDo.Client.Entities.Requests.Auth;
using ToDo.Client.Entities.Responses;
using ToDo.Client.Entities.Responses.Auth;

namespace ToDo.Business.Contracts.Services
{
    public interface IAuthService
    {
        ApiResponse CreateToken(TokenRequest request);
    }
}
