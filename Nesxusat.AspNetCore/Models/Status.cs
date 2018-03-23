using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Nesxusat.AspNetCore.Models.ValidationErrorInfo;

namespace Nesxusat.AspNetCore.Models
{
    public sealed class Status
    {
        int HttpCode { get; set; }
        string Code { get; set; }
        string Description { get; set; }
        string UserDescription { get; set; }

        
    }
}
