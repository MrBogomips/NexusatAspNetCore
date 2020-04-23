using Microsoft.AspNetCore.Builder;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Middleware;
using System;

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
			
			app.UseMiddleware<CustomStatusCodesMiddleware>();
			app.UseMiddleware<UnhandledExceptionMiddleware>();

            return app;
        }
    }
   
    
}
