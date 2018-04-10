using Microsoft.AspNetCore.Builder;
using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Middleware;
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
        /// Uses the nexusat ASP net core.
        /// </summary>
        /// <returns>The nexusat ASP net core.</returns>
        /// <param name="app">App.</param>
        public static IApplicationBuilder UseNexusatAspNetCore(this IApplicationBuilder app)
        {
            // Manage HTTP 404 and other MVC status codes not managed
            app.UseStatusCodePages(new InternalStatusCodePagesOptions());

            return app;
        }
        /// <summary>
        /// Register a middleware to manage exception.
        /// Register this middleware BEFORE Mvc for a better support.
        /// </summary>
        /// <returns>The nexusat ASP net core exception handling.</returns>
        /// <param name="app">App.</param>
        public static IApplicationBuilder UseNexusatAspNetCoreExceptionHandling(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
   
    
}
