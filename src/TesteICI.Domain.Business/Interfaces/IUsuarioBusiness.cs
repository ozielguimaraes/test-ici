using TesteICI.Domain.Business.Requests.Usuario;
using TesteICI.Domain.Business.Responses.Usuario;

namespace TesteICI.Domain.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<AdicionarUsuarioResponse> Create(AdicionarUsuarioRequest request);
    Task<EditarUsuarioResponse> Update(EditarUsuarioRequest request);
    Task<UsuarioResponse> GetById(long usuarioId);
    Task<bool> EmailEstaEmUso(string email, CancellationToken cancellationToken);
    Task<UsuarioResponse?> ObterPorEmail(string login, CancellationToken cancellationToken);
}
