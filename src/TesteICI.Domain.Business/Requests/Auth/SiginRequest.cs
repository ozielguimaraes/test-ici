namespace TesteICI.Domain.Business.Requests.Auth;

public sealed class SiginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
