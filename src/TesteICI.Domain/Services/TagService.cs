using System.Linq.Expressions;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Services;

public class TagService : BaseService, ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(IUnitOfWork uow, ITagRepository tagRepository) : base(uow)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> Add(Tag tag)
    {
        BeginTransaction();
        tag = _tagRepository.Add(tag);
        await CommitAsync();
        return tag;
    }

    public async Task<Tag> Update(Tag tagUpdated)
    {
        BeginTransaction();
        var tag = await GetById(tagUpdated.TagId);
        tag.Update(tagUpdated);
        tagUpdated = _tagRepository.Update(tag);
        await CommitAsync();
        return tagUpdated;
    }

    public async Task Remove(long tagId)
    {
        BeginTransaction();
        var item = await GetById(tagId);
        if (item != null)
            _tagRepository.Remove(item);
        await CommitAsync();
    }

    public async Task<Tag?> GetById(long tagId)
    {
        return await _tagRepository.GetById(tagId);
    }

    public async virtual Task<bool> HasAny(Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _tagRepository.HasAnyAsync(predicate, cancellationToken);
    }

    public async Task<IQueryable<Tag>> Filter(Expression<Func<Tag, bool>> predicate)
    {
        return _tagRepository.Filter(predicate);
    }

    public IQueryable<Tag> All()
    {
        return _tagRepository.All();
    }

    public void Dispose()
    {
        _tagRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
