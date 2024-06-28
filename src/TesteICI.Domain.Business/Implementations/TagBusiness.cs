using FluentValidation;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Tag;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Implementations;

public class TagBusiness : ITagBusiness
{
    private readonly ITagService _tagService;
    private readonly IValidator<AdicionarTagRequest> _adicionarTagValidator;
    private readonly IValidator<EditarTagRequest> _editarTagValidator;

    public TagBusiness(ITagService tagService, IValidator<EditarTagRequest> editarTagValidator, IValidator<AdicionarTagRequest> adicionarTagValidator)
    {
        _tagService = tagService;
        _editarTagValidator = editarTagValidator;
        _adicionarTagValidator = adicionarTagValidator;
    }

    public async Task<AdicionarTagResponse> Adicionar(AdicionarTagRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var resultadoValidacao = await _adicionarTagValidator.ValidateAsync(request);

        if (!resultadoValidacao.IsValid)
            return new AdicionarTagResponse(resultadoValidacao);

        var tag = new Tag(request.Descricao);
        var result = await _tagService.Adicionar(tag, cancellationToken);

        return new AdicionarTagResponse(result.TagId);
    }

    public async Task<BaseResponse> Deletar(long tagId, CancellationToken cancellationToken)
    {
        var foiDeletado = await _tagService.Deletar(tagId, cancellationToken);

        if (foiDeletado)
            return new EmptyResponse();

        return new NullResponse();
    }

    public async Task<EditarTagResponse> Editar(EditarTagRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var resultadoValidacao = await _editarTagValidator.ValidateAsync(request);
        if (!resultadoValidacao.IsValid)
            return new EditarTagResponse(resultadoValidacao);

        var tag = new Tag(request.TagId, request.Descricao);
        var result = await _tagService.Editar(tag, cancellationToken);

        if (result is null)
            return new EditarTagResponse(new FluentValidation.Results.ValidationResult
            {
                Errors = new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("TagId", "Tag não encontrada")
                }
            });

        return new EditarTagResponse(result.TagId, request.Descricao);
    }

    public async Task<IEnumerable<TagResponse>> GetAllAsync()
    {
        var queryable = _tagService.All();
        var tags = queryable.ToList();
        return await Task.FromResult(tags.Select(x => new TagResponse(x)).ToList());
    }

    public async Task<BaseResponse> ObterPorId(long tagId, CancellationToken cancellationToken)
    {
        var result = await _tagService.ObterPorId(tagId, cancellationToken);
        if (result is null)
            return new NullResponse();

        return new TagResponse(result);
    }
}
