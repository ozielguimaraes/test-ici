using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface ITagService : IDisposable
    {
        Task<Tag> Add(Tag obj);
        Task<Tag> Update(Tag obj);
        Task Remove(long tagId);
        Task<Tag?> GetById(long tagId);
        Task<bool> HasAny(Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken);
        IQueryable<Tag> All();
        Task<IQueryable<Tag>> Filter(Expression<Func<Tag, bool>> predicate);
    }
}
