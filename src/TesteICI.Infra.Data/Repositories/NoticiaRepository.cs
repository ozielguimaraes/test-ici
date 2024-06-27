using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Infra.Data.Context;

namespace TesteICI.Infra.Data.Repositories
{
    public class NoticiaRepository : Repository<Noticia>, INoticiaRepository
    {
        public NoticiaRepository(MainContext context) : base(context) { }
    }
}
