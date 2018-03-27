using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Models;
using System;

namespace Nexusat.AspNetCore.Configuration
{
    /// <summary>
    /// Nexusat ASP net core options builder.
    /// This is the class by which client code configures the system.
    /// </summary>
    public class NexusatAspNetCoreOptionsBuilder : IBuilder
    {
        private readonly NexusatAspNetCoreOptions Options;

        public NexusatAspNetCoreOptionsBuilder(NexusatAspNetCoreOptions options) =>
            Options = options ?? throw new ArgumentNullException(nameof(options));

        public void EnableRuntimeProfilation()
        {
            Options.IsRuntimeProfilationEnabled = true;
        }
        /// <summary>
        /// Sets the default ok status.
        /// <see cref="NexusatAspNetCoreOptions.DefaultOkStatusCode"/> for details
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        public void SetDefaultOkStatus(string statusCode)
        {
            StatusCode.CheckValidCodeOrThrow(statusCode);
            Options.DefaultOkStatusCode = statusCode;
        }
        /// <summary>
        /// Sets the default ko status.
        /// <see cref="NexusatAspNetCoreOptions.DefaultKoStatusCode"/> for details.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        public void SetDefaultKoStatus(string statusCode)
        {
            StatusCode.CheckValidCodeOrThrow(statusCode);
            Options.DefaultKoStatusCode = statusCode;
        }
        /// <summary>
        /// Sets the default unset status code.
        /// <see cref="NexusatAspNetCoreOptions.DefaultUnsetStatusCode"/> for details.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        public void SetDefaultUnsetStatusCode(string statusCode)
        {
            StatusCode.CheckValidCodeOrThrow(statusCode);
            Options.DefaultUnsetStatusCode = statusCode;
        }
    }
}
