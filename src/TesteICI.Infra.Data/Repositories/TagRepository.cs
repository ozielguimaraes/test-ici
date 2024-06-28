using Microsoft.EntityFrameworkCore;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Infra.Data.Context;

namespace TesteICI.Infra.Data.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(MainContext context) : base(context) { }

    public async Task<IEnumerable<Tag>> PesquisarPorDescricao(string pesquisa, CancellationToken cancellationToken)
    {
        return await DbSet.Where(x => x.Descricao.Contains(pesquisa)).ToListAsync(cancellationToken);
    }

    public async Task<bool> TodosExistem(List<long> ids, CancellationToken cancellationToken)
    {
        var totalNoBanco = await DbSet.CountAsync(x => ids.Contains(x.TagId), cancellationToken);

        return totalNoBanco == ids.Count;
    }
}
