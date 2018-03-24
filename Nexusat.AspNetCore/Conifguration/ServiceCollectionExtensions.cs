using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Conifguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NexusatAspNetCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddNexusatAspNetCore(this IServiceCollection services, Action<NexusatAspNetCoreOptions> setupAction)
        {
            services.AddSingleton<IApiResponseBuilderFactory>(_ => new ApiResponseBuilderFactory());
            return services;
        }

    }
   
    
}
