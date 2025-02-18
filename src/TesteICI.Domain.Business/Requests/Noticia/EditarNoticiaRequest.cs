namespace TesteICI.Domain.Business.Requests.Noticia;

public sealed class EditarNoticiaRequest
{
    public long NoticiaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
    public List<long> TagIds { get; set; } = new List<long>();
}
