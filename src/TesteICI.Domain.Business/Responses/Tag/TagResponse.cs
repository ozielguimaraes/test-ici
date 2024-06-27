using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Tag;

public class TagResponse : BaseResponse
{
    public TagResponse(ValidationResult validationResult) : base(validationResult)
    {

    }
    public TagResponse(Entities.Tag tag)
    {
        TagId = tag.TagId;
        Descricao = tag.Descricao;
    }

    public long TagId { get; private set; }
    public string Descricao { get; private set; }

}
