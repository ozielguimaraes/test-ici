namespace TesteICI.Domain.Business.Requests.Noticia;

public sealed class CreateNoticiaRequest
{
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public long UsuarioId { get; set; }
}
