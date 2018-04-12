using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Configuration
{
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
        /// Set the default status for Ok responses when not specifically supplied by the user
        /// </summary>
        /// <param name="statusCode"></param>
        public void SetDefaultOkStatus(string statusCode)
        {
            StatusCode.CheckValidCodeOrThrow(statusCode);
            Options.DefaultOkStatusCode = statusCode;
        }
        /// <summary>
        /// Set the default status for Ko responses when not specifically supplied by the user
        /// </summary>
        /// <param name="statusCode"></param>
        public void SetDefaultKoStatus(string statusCode)
        {
            StatusCode.CheckValidCodeOrThrow(statusCode);
            Options.DefaultKoStatusCode = statusCode;
        }
    }
}
