using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Domain.Business.Interfaces;

public interface INoticiaBusiness
{
    Task<AdicionarNoticiaResponse> Adicionar(AdicionarNoticiaRequest request, CancellationToken cancellationToken);
    Task<BaseResponse> Deletar(long noticiaId, CancellationToken cancellationToken);
    Task<EditarNoticiaResponse> Editar(EditarNoticiaRequest request, CancellationToken cancellationToken);
    Task<BaseResponse> ObterPorId(long noticiaId, CancellationToken cancellationToken);
    Task<IList<NoticiaResponse>> ObterTodas(CancellationToken cancellationToken);
}
