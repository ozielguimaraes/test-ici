namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class EditarNoticiaResponse : BaseResponse
    {
        public EditarNoticiaResponse(long noticiaId)
        {
            NoticiaId = noticiaId;
        }

        public long NoticiaId { get; private set; }
    }
}
