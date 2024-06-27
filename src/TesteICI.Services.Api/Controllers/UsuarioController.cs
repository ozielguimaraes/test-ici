using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Security.Jwt.Core.Interfaces;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Usuario;
using TesteICI.Domain.Business.Responses.Usuario;

namespace TesteICI.Services.Api.Controllers;

[Route("api/[controller]")]
public class UsuarioController : BaseController
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IUsuarioBusiness _usuarioBusiness;

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public UsuarioController(ILogger<BaseController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration, IJwtService jwtService) : base(logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("signup")]
    [ProducesResponseType(typeof(AdicionarUsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup(AdicionarUsuarioRequest request)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Signup)} - POST");

            var response = await _usuarioBusiness.Create(request);
            if (response.IsValid())
            {
                var identityUser = new IdentityUser
                {
                    UserName = request.Nome,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(identityUser, request.Senha);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser, false);
                }

                return ResultWhenAdding(response);
            }

            return ResultWhenAdding(response);
        }
        catch (Exception ex)
        {
            var message = "Error to signup";
            Logger.LogError(ex, message);
            return InternalServerError(ex, message);
        }
    }
}
