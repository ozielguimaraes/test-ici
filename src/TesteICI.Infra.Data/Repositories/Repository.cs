using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Infra.Data.Context;

namespace TesteICI.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MainContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(MainContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
            return obj;
        }

        public virtual TEntity Editar(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;

            return obj;
        }

        public virtual void Remover(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public async virtual Task<TEntity?> ObterPorId(long id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public async virtual Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbSet.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<IList<TEntity>> ObterTodos(CancellationToken cancellationToken)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async virtual Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return await Db.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
