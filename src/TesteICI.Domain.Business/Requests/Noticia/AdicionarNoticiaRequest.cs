using System.ComponentModel.DataAnnotations;

namespace TesteICI.Domain.Business.Requests.Noticia;

public sealed class AdicionarNoticiaRequest
{
    [Display(Name = "Título")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Length(5, 250, ErrorMessage = "O campo {0} deve ter entre {1} e {2} caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Display(Name = "Texto")]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MinLength(50, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
    public string Texto { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }

    [Display(Name = "Tags")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public List<long> TagIds { get; set; } = new List<long>();
}
