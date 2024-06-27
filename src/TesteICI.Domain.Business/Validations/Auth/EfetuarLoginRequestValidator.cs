using FluentValidation;
using TesteICI.Domain.Business.Requests.Auth;

namespace TesteICI.Domain.Business.Validations.Auth;

public sealed class EfetuarLoginRequestValidator : AbstractValidator<EfetuarLoginRequest>
{
    public EfetuarLoginRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("O login é obrigatório.")
            .EmailAddress().WithMessage("O login deve ser válido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
