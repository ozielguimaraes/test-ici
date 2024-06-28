using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> PesquisarPorDescricao(string pesquisa, CancellationToken cancellationToken);
        Task<bool> TodosExistem(List<long> ids, CancellationToken token);
    }
}
