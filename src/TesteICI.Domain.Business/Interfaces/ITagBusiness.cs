using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface ITagBusiness
    {
        Task<TagResponse> GetById(long tagId);
        Task<IEnumerable<TagResponse>> GetAllAsync();
        Task<AdicionarTagResponse> Create(AdicionarTagRequest request);
        Task<EditarTagResponse> Update(EditarTagRequest request);
    }
}
