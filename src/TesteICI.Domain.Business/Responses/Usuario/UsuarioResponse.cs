namespace TesteICI.Domain.Business.Responses.Usuario;

public class UsuarioResponse : BaseResponse
{
    public UsuarioResponse(Guid usuarioId, string nome, string email)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
    }

    public Guid UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
}
