using Microsoft.AspNetCore.Identity;

namespace TesteICI.Infra.CrossCutting.Security;
public interface ISecurityService
{
    Task<IdentityUser> ObterPorEmailAsync(string email, CancellationToken cancellationToken);
    Task<IdentityUser?> ObterPorIdAsync(Guid userId);
    Task<bool> SenhaEstaValidaAsync(IdentityUser usuarioIdentity, string senha);

    Task<bool> EmailExiste(string email);
}
