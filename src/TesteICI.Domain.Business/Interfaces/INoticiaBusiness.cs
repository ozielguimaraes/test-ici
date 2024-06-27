using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface INoticiaBusiness
    {
        Task<AdicionarNoticiaResponse> Create(AdicionarNoticiaRequest request);
        Task<EditarNoticiaResponse> Update(EditarNoticiaRequest request);
        Task<NoticiaResponse> GetById(long noticiaId);
    }
}
