using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Security.Jwt.Core.Interfaces;
using TesteICI.Domain.Business.Interfaces;

namespace TesteICI.Services.Api.Controllers;

[Route("api/[controller]")]
public class UsuarioController : BaseController
{
    private readonly IUsuarioBusiness _usuarioBusiness;

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public UsuarioController(ILogger<BaseController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration, IJwtService jwtService, IUsuarioBusiness usuarioBusiness) : base(logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _jwtService = jwtService;
        _usuarioBusiness = usuarioBusiness;
    }

    //[AllowAnonymous]
    //[HttpPost]
    //[Route("")]
    //[ProducesResponseType(typeof(AdicionarUsuarioResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> Adicionar(AdicionarUsuarioRequest request)
    //{
    //    try
    //    {
    //        Logger.LogInformation($"Method: {nameof(Adicionar)} - POST");

    //        var response = await _usuarioBusiness.Create(request);
    //        if (response.IsValid())
    //        {
    //            var identityUser = new IdentityUser
    //            {
    //                UserName = request.Nome,
    //                EmailConfirmed = true
    //            };
    //            var result = await _userManager.CreateAsync(identityUser, request.Senha);
    //            if (result.Succeeded)
    //            {
    //                await _signInManager.SignInAsync(identityUser, false);
    //            }

    //            return ResultadoQuandoAdicionando(response);
    //        }

    //        return ResultadoQuandoAdicionando(response);
    //    }
    //    catch (Exception ex)
    //    {
    //        var message = "Error to signup";
    //        Logger.LogError(ex, message);
    //        return InternalServerError(ex, message);
    //    }
    //}
}
