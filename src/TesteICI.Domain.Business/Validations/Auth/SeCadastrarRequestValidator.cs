using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Validations.Auth;
public sealed class SeCadastrarRequestValidator : AbstractValidator<SeCadastrarRequest>
{
    private readonly IUsuarioService _usuarioService;

    public SeCadastrarRequestValidator(IUsuarioService usuarioService, IOptions<IdentityOptions> identityOptions)
    {
        var passwordOptions = identityOptions.Value.Password;
        _usuarioService = usuarioService;

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 250).WithMessage("O nome deve ter entre 2 e 250 caracteres.")
            .NomeDePessoaValido();

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser válido.")
            .MaximumLength(250).WithMessage("O nome deve ter até 250 caracteres.")
            .MustAsync(BeUniqueEmail).WithMessage("O email já está em uso.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .PossuiSenhaDentroDoPadrao(passwordOptions);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _usuarioService.EmailExiste(email, cancellationToken);
    }
}
