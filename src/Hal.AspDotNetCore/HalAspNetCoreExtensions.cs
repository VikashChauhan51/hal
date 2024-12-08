using Hal.Core;
using Hal.Core.Converters;
using Hal.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hal.AspDotNetCore;
public static class HalAspNetCoreExtensions
{

    public static IServiceCollection AddHalSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.Configure<HalJsonSerializerSettings>(settings =>
        {
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.None;
            settings.Converters.Add(new LinkConverter());
            settings.Converters.Add(new HttpVerbsConverter());
            settings.Converters.Add(new ResourceConverter());
            settings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        });
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IHalService, HalService>();
        serviceCollection.AddScoped<IHalLinkGenerator, HalLinkGenerator>();
        serviceCollection.AddScoped<ISmartLinkBuilder, SmartLinkBuilder>();
        return serviceCollection;
    }
}
