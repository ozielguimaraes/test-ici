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

    public async Task<Tag> Adicionar(Tag tag, CancellationToken cancellationToken)
    {
        BeginTransaction();
        tag = _tagRepository.Adicionar(tag);
        await CommitAsync(cancellationToken);
        return tag;
    }

    public async Task<Tag?> Editar(Tag tagUpdated, CancellationToken cancellationToken)
    {
        BeginTransaction();
        var tag = await ObterPorId(tagUpdated.TagId, cancellationToken);

        if (tag is null)
            return null;

        tag.Update(tagUpdated);
        tagUpdated = _tagRepository.Editar(tag);
        await CommitAsync(cancellationToken);

        return tagUpdated;
    }

    public async Task<bool> Deletar(long tagId, CancellationToken cancellationToken)
    {
        BeginTransaction();
        var item = await ObterPorId(tagId, cancellationToken);
        if (item is null)
            return false;

        _tagRepository.Remover(item);
        await CommitAsync(cancellationToken);

        return true;
    }

    public async Task<Tag?> ObterPorId(long tagId, CancellationToken cancellationToken)
    {
        return await _tagRepository.ObterPorId(tagId, cancellationToken);
    }

    public async virtual Task<bool> PossuiAlgum(Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _tagRepository.HasAnyAsync(predicate, cancellationToken);
    }

    public async Task<bool> TodosExistem(List<long> ids, CancellationToken token)
    {
        return await _tagRepository.TodosExistem(ids, token);
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
