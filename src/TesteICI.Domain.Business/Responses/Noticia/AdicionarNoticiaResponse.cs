namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class AdicionarNoticiaResponse : BaseResponse
    {
        public AdicionarNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
