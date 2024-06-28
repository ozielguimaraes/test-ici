using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface ITagBusiness
    {
        Task<BaseResponse> ObterPorId(long tagId, CancellationToken cancellationToken);
        Task<IEnumerable<TagResponse>> ObterTodas(CancellationToken cancellationToken);
        Task<AdicionarTagResponse> Adicionar(AdicionarTagRequest request, CancellationToken cancellationToken);
        Task<EditarTagResponse> Editar(EditarTagRequest request, CancellationToken cancellationToken);
        Task<BaseResponse> Deletar(long tagId, CancellationToken cancellationToken);
        Task<IEnumerable<TagResponse>> Pesquisar(string term, CancellationToken cancellationToken);
    }
}
