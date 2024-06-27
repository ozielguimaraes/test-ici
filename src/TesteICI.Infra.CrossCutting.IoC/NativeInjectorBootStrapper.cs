using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteICI.Domain.Business.Implementations;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Auth;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Requests.Tag;
using TesteICI.Domain.Business.Validations.Auth;
using TesteICI.Domain.Business.Validations.Noticia;
using TesteICI.Domain.Business.Validations.Tag;
using TesteICI.Domain.Interfaces;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Domain.Interfaces.Services;
using TesteICI.Domain.Services;
using TesteICI.Infra.CrossCutting.Security;
using TesteICI.Infra.CrossCutting.Security.Shared;
using TesteICI.Infra.Data.Context;
using TesteICI.Infra.Data.Repositories;
using TesteICI.Infra.Data.UoW;

namespace TesteICI.Infra.CrossCutting.IoC;

public static class NativeInjectorBootStrapper
{
    public static void RegisterDependencyInjection(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);

        // ASP.NET HttpContext dependency
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IUser, AspNetUser>();

        // Domain.Business
        services.AddScoped<IAuthBusiness, AuthBusiness>();
        services.AddScoped<ITagBusiness, TagBusiness>();
        services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();

        // Domain
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<INoticiaService, NoticiaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        // Infra - Data
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<INoticiaRepository, NoticiaRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<MainContext>();

        // Registrar FluentValidation
        services.AddScoped<IValidator<EfetuarLoginRequest>, EfetuarLoginRequestValidator>();
        services.AddScoped<IValidator<SeCadastrarRequest>, SeCadastrarRequestValidator>();

        services.AddScoped<IValidator<EditarTagRequest>, EditarTagRequestValidator>();
        services.AddScoped<IValidator<AdicionarTagRequest>, AdicionarTagRequestValidator>();

        services.AddScoped<IValidator<EditarNoticiaRequest>, EditarNoticiaRequestValidator>();
        services.AddScoped<IValidator<AdicionarNoticiaRequest>, AdicionarNoticiaRequestValidator>();
    }
}
