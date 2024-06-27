using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Domain.Business.Interfaces
{
    public interface INoticiaBusiness
    {
        Task<CreateNoticiaResponse> Create(CreateNoticiaRequest request);
        Task<UpdateNoticiaResponse> Update(UpdateNoticiaRequest request);
        Task<NoticiaResponse> GetById(long noticiaId);
    }
}
