namespace TesteICI.Domain.Business.Requests.Usuario;

public class EditarUsuarioRequest
{
    public long UsuarioId { get; set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
}
