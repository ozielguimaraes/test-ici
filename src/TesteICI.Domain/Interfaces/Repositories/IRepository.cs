using System.Linq.Expressions;

namespace TesteICI.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Adicionar(TEntity obj);
        TEntity Editar(TEntity obj);
        void Remover(TEntity obj);
        Task<TEntity?> ObterPorId(long id, CancellationToken cancellationToken);
        Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<IList<TEntity>> ObterTodos(CancellationToken cancellationToken);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
