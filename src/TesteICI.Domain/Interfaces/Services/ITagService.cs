using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface ITagService : IDisposable
    {
        Task<Tag> Adicionar(Tag obj);
        Task<Tag> Editar(Tag obj);
        Task Deletar(long tagId);
        Task<Tag?> ObterPorId(long tagId);
        Task<bool> PossuiAlgum(Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken);
        IQueryable<Tag> All();
        Task<IQueryable<Tag>> Filter(Expression<Func<Tag, bool>> predicate);
    }
}
