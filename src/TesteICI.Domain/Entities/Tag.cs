namespace TesteICI.Domain.Entities;

public class Tag
{
    public Tag(string descricao)
    {
        Descricao = descricao;
        Tags = new List<NoticiaTag>();
    }

    public Tag(long tagId, string descricao)
    {
        TagId = tagId;
        Descricao = descricao;
        Tags = new List<NoticiaTag>();
    }

    public long TagId { get; set; }
    public string Descricao { get; private set; } = string.Empty;
    public ICollection<NoticiaTag> Tags { get; private set; }

    public void Update(Tag tagUpdated)
    {
        Descricao = tagUpdated.Descricao;
    }
}
