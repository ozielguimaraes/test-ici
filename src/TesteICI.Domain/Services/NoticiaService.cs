using System.Linq.Expressions;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Services;

public class NoticiaService : BaseService, INoticiaService
{
    private readonly INoticiaRepository _noticiaRepository;

    public NoticiaService(IUnitOfWork uow, INoticiaRepository noticiaRepository) : base(uow)
    {
        _noticiaRepository = noticiaRepository;
    }

    public async Task<Noticia> Add(Noticia noticia)
    {
        BeginTransaction();
        noticia = _noticiaRepository.Add(noticia);
        await CommitAsync();
        return noticia;
    }

    public async Task<Noticia> Update(Noticia noticiaUpdated)
    {
        BeginTransaction();
        var noticia = await GetById(noticiaUpdated.NoticiaId);
        noticia.Update(noticiaUpdated);
        noticiaUpdated = _noticiaRepository.Update(noticia);
        await CommitAsync();
        return noticiaUpdated;
    }

    public async Task Remove(long noticiaId)
    {
        BeginTransaction();
        _noticiaRepository.Remove(await GetById(noticiaId));
        await CommitAsync();
    }

    public async Task<Noticia> GetById(long noticiaId)
    {
        var noticia = await _noticiaRepository.GetById(noticiaId);

        if (noticia is null)
            throw new Exception("Notícia não encontrada.");

        return noticia;
    }

    public async virtual Task<bool> HasAny(Expression<Func<Noticia, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _noticiaRepository.HasAnyAsync(predicate, cancellationToken);
    }


    public IQueryable<Noticia> All()
    {
        return _noticiaRepository.All();
    }

    public void Dispose()
    {
        _noticiaRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
