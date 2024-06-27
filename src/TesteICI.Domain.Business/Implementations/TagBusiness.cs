using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses.Tag;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Implementations;

public class TagBusiness : ITagBusiness
{
    private readonly ITagService _tagService;

    public TagBusiness(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<CreateTagResponse> Create(CreateTagRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Descricao is null)
            throw new ArgumentNullException(nameof(request.Descricao));

        var tag = new Tag(request.Descricao);

        var result = await _tagService.Add(tag);

        return new CreateTagResponse(result.TagId);
    }

    public async Task<UpdateTagResponse> Update(UpdateTagRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tag = new Tag(request.TagId, request.Descricao);
        var result = await _tagService.Update(tag);

        return new UpdateTagResponse(result.TagId);
    }

    public async Task<IEnumerable<TagResponse>> GetAllAsync()
    {
        var queryable = _tagService.All();
        var tags = queryable.ToList();
        return await Task.FromResult(tags.Select(x => new TagResponse(x)).ToList());
    }

    public async Task<TagResponse> GetById(long tagId)
    {
        var result = await _tagService.GetById(tagId);
        if (result is null)
            return new TagResponse(new FluentValidation.Results.ValidationResult
            {
                Errors = new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("TagId", "Tag não encontrada")
                }
            });

        return new TagResponse(result);
    }
}
