namespace TesteICI.Domain.Business.Requests.Noticia;

public sealed class AdicionarNoticiaRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public long UsuarioId { get; set; }
    public List<long> TagIds { get; set; } = new List<long>();
}
