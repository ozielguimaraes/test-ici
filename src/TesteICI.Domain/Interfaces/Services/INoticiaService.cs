using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface INoticiaService : IDisposable
    {
        Task<Noticia> Adicionar(Noticia obj, CancellationToken cancellationToken);
        Task<Noticia> Editar(Noticia obj, CancellationToken cancellationToken);
        Task Remover(long noticiaId, CancellationToken cancellationToken);
        Task<Noticia> ObterPorId(long noticiaId, CancellationToken cancellationToken);
        Task<bool> HasAny(Expression<Func<Noticia, bool>> predicate, CancellationToken cancellationToken);
        Task<IList<Noticia>> ObterTodas(CancellationToken cancellationToken);
        Task<bool> Deletar(long usuarioId, CancellationToken cancellationToken);
    }
}
