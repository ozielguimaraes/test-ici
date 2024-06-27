using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Usuario;
using TesteICI.Domain.Business.Responses.Usuario;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Implementations;

public class UsuarioBusiness : IUsuarioBusiness
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioBusiness(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<AdicionarUsuarioResponse> Create(AdicionarUsuarioRequest request)
    {
        //TODO Validar com fluent validation

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new NullReferenceException("Nome é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Senha))
            throw new NullReferenceException("Senha é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new NullReferenceException("Email é obrigatório");

        var usuario = new Usuario(request.Nome, request.Email, request.Senha);

        var result = await _usuarioService.Add(usuario);

        return new AdicionarUsuarioResponse(result.UsuarioId);
    }

    public async Task<EditarUsuarioResponse> Update(EditarUsuarioRequest request)
    {
        //TODO Validar, usando FluentValidations
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new NullReferenceException("Nome é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Senha))
            throw new NullReferenceException("Senha é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new NullReferenceException("Email é obrigatório");

        var usuario = new Usuario();
        var result = await _usuarioService.Update(usuario);

        return new EditarUsuarioResponse(result.UsuarioId);
    }

    public async Task<List<UsuarioResponse>> GetAll()
    {
        var result = _usuarioService.All();
        return await Task.FromResult(result.Select(x => MapearParaResponse(x)).ToList());
    }

    public async Task<UsuarioResponse> GetById(long usuarioId)
    {
        var result = await _usuarioService.GetById(usuarioId);

        return MapearParaResponse(result);
    }

    public async Task<bool> EmailEstaEmUso(string email, CancellationToken cancellationToken)
    {
        return await _usuarioService.EmailExiste(email, cancellationToken);
    }

    public async Task<UsuarioResponse?> ObterPorEmail(string login, CancellationToken cancellationToken)
    {
        var resultado = await _usuarioService.Filter(x => x.Email == login, cancellationToken);

        var usuario = resultado.FirstOrDefault();
        if (usuario is null)
            return null;

        return MapearParaResponse(usuario);
    }

    private static UsuarioResponse MapearParaResponse(Usuario result) => new UsuarioResponse(result.UsuarioId, result.Nome, result.Email, result.Senha);
}
