using TesteICI.Domain.Business.Requests.Usuario;
using TesteICI.Domain.Business.Responses.Usuario;

namespace TesteICI.Domain.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<CreateUsuarioResponse> Create(CreateUsuarioRequest request);
    Task<UpdateUsuarioResponse> Update(UpdateUsuarioRequest request);
    Task<UsuarioResponse> GetById(long usuarioId);
    Task<bool> EmailEstaEmUso(string email, CancellationToken cancellationToken);
}
