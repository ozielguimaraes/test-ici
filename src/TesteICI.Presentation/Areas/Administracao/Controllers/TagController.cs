using Microsoft.AspNetCore.Mvc;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Tag;

namespace TesteICI.Presentation.Areas.Administracao.Controllers;

[Route("administracao/tag")]
[Area("administracao")]
public class TagController : BaseController
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagBusiness _tagBusiness;

    public TagController(ITagBusiness tagBusiness, ILogger<TagController> logger)
    {
        _tagBusiness = tagBusiness;
        _logger = logger;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de tags.");
        var tags = await _tagBusiness.ObterTodas(cancellationToken);
        return View(tags);
    }

    [Route("adicionar")]
    [HttpGet]
    public IActionResult Adicionar()
    {
        _logger.LogInformation("Acessou a página de adicionar tag.");
        return View();
    }

    [Route("adicionar")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar([FromForm] AdicionarTagRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de adicionar tag.");
        var tag = await _tagBusiness.Adicionar(request, cancellationToken);

        if (tag.IsValid())
            return RedirectToAction("Index");
        else
            return View(request);
    }

    [Route("editar/{tagId:long}")]
    public async Task<IActionResult> Editar([FromRoute] long tagId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de editar tag.");

        var resultado = await _tagBusiness.ObterPorId(tagId, cancellationToken);

        if (resultado is null || resultado is NullResponse)
            return NotFound();

        var tag = (TagResponse)resultado;

        return View(new EditarTagRequest
        {
            Descricao = tag.Descricao,
            TagId = tag.TagId
        });
    }

    [Route("editar/{tagId:long}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar([FromForm] EditarTagRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de editar tag.");
        var tag = await _tagBusiness.Editar(request, cancellationToken);

        if (tag.IsValid())
            return RedirectToAction("Index");
        else
            return View(request);
    }

    [Route("excluir/{tagId:long}")]
    public async Task<IActionResult> Excluir([FromRoute] long tagId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de excluir tag.");

        var resultado = await _tagBusiness.ObterPorId(tagId, cancellationToken);

        if (resultado is null || resultado is NullResponse)
            return NotFound();

        var tag = (TagResponse)resultado;

        return View(tag);
    }

    [Route("excluir/{tagId:long}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmacao([FromRoute] long tagId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou a página de excluir tag.");
        var tag = await _tagBusiness.Deletar(tagId, cancellationToken);

        if (tag is NullResponse)
            return NotFound();

        if (tag.IsValid())
            return RedirectToAction(nameof(Index));
        else
            return RedirectToAction(nameof(Excluir), new { tagId });
    }

    [HttpGet]
    [Route("pesquisar/json")]
    public async Task<IActionResult> Pesquisar([FromQuery] string term, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acessou endpoint de tags.");
        var tags = await _tagBusiness.Pesquisar(term, cancellationToken);
        return Ok(tags);
    }
}
