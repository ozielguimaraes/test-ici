using Microsoft.AspNetCore.Identity;

namespace TesteICI.Infra.CrossCutting.Security;
public interface ISecurityService
{
    Task<IdentityUser> ObterPorEmailAsync(string email);
    Task<bool> SenhaEstaValidaAsync(IdentityUser usuarioIdentity, string senha);
}
