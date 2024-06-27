using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface ITagBusiness
    {
        Task<TagResponse> ObterPorId(long tagId);
        Task<IEnumerable<TagResponse>> GetAllAsync();
        Task<AdicionarTagResponse> Adicionar(AdicionarTagRequest request);
        Task<EditarTagResponse> Editar(EditarTagRequest request);
        Task<BaseResponse> Deletar(EditarTagRequest request, CancellationToken cancellationToken);
    }
}
