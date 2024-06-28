using System.ComponentModel.DataAnnotations;

namespace TesteICI.Domain.Business.Requests.Tag;

public sealed class AdicionarTagRequest
{
    [Display(Name = "Descrição da tag", Prompt = "Digite aqui a descrição")]
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [Length(3, 100, ErrorMessage = "A descrição deve ter entre {1} e {2} caracteres")]
    public string Descricao { get; set; } = string.Empty;
}
