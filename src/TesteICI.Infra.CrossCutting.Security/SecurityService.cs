using Microsoft.AspNetCore.Identity;

namespace TesteICI.Infra.CrossCutting.Security;

public sealed class SecurityService : ISecurityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public SecurityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser> ObterPorEmailAsync(string email)
    {
        ArgumentNullException.ThrowIfNull(email);

        var usuario = await _userManager.FindByEmailAsync(email);
        ArgumentNullException.ThrowIfNull(usuario);

        return usuario;
    }

    public async Task<IdentityUser?> ObterPorIdAsync(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var usuario = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(usuario);

        return usuario;
    }

    public async Task<bool> SenhaEstaValidaAsync(IdentityUser usuarioIdentity, string senha)
    {
        ArgumentNullException.ThrowIfNull(usuarioIdentity);
        ArgumentNullException.ThrowIfNull(senha);

        return await _userManager.CheckPasswordAsync(usuarioIdentity, senha);
    }
}
