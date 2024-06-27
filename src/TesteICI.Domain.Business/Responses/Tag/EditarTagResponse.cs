using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Tag
{
    public class EditarTagResponse : BaseResponse
    {
        public EditarTagResponse(ValidationResult validationResult) : base(validationResult) { }

        public EditarTagResponse(long tagId, string descricao)
        {
            TagId = tagId;
            Descricao = descricao;
        }

        public long TagId { get; private set; }
        public string Descricao { get; private set; }
    }
}
