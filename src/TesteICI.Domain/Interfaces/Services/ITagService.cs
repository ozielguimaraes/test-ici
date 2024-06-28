using System.Linq.Expressions;
using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Services
{
    public interface ITagService : IDisposable
    {
        Task<Tag> Adicionar(Tag obj, CancellationToken cancellationToken);
        Task<Tag?> Editar(Tag obj, CancellationToken cancellationToken);
        Task<bool> Deletar(long tagId, CancellationToken cancellationToken);
        Task<Tag?> ObterPorId(long tagId, CancellationToken cancellationToken);
        Task<bool> PossuiAlgum(Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken);
        Task<IList<Tag>> ObterTodas(CancellationToken cancellationToken);
        Task<IEnumerable<Tag>> Pesquisar(string pesquisa, CancellationToken cancellationToken);
        Task<bool> TodosExistem(List<long> list, CancellationToken token);
    }
}
