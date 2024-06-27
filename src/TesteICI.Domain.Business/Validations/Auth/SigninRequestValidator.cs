using FluentValidation;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Validations.Auth;

public class SigninRequestValidator : AbstractValidator<SiginRequest>
{
    private readonly IUsuarioService _usuarioService;

    public SigninRequestValidator(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("O login é obrigatório.")
            .EmailAddress().WithMessage("O login deve ser válido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _usuarioService.EmailExiste(email, cancellationToken);
    }
}
