namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class CreateNoticiaResponse : BaseResponse
    {
        public CreateNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
