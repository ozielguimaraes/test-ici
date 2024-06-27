using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Responses.Auth;

namespace TesteICI.Domain.Business.Interfaces;

public interface IAuthBusiness
{
    Task<SeCadastrarResponse> Validate(SeCadastrarRequest request, CancellationToken cancellationToken);
    Task<EfetuarLoginResponse> Validate(EfetuarLoginRequest request, CancellationToken cancellationToken);
}
