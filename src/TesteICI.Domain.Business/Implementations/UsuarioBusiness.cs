using Microsoft.AspNetCore.Identity;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Usuario;
using TesteICI.Infra.CrossCutting.Security;
using TesteICI.Infra.CrossCutting.Security.Shared;

namespace TesteICI.Domain.Business.Implementations;

public class UsuarioBusiness : IUsuarioBusiness
{
    private readonly ISecurityService _securityService;
    private readonly IUser _user;

    public UsuarioBusiness(IUser user, ISecurityService securityService)
    {
        _user = user;
        _securityService = securityService;
    }

    public async Task<BaseResponse> ObterPorId(Guid usuarioId)
    {
        var usuario = await _securityService.ObterPorIdAsync(usuarioId);

        if (usuario is null)
            return new NullResponse();

        return MapearParaResponse(usuario);
    }

    public async Task<BaseResponse> ObterInformacoes()
    {
        var usuario = await _securityService.ObterPorIdAsync(_user.GetUserId());

        if (usuario is null)
            return new NullResponse();

        return MapearParaResponse(usuario);
    }

    public async Task<bool> EmailExiste(string email, CancellationToken cancellationToken)
    {
        return await _securityService.EmailExiste(email);
    }

    private UsuarioResponse MapearParaResponse(IdentityUser user)
    {
        return new UsuarioResponse(Guid.Parse(user.Id), user.UserName!, user.Email!);
    }
}
