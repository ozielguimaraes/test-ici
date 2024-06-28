using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Services.Api.Controllers;

[Route("api/[controller]")]
public class TagController : BaseController
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ITagBusiness _tagBusiness;

    public TagController(
        ILogger<TagController> logger,
        IHttpContextAccessor httpContextAccessor,
        ITagBusiness tagBusiness
        ) : base(logger)
    {
        this.httpContextAccessor = httpContextAccessor;
        _tagBusiness = tagBusiness;
    }

    [HttpGet]
    [Route("{tagId:int}")]
    [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int tagId, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Get)} - GET");
            Logger.LogInformation($"tagId: {tagId}");
            return ResultadoQuandoPesquisando(await _tagBusiness.ObterPorId(tagId, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = $"Error to get Tag by id: {tagId}";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Get)} - GET");
            return ResultWhenSearching(await _tagBusiness.GetAllAsync());
        }
        catch (Exception ex)
        {
            var message = "Error to get all Tags";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(TagResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarTagRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Adicionar)} - POST");
            return ResultadoQuandoAdicionando(await _tagBusiness.Adicionar(request, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Error to add new Tag";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Editar([FromBody] EditarTagRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Editar)} - PUT");
            return ResultadoQuandoEditando(await _tagBusiness.Editar(request, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Error to update Tag";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }

    [HttpDelete]
    [Route("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deletar([FromRoute] long tagId, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Deletar)} - DELETE");
            return ResultadoQuandoRemovendo(await _tagBusiness.Deletar(tagId, cancellationToken));
        }
        catch (Exception ex)
        {
            var message = "Erro para deletar Tag";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }
}

