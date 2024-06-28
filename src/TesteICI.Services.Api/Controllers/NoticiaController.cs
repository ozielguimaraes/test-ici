using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Services.Api.Controllers;

[Route("api/[controller]")]
public class NoticiaController : BaseController
{
    private readonly INoticiaBusiness _noticiaBusiness;

    public NoticiaController(ILogger<NoticiaController> logger, INoticiaBusiness noticiaBusiness) : base(logger)
    {
        _noticiaBusiness = noticiaBusiness;
    }

    [HttpGet]
    [Route("{noticiaId:int}")]
    [ProducesResponseType(typeof(NoticiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int noticiaId, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Get)} - GET");
            Logger.LogInformation($"noticiaId: {noticiaId}");
            return ResultadoQuandoPesquisando(await _noticiaBusiness.ObterPorId(noticiaId, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = $"Error to get Noticia by id: {noticiaId}";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(NoticiaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarNoticiaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Adicionar)} - POST");
            return ResultadoQuandoAdicionando(await _noticiaBusiness.Adicionar(request, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Error to add new Noticia";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(typeof(NoticiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Editar([FromBody] EditarNoticiaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Editar)} - PUT");
            return ResultadoQuandoEditando(await _noticiaBusiness.Editar(request, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Error to update Noticia";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpDelete]
    [Route("{noticiaId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deletar([FromRoute] long noticiaId, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Deletar)} - DELETE");
            return ResultadoQuandoRemovendo(await _noticiaBusiness.Deletar(noticiaId, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Erro para deletar Noticia";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }
}
