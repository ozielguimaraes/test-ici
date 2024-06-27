using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Infra.Data.Context;

namespace TesteICI.Infra.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(MainContext context) : base(context) { }
    }
}
