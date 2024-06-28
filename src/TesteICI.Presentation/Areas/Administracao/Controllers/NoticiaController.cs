using Microsoft.AspNetCore.Mvc;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;

namespace TesteICI.Presentation.Areas.Administracao.Controllers;

[Route("administracao/noticia")]
[Area("administracao")]
public class NoticiaController : BaseController
{
    private readonly ILogger<NoticiaController> _logger;
    private readonly INoticiaBusiness _noticiaBusiness;
    private readonly ITagBusiness _tagBusiness;

    public NoticiaController(INoticiaBusiness noticiaBusiness, ILogger<NoticiaController> logger, ITagBusiness tagBusiness)
    {
        _noticiaBusiness = noticiaBusiness;
        _logger = logger;
        _tagBusiness = tagBusiness;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de notícias.");
        var noticias = await _noticiaBusiness.ObterTodas(cancellationToken);
        return View(noticias);
    }

    [Route("adicionar")]
    [HttpGet]
    public IActionResult Adicionar()
    {
        _logger.LogInformation("Acessou a página de adicionar notícia.");

        return View();
    }

    [Route("adicionar")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarNoticiaRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de adicionar notícia.");
        var noticia = await _noticiaBusiness.Adicionar(request, cancellationToken);

        if (noticia.IsValid())
            return Created();
        else
            return BadRequest();
    }

    [Route("editar/{noticiaId:long}")]
    public async Task<IActionResult> Editar([FromRoute] long noticiaId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou o endpoint de editar notícia.");

        var noticia = await _noticiaBusiness.ObterPorId(noticiaId, cancellationToken);

        if (noticia is null || noticia is NullResponse)
            return NotFound();

        return View(noticia);
    }

    [Route("editar")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar([FromBody] EditarNoticiaRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou o endpoint de editar notícia.");
        var noticia = await _noticiaBusiness.Editar(request, cancellationToken);

        if (noticia.IsValid())
            return StatusCode(StatusCodes.Status200OK);
        else
            return BadRequest();
    }

    [Route("excluir/{noticiaId:long}")]
    public async Task<IActionResult> Excluir([FromRoute] long noticiaId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de excluir noticia.");

        var resultado = await _noticiaBusiness.ObterPorId(noticiaId, cancellationToken);

        if (resultado is null || resultado is NullResponse)
            return NotFound();

        var noticia = (NoticiaResponse)resultado;

        return View(noticia);
    }

    [Route("excluir/{noticiaId:long}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmacao([FromRoute] long noticiaId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de excluir noticia.");
        var noticia = await _noticiaBusiness.Deletar(noticiaId, cancellationToken);

        if (noticia is NullResponse)
            return NotFound();

        if (noticia.IsValid())
            return RedirectToAction(nameof(Index));
        else
            return RedirectToAction(nameof(Excluir), new { noticiaId });
    }
}
