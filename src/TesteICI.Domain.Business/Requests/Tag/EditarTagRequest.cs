namespace TesteICI.Domain.Business.Requests.Tag;

public sealed class EditarTagRequest
{
    public long TagId { get; set; }
    public string Descricao { get; set; } = string.Empty;
}
