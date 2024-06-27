using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Auth;

public class SignupResponse : BaseResponse
{
    public SignupResponse(ValidationResult validationResult) : base(validationResult) { }

    public SignupResponse(long usuarioId)
    {
        UsuarioId = usuarioId;
    }

    public long UsuarioId { get; private set; }
}
