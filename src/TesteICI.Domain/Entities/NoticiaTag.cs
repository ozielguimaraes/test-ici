namespace TesteICI.Domain.Entities;

public class NoticiaTag
{
    public long NoticiaTagId { get; set; }

    public long NoticiaId { get; set; }
    public Noticia? Noticia { get; set; }

    public long TagId { get; set; }
    public Tag? Tag { get; set; }
}
