using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Domain.Business.Responses.Noticia
{
    public class NoticiaResponse : BaseResponse
    {
        public NoticiaResponse(long noticiaId, string titulo, string texto, Guid usuarioId, ICollection<NoticiaTagResponse> tags)
        {
            NoticiaId = noticiaId;
            Titulo = titulo;
            Texto = texto;
            UsuarioId = usuarioId;
            Tags = tags;
        }

        public long NoticiaId { get; private set; }
        public string Titulo { get; private set; }
        public string Texto { get; private set; }
        public Guid UsuarioId { get; private set; }
        public ICollection<NoticiaTagResponse> Tags { get; private set; }
    }

    public class NoticiaTagResponse
    {
        public NoticiaTagResponse(long noticiaTagId, long tagId)
        {
            NoticiaTagId = noticiaTagId;
            TagId = tagId;
        }

        public NoticiaTagResponse(long noticiaTagId, long tagId, TagResponse? tag)
        {
            NoticiaTagId = noticiaTagId;
            TagId = tagId;
            Tag = tag;
        }

        public long NoticiaTagId { get; private set; }

        public long TagId { get; private set; }
        public TagResponse? Tag { get; private set; }
    }
}
