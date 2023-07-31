using ToDo.Client.Entities.Requests.Auth;
using ToDo.Client.Entities.Responses.Auth;

namespace ToDo.Business.Contracts.Engines
{
    public interface IAuthEngine
    {
        TokenResponse CreateToken(TokenRequest request);
    }
}
