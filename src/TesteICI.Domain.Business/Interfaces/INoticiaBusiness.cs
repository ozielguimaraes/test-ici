using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface INoticiaBusiness
    {
        Task<AdicionarNoticiaResponse> Adicionar(AdicionarNoticiaRequest request);
        Task<EditarNoticiaResponse> Editar(EditarNoticiaRequest request);
        Task<BaseResponse> ObterPorId(long noticiaId);
    }
}
