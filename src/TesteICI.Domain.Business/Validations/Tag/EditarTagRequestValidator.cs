using FluentValidation;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Validations.Tag;

public sealed class EditarTagRequestValidator : AbstractValidator<EditarTagRequest>
{
    private readonly ITagService _tagService;

    public EditarTagRequestValidator(ITagService tagService)
    {
        _tagService = tagService;

        RuleFor(x => x.TagId)
            .NotEmpty().WithMessage("A id é obrigatório.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .Length(3, 100).WithMessage("A descricao deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.TagId)
            .MustAsync(TagExiste).WithMessage("Tag não encontrada.");
    }

    private async Task<bool> TagExiste(long id, CancellationToken token)
    {
        var tag = await _tagService.ObterPorId(id);

        return tag is not null;
    }
}
