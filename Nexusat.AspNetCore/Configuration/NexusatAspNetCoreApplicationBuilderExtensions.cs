using Microsoft.AspNetCore.Builder;
using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NexusatAspNetCoreApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the nexusat ASP net core to the MVC pipeline.
        /// </summary>
        /// <returns>The MVC builder</returns>
        /// <param name="mvcBuilder">Mvc builder.</param>
        /// <param name="setupAction">Setup action.</param>
        public static IApplicationBuilder UseNexusatAspNetCore(this IApplicationBuilder app)
        {
            // Manage HTTP 404 and other MVC status codes
            app.UseStatusCodePages(new InternalStatusCodePagesOptions());

            return app;
        }
    }
   
    
}
