using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace TesteICI.Presentation.Configurations;

internal static class GlobalizationConfiguration
{
    internal static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
    {
        var defaultCulture = new CultureInfo("pt-BR");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };

        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.CurrentCulture = defaultCulture;
        CultureInfo.CurrentUICulture = defaultCulture;

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
