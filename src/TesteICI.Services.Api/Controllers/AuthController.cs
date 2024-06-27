using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Responses.Auth;


namespace TesteICI.Services.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthBusiness _authBusiness;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public AuthController(ILogger<BaseController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAuthBusiness authBusiness, IConfiguration configuration, IJwtService jwtService) : base(logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _authBusiness = authBusiness;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("signup")]
    [ProducesResponseType(typeof(SeCadastrarResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup(SeCadastrarRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Signup)} - POST");

            var response = await _authBusiness.Validate(request, cancellationToken);

            if (response.IsValid())
            {
                var identityUser = new IdentityUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(identityUser, request.Senha);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser, false);
                }

                return ResultWhenAdding(result, identityUser);
            }

            return ResultadoQuandoAdicionando(response);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex, message: "Error to signup");
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("signin")]
    [ProducesResponseType(typeof(EfetuarLoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signin(EfetuarLoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation($"Method: {nameof(Signin)} - POST");

            var response = await _authBusiness.Validate(request, cancellationToken);

            if (response.IsValid())
            {
                var result = await _signInManager.PasswordSignInAsync(request.Login, request.Senha, isPersistent: false, lockoutOnFailure: true);

                return await ResultWhenSignIn(result, request, response);
            }

            return ResultWhenSignIn(request);
        }
        catch (Exception ex)
        {
            var message = "Error to signin";
            return InternalServerError(ex, message);
        }
    }

    private async Task<IActionResult> ResultWhenSignIn(Microsoft.AspNetCore.Identity.SignInResult result, EfetuarLoginRequest request, EfetuarLoginResponse response)
    {
        if (result.Succeeded)
        {
            Logger.LogInformation($"user signin: {request.Login}");
            var token = await GenerateAccessToken(request.Login);
            return StatusCode(StatusCodes.Status200OK, token);
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

        Logger.LogInformation("Username or password is incorrect");
        return BadRequest("Login ou senha incorreto");
    }

    private async Task<string> GenerateAccessToken(string userName)
    {
        var usuario = await _userManager.FindByNameAsync(userName);
        ArgumentNullException.ThrowIfNull(usuario);

        var userRoles = await _userManager.GetRolesAsync(usuario);
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(await _userManager.GetClaimsAsync(usuario));
        identityClaims.AddClaims(userRoles.Select(s => new Claim("role", s)));

        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, usuario.Email!));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName!));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        return BuildToken(identityClaims);
    }

    private string BuildToken(ClaimsIdentity identityClaims)
    {
        var settings = _configuration.GetSection("AppJwtSettings");
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = settings["SecretKey"];
        ArgumentNullException.ThrowIfNull(secret);

        var key = Encoding.ASCII.GetBytes(secret);
        var issuer = settings["Issuer"];
        var audience = settings["Audience"];

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(double.Parse(settings["ExpirationInSeconds"] ?? "3600")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}
