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
        List<string> Data = new List<string>(new[] { "one", "two", "three" });

        [ValidatePagination]
        [HttpGet("CheckSimpleValidation")]
        public IApiEnumResponse<string> GetPaginationCheckLimits()
        {
            return OkEnum(Data);
        }

        /// <summary>
        /// This method is just for the sake of testing.
        /// In practice it doesn't make sense to use pagination on an <see cref="IApiObjectResponse{T}"/>
        /// action method.
        /// </summary>
        /// <returns>The pagination cursor.</returns>
        [ValidatePagination]
        [HttpGet("CheckPaginationCursor")]
        public IApiObjectResponse<PaginationCursor> GetPaginationCursor()
            => OkObject(CurrentPage);


    }
}