namespace TesteICI.Domain.Business.Requests.Tag;

public sealed class UpdateTagRequest
{
    public long TagId { get; set; }
    public string Descricao { get; set; } = string.Empty;
}
