using Microsoft.OpenApi.Models;
using TesteICI.Infra.CrossCutting.IoC;
using TesteICI.Infra.CrossCutting.Security.Builders;
using TesteICI.Infra.CrossCutting.Security.Extensions;
using TesteICI.Services.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDependencyInjection(builder.Configuration);

builder.Services.AddAuthenticationApiConfig(builder.Configuration);

builder.Services.AddApiConfig();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Teste ICI",
        Description = "Projeto de teste ICI",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt")
        }
    });
});

// Configure JSON logging to the console.
builder.Logging.AddJsonConsole();

// Configure routing to use lowercase URLs
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseCors("Production");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());

app.MapControllers();

app.Run();
