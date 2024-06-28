using FluentValidation;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Validations.Noticia;

public class EditarNoticiaRequestValidator : AbstractValidator<EditarNoticiaRequest>
{
    private readonly INoticiaService _noticiaService;
    private readonly ITagService _tagService;

    public EditarNoticiaRequestValidator(INoticiaService noticiaService, ITagService tagService)
    {
        _noticiaService = noticiaService;
        _tagService = tagService;

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .Length(10, 250).WithMessage("O título deve ter entre 10 e 250 caracteres.");

        RuleFor(x => x.Texto)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .Length(250).WithMessage("O título deve ter entre 10 e 250 caracteres.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O usuário é obrigatório.");

        RuleFor(x => x.NoticiaId)
            .MustAsync(NoticiaExiste).WithMessage("Notícia não encontrada.");

        RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("As tags são obrigatórias.")
            .MustAsync(TodasExistemAsync).WithMessage("Uma ou mais tags não encontradas.");
    }

    private async Task<bool> TodasExistemAsync(List<long> list, CancellationToken token)
        => await _tagService.TodosExistem(list, token);

    private async Task<bool> NoticiaExiste(long id, CancellationToken token)
    {
        var noticia = await _noticiaService.ObterPorId(id, token);

        return noticia is not null;
    }
}
