
namespace TesteICI.Domain.Entities;

public class Noticia
{
    public Noticia(string titulo, string texto, Guid usuarioId)
    {
        Titulo = titulo;
        Texto = texto;
        UsuarioId = usuarioId;
        Tags = new List<NoticiaTag>();
    }

    public Noticia(long noticiaId, string titulo, string texto, Guid usuarioId)
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
    public Guid UsuarioId { get; private set; }

    public ICollection<NoticiaTag> Tags { get; private set; }

    public void InformarTags(List<long> tagIds)
    {
        Tags = tagIds.Select(tagId => new NoticiaTag { TagId = tagId }).ToList();
    }

    internal void Update(Noticia noticiaUpdated)
    {
        Titulo = noticiaUpdated.Titulo;
        Texto = noticiaUpdated.Texto;
        UsuarioId = noticiaUpdated.UsuarioId;

        var tagsParaAdicionar = noticiaUpdated.Tags.Where(tag => Tags.All(t => t.TagId != tag.TagId)).ToList();
        var tagsParaRemover = Tags.Where(tag => noticiaUpdated.Tags.All(t => t.TagId != tag.TagId)).ToList();

        foreach (var tag in tagsParaAdicionar)
            Tags.Add(tag);

        foreach (var tag in tagsParaRemover)
            Tags.Remove(tag);
    }
}
