using TesteICI.Domain.Business.Responses.Noticia;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TesteICI.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class NoticiaController : BaseController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly INoticiaBusiness _noticiaBusiness;

        public NoticiaController(
            ILogger<NoticiaController> logger, 
            IHttpContextAccessor httpContextAccessor, 
            INoticiaBusiness noticiaBusiness
            ) : base(logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            _noticiaBusiness = noticiaBusiness;
        }

        [HttpGet]
        [Route("{noticiaId:int}")]
        [ProducesResponseType(typeof(NoticiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int noticiaId)
        {
            try
            {
                Logger.LogInformation($"Method: {nameof(Get)} - GET");
                Logger.LogInformation($"noticiaId: {noticiaId}");
                return ResultWhenSearching(await _noticiaBusiness.GetById(noticiaId));
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
        public async Task<IActionResult> Create([FromBody] CreateNoticiaRequest request)
        {
            try
            {
                Logger.LogInformation($"Method: {nameof(Get)} - POST");
                return ResultWhenAdding(await _noticiaBusiness.Create(request));
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
        public async Task<IActionResult> Update([FromBody] UpdateNoticiaRequest request)
        {
            try
            {
                Logger.LogInformation($"Method: {nameof(Get)} - PUT");
                return ResultWhenUpdating(await _noticiaBusiness.Update(request));
            }
            catch (Exception ex)
            {
                var message = "Error to update Noticia";
                Logger.LogError(ex, message);
                return InternalServerError(ex, message);
            }
        }
    }
}

