using FluentValidation;
using TesteICI.Domain.Business.Requests.Tag;

namespace TesteICI.Domain.Business.Validations.Tag;

public sealed class AdicionarTagRequestValidator : AbstractValidator<AdicionarTagRequest>
{
    public AdicionarTagRequestValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .Length(3, 100).WithMessage("A descricao deve ter entre 3 e 100 caracteres.");
    }
}
