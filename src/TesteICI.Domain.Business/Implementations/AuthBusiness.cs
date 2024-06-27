using FluentValidation;
using FluentValidation.Results;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Responses.Auth;
using TesteICI.Infra.CrossCutting.Security;

namespace TesteICI.Domain.Business.Implementations;

public sealed class AuthBusiness : IAuthBusiness
{
    private readonly IUsuarioBusiness _usuarioBusiness;
    private readonly IValidator<SeCadastrarRequest> _signupValidator;
    private readonly ISecurityService _securityService;

    public AuthBusiness(IUsuarioBusiness usuarioBusiness, IValidator<SeCadastrarRequest> signupValidator, ISecurityService securityService)
    {
        _usuarioBusiness = usuarioBusiness;
        _signupValidator = signupValidator;
        _securityService = securityService;
    }

    public async Task<SeCadastrarResponse> Validate(SeCadastrarRequest request, CancellationToken cancellationToken)
    {
        var result = await _signupValidator.ValidateAsync(request, cancellationToken);

        return new SeCadastrarResponse(result);
    }

    public async Task<EfetuarLoginResponse> Validate(EfetuarLoginRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Login))
            throw new ArgumentNullException(nameof(request.Login));

        if (string.IsNullOrEmpty(request.Senha))
            throw new ArgumentNullException(nameof(request.Senha));

        var usuario = await _securityService.ObterPorEmailAsync(request.Login);

        if (usuario is null)
            return new EfetuarLoginResponse(validationResult: new ValidationResult(new List<ValidationFailure>
            {
                new() { ErrorMessage = "Usuário ou senha inválida." }
            }));

        return new EfetuarLoginResponse(validationResult: new ValidationResult());
    }
}
