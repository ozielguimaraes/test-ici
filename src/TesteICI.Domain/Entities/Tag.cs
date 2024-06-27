namespace TesteICI.Domain.Entities;

public class Tag
{
    public Tag(string descricao) => Descricao = descricao;

    public Tag(long tagId, string descricao)
    {
        TagId = tagId;
        Descricao = descricao;
    }

    public long TagId { get; set; }
    public string Descricao { get; private set; } = string.Empty;

    public void Update(Tag tagUpdated)
    {
        Descricao = tagUpdated.Descricao;
    }
}
