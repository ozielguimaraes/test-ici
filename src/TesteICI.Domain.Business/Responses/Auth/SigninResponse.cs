using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Auth;

public class SigninResponse : BaseResponse
{
    public SigninResponse(ValidationResult validationResult) : base(validationResult) { }

    public SigninResponse(string token, string nome, DateTime expiracaoEmUtc)
    {
        Token = token;
        Nome = nome;
        ExpiracaoEmUtc = expiracaoEmUtc;
    }

    public string Token { get; private set; }
    public string Nome { get; private set; }
    public DateTime ExpiracaoEmUtc { get; private set; }
}
