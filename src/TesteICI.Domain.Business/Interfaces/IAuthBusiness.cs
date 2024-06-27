using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Responses.Auth;

namespace TesteICI.Domain.Business.Interfaces;

public interface IAuthBusiness
{
    Task<SignupResponse> Validate(SignupRequest request, CancellationToken cancellationToken);
    Task<SigninResponse> Validate(SigninRequest request, CancellationToken cancellationToken);
}
