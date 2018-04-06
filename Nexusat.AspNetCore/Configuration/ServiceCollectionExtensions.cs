using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NexusatAspNetCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddNexusatAspNetCore(this IServiceCollection services, Action<NexusatAspNetCoreOptionsBuilder> setupAction)
        {
            

            // Add framework services
            services.AddSingleton<IApiResponseBuilderFactory>(_ => new ApiResponseBuilderFactory());
            services.AddSingleton<ApiResponseExecutor>();

            // Add framework configuration IOptions
            services.Configure<NexusatAspNetCoreOptions>(options => {
                // Build Frameowrk configuration options
                var setupBuilder = new NexusatAspNetCoreOptionsBuilder(options);
                setupAction(setupBuilder);
            });

            return services;
        }

    }
   
    
}
