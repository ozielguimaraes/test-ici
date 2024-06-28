namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class NoticiaResponse : BaseResponse
    {
        public NoticiaResponse(long noticiaId, string titulo, string texto, Guid usuarioId)
        {
            NoticiaId = noticiaId;
            Titulo = titulo;
            Texto = texto;
            UsuarioId = usuarioId;
        }

        public long NoticiaId { get; private set; }
        public string Titulo { get; private set; }
        public string Texto { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
