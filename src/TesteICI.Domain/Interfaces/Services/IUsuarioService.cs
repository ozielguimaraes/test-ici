using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface IUsuarioService : IDisposable
    {
        Task<Usuario> Add(Usuario obj);
        Task<Usuario> Update(Usuario obj);
        Task Remove(long usuarioId);
        Task<Usuario> GetById(long usuarioId);
        Task<bool> EmailExiste(string email, CancellationToken cancellationToken);
        Task<bool> HasAny(Expression<Func<Usuario, bool>> predicate, CancellationToken cancellationToken);
        IQueryable<Usuario> All();
        Task<IQueryable<Usuario>> Filter(Expression<Func<Usuario, bool>> predicate, CancellationToken cancellationToken);
    }
}
