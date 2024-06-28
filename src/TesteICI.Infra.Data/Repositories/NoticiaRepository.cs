using Microsoft.EntityFrameworkCore;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Infra.Data.Context;

namespace TesteICI.Infra.Data.Repositories
{
    public class NoticiaRepository : Repository<Noticia>, INoticiaRepository
    {
        public NoticiaRepository(MainContext context) : base(context) { }

        public override async Task<Noticia?> ObterPorId(long id, CancellationToken cancellationToken)
        {
            return await DbSet
                .Include(x => x.Tags).ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.NoticiaId == id, cancellationToken);
        }
    }
}
