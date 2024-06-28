using TesteICI.Domain.Business.Responses;

namespace TesteICI.Domain.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<BaseResponse> ObterPorId(Guid usuarioId);
    Task<BaseResponse> ObterInformacoes();
    Task<bool> EmailExiste(string email, CancellationToken cancellationToken);
}
