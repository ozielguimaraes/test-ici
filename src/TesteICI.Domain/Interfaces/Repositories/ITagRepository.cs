using TesteICI.Domain.Entities;

namespace TesteICI.Domain.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<bool> TodosExistem(List<long> ids, CancellationToken token);
    }
}
