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
    private readonly IValidator<SeCadastrarRequest> _seCadastrarValidator;
    private readonly IValidator<EfetuarLoginRequest> _efetuarLoginValidator;
    private readonly ISecurityService _securityService;

    public AuthBusiness(IUsuarioBusiness usuarioBusiness, IValidator<SeCadastrarRequest> seCadastrarValidator, ISecurityService securityService, IValidator<EfetuarLoginRequest> efetuarLoginValidator)
    {
        _usuarioBusiness = usuarioBusiness;
        _seCadastrarValidator = seCadastrarValidator;
        _efetuarLoginValidator = efetuarLoginValidator;
        _securityService = securityService;
    }

    public async Task<SeCadastrarResponse> Validate(SeCadastrarRequest request, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await _seCadastrarValidator.ValidateAsync(request, cancellationToken);

        return new SeCadastrarResponse(resultadoValidacao);
    }

    public async Task<EfetuarLoginResponse> Validate(EfetuarLoginRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var resultadoValidacao = await _efetuarLoginValidator.ValidateAsync(request);
        if (!resultadoValidacao.IsValid)
            return new EfetuarLoginResponse(resultadoValidacao);

        var usuario = await _securityService.ObterPorEmailAsync(request.Login);

        if (usuario is null)
            return new EfetuarLoginResponse(validationResult: new ValidationResult(new List<ValidationFailure>
            {
                new() { ErrorMessage = "Usuário ou senha inválida." }
            }));

        return new EfetuarLoginResponse(validationResult: new ValidationResult());
    }
}
