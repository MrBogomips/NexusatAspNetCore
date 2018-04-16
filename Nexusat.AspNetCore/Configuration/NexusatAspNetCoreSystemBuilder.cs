using System;
using Microsoft.Extensions.DependencyInjection;
using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Mvc.Formatters;

namespace Nexusat.AspNetCore.Configuration
{
    internal class NexusatAspNetCoreSystemBuilder
    {
        private IMvcBuilder MvcBuilder { get; }
        private NexusatAspNetCoreOptions Options { get; }
        private IServiceCollection Services { get; }


        public NexusatAspNetCoreSystemBuilder(IMvcBuilder mvcBuilder, Action<NexusatAspNetCoreOptionsBuilder> setupAction)
        {
            MvcBuilder = mvcBuilder;
            Services = mvcBuilder.Services;

            // Add framework configuration IOptions
            Services.Configure<NexusatAspNetCoreOptions>(options => {
                // Build Frameowrk configuration options
                var setupBuilder = new NexusatAspNetCoreOptionsBuilder(options);
                setupAction(setupBuilder);
                setupBuilder.ValidateOptions(); // possibly throws an exception
            });
        }

        public NexusatAspNetCoreSystemBuilder AddSystemService()
        {
            Services.AddSingleton<NexusatAspNetCoreSystem>();
            return this;
        }

        public NexusatAspNetCoreSystemBuilder AddFrameworkServices()
        {
            Services.AddSingleton<IApiResponseBuilderFactory>(_ => new ApiResponseBuilderFactory());
            Services.AddSingleton<ApiResponseExecutor>();
            return this;
        }
    }
}
