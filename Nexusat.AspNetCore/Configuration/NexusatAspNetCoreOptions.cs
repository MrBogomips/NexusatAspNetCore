using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Configuration
{
    public class NexusatAspNetCoreOptions
    {
        public string DefaultOkStatusCode { get; set; } = StatusCode.DEFAULT_OK_STATUS_CODE;
        public string DefaultKoStatusCode { get; set; } = StatusCode.DEFAULT_KO_STATUS_CODE;
        public bool IsRuntimeProfilationEnabled { get; set; } = false;
    }
}
