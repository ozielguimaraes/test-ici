using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface INoticiaService : IDisposable
    {
        Task<Noticia> Add(Noticia obj);
        Task<Noticia> Update(Noticia obj);
        Task Remove(long noticiaId);
        Task<Noticia> GetById(long noticiaId);
        Task<bool> HasAny(Expression<Func<Noticia, bool>> predicate, CancellationToken cancellationToken);
        IQueryable<Noticia> All();
    }
}
