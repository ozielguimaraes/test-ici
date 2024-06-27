using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class AdicionarNoticiaResponse : BaseResponse
    {
        public AdicionarNoticiaResponse(ValidationResult validationResult) : base(validationResult) { }

        public AdicionarNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
