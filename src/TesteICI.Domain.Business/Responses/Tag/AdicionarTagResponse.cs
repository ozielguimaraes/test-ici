using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Tag
{
    public class AdicionarTagResponse : BaseResponse
    {
        public AdicionarTagResponse(ValidationResult validationResult) : base(validationResult) { }

        public AdicionarTagResponse(long tagId)
        {
            TagId = tagId;
        }

        public long TagId { get; private set; }
    }
}
