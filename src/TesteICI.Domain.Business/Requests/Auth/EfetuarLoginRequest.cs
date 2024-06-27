namespace TesteICI.Domain.Business.Requests.Auth;

public sealed class EfetuarLoginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
