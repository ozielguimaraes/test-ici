using FluentValidation;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Validations.Noticia;

public class AdicionarNoticiaRequestValidator : AbstractValidator<AdicionarNoticiaRequest>
{
    private readonly ITagService _tagService;

    public AdicionarNoticiaRequestValidator(ITagService tagService)
    {
        _tagService = tagService;

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .Length(10, 250).WithMessage("O título deve ter entre 10 e 250 caracteres.");

        RuleFor(x => x.Texto)
            .NotEmpty().WithMessage("O conteúdo é obrigatório.")
            .Length(200, 8000).WithMessage("O conteúdo deve ter entre 200 e 8000 caracteres.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O usuário é obrigatório.");

        RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("As tags são obrigatórias.")
            .MustAsync(TodosExistemAsync).WithMessage("Uma ou mais tags não encontradas.");
    }

    private async Task<bool> TodosExistemAsync(List<long> list, CancellationToken token)
        => await _tagService.TodosExistem(list, token);
}
