using System;
using Microsoft.AspNetCore.Builder;
using TesteICI.Infra.CrossCutting.Security.Builders;
using TesteICI.Infra.CrossCutting.Security.Policies;

namespace TesteICI.Infra.CrossCutting.Security.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
        {
            SecurityHeadersPolicy policy = builder.Build();
            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }
    }
}
