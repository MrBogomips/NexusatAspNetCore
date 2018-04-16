using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Produces("application/json")]
    [Route("Pagination")]
    public class PaginationController : ApiController
    {
        List<string> Data = new List<string>(new[] { "one", "two", "three"});

        [ValidatePagination]
        [HttpGet("CheckSimpleValidation")]
        public IApiEnumResponse<string> GetPaginationCheckLimits() {
            return OkEnum(Data);
        }
    }
}