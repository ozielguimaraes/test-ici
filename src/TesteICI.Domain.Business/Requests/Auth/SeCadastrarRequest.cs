namespace TesteICI.Domain.Business.Requests.Auth;

public sealed class SeCadastrarRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
