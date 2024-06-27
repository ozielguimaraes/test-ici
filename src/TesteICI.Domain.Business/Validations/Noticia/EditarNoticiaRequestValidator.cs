using FluentValidation;
using TesteICI.Domain.Business.Requests.Noticia;

namespace TesteICI.Domain.Business.Validations.Noticia;

public class EditarNoticiaRequestValidator : AbstractValidator<EditarNoticiaRequest>
{
    public EditarNoticiaRequestValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .Length(10, 250).WithMessage("O título deve ter entre 10 e 250 caracteres.");

        RuleFor(x => x.Texto)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .Length(250).WithMessage("O título deve ter entre 10 e 250 caracteres.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O usuário é obrigatório.");

        //RuleFor(x => x.TagIds)
        //    .NotEmpty().WithMessage("As tags são obrigatórias.");
    }
}
