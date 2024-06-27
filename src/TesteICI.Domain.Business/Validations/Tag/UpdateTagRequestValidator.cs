using FluentValidation;
using TesteICI.Domain.Business.Requests.Tag;

namespace TesteICI.Domain.Business.Validations.Tag;

public sealed class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
{
    public UpdateTagRequestValidator()
    {
        RuleFor(x => x.TagId)
            .NotEmpty().WithMessage("A id é obrigatório.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .Length(3, 100).WithMessage("A descricao deve ter entre 3 e 100 caracteres.");
    }
}
