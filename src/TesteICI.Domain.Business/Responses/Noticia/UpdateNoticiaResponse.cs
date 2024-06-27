namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class UpdateNoticiaResponse : BaseResponse
    {
        public UpdateNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
