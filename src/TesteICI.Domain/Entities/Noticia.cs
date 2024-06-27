
namespace TesteICI.Domain.Entities;

public class Noticia
{
    public Noticia(string titulo, string texto, long usuarioId)
    {
        Titulo = titulo;
        Texto = texto;
        UsuarioId = usuarioId;
        Tags = new List<NoticiaTag>();
    }

    public Noticia(long noticiaId, string titulo, string texto, long usuarioId)
    {
        NoticiaId = noticiaId;
        Titulo = titulo;
        Texto = texto;
        UsuarioId = usuarioId;
        Tags = new List<NoticiaTag>();
    }

    public long NoticiaId { get; set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Texto { get; private set; } = string.Empty;
    public long UsuarioId { get; private set; }

    public ICollection<NoticiaTag> Tags { get; private set; }

    internal void Update(Noticia noticiaUpdated)
    {
        Titulo = noticiaUpdated.Titulo;
        Texto = noticiaUpdated.Texto;
        UsuarioId = noticiaUpdated.UsuarioId;
        Tags = noticiaUpdated.Tags;
    }
}
