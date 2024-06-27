using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class EditarNoticiaResponse : BaseResponse
    {
        public EditarNoticiaResponse(ValidationResult validationResult) : base(validationResult) { }

        public EditarNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
