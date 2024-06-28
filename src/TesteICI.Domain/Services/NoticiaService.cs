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

    public async Task<Noticia> Adicionar(Noticia noticia, CancellationToken cancellationToken)
    {
        BeginTransaction();
        noticia = _noticiaRepository.Adicionar(noticia);
        await CommitAsync(cancellationToken);
        return noticia;
    }

    public async Task<Noticia> Editar(Noticia noticiaUpdated, CancellationToken cancellationToken)
    {
        BeginTransaction();
        var noticia = await ObterPorId(noticiaUpdated.NoticiaId, cancellationToken);
        noticia.Update(noticiaUpdated);
        noticiaUpdated = _noticiaRepository.Editar(noticia);
        await CommitAsync(cancellationToken);
        return noticiaUpdated;
    }

    public async Task Remover(long noticiaId, CancellationToken cancellationToken)
    {
        BeginTransaction();
        _noticiaRepository.Remover(await ObterPorId(noticiaId, cancellationToken));
        await CommitAsync(cancellationToken);
    }

    public async Task<Noticia> ObterPorId(long noticiaId, CancellationToken cancellationToken)
    {
        var noticia = await _noticiaRepository.ObterPorId(noticiaId, cancellationToken);

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

    public async Task<bool> Deletar(long usuarioId, CancellationToken cancellationToken)
    {
        BeginTransaction();
        var item = await ObterPorId(usuarioId, cancellationToken);
        if (item is null)
            return false;

        _noticiaRepository.Remover(item);
        await CommitAsync(cancellationToken);

        return true;
    }

    public void Dispose()
    {
        _noticiaRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
