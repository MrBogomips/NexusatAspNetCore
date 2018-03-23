using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Models
{
    public sealed class Status
    {
        public int HttpCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UserDescription { get; set; }
    }
}
