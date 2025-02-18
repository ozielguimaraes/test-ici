using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Responses;

namespace TesteICI.Services.Api.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger Logger;

    protected BaseController(ILogger<BaseController> logger)
    {
        Logger = logger;
    }

    protected ObjectResult ResultadoQuandoAdicionando(BaseResponse response)
    {
        if (response.IsValid())
        {
            Logger.LogInformation($"item added: {response}");
            return StatusCode(StatusCodes.Status201Created, response);
        }

        return BadRequest(response.GetValidationFailures());
    }

    protected ObjectResult ResultadoQuandoEditando(BaseResponse response)
    {
        if (response.IsValid())
        {
            Logger.LogInformation($"item updated: {response}");
            return StatusCode(StatusCodes.Status200OK, response);
        }

        return BadRequest(response.GetValidationFailures());
    }

    protected ObjectResult ResultadoQuandoRemovendo(BaseResponse response)
    {
        if (response is NullResponse)
        {
            Logger.LogInformation("item not found");
            return StatusCode(StatusCodes.Status404NotFound, response);
        }

        if (response.IsValid())
        {
            Logger.LogInformation("item deletado");
            return StatusCode(StatusCodes.Status204NoContent, response);
        }

        return BadRequest(response.GetValidationFailures());
    }

    protected ObjectResult ResultadoQuandoPesquisando(BaseResponse response)
    {
        if (response is NullResponse)
        {
            Logger.LogInformation("item not found");
            return StatusCode(StatusCodes.Status404NotFound, response);
        }

        return Ok(response);
    }

    protected ObjectResult ResultWhenSearching(IEnumerable<BaseResponse> response)
    {
        return Ok(response);
    }

    protected IActionResult ResultWhenAdding(IdentityResult result, IdentityUser identityUser)
    {
        if (result.Succeeded)
        {
            Logger.LogInformation("user added");
            return StatusCode(StatusCodes.Status201Created, new
            {
                id = identityUser.Id,
                userName = identityUser.UserName
            });
        }

        return BadRequest(result.Errors);
    }

    protected IActionResult ResultWhenSignIn(EfetuarLoginRequest request)
    {
        Logger.LogInformation($"Usuário ou senha inválida {request.Login}");
        return BadRequest("Usuário ou senha inválida");
    }

    protected IActionResult ResultWhenSignIn(Microsoft.AspNetCore.Identity.SignInResult result, EfetuarLoginRequest request)
    {
        if (result.Succeeded)
        {
            Logger.LogInformation($"user signin: {request.Login}");
            return StatusCode(StatusCodes.Status200OK);
        }
        if (result.IsLockedOut)
        {
            Logger.LogInformation("user exceeded tentative limit");
            return StatusCode(StatusCodes.Status401Unauthorized, "User exceeded tentative limit");
        }
        if (result.IsNotAllowed)
        {
            Logger.LogInformation("user is not allowed");
            return StatusCode(StatusCodes.Status401Unauthorized, "User is not allowed");
        }
        if (result.RequiresTwoFactor)
        {
            Logger.LogInformation("user requires two factor");
            return StatusCode(StatusCodes.Status401Unauthorized, "User requires two factor");
        }
        return BadRequest("Usuário ou senha inválida");
    }

    protected ObjectResult InternalServerError(Exception exception, string message)
    {
        Logger.LogError(exception, message);
        return StatusCode(StatusCodes.Status500InternalServerError, message);
    }
}
