using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Auth;

public class SeCadastrarResponse : BaseResponse
{
    public SeCadastrarResponse(ValidationResult validationResult) : base(validationResult) { }

    public SeCadastrarResponse(long usuarioId)
    {
        UsuarioId = usuarioId;
    }

    public long UsuarioId { get; private set; }
}
