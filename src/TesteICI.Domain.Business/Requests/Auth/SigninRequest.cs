namespace TesteICI.Domain.Business.Requests.Auth;

public sealed class SigninRequest
{
    public string Login { get; set; }
    public string Senha { get; set; }
}
