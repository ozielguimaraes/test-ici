using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Auth;

public class EfetuarLoginResponse : BaseResponse
{
    public EfetuarLoginResponse(ValidationResult validationResult) : base(validationResult) { }

    public EfetuarLoginResponse(string token, string nome, DateTime expiracaoEmUtc)
    {
        Token = token;
        Nome = nome;
        ExpiracaoEmUtc = expiracaoEmUtc;
    }

    public string Token { get; private set; }
    public string Nome { get; private set; }
    public DateTime ExpiracaoEmUtc { get; private set; }
}
