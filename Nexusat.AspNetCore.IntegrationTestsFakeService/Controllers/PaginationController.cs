using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Exceptions;
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
			return new Ok.Enum<string>(Data, CurrentPage, Data.Count);
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
            => new Ok.Object<PaginationCursor>(CurrentPage);

        /// <summary>
        /// Overrides Application defaults
        /// </summary>
        /// <returns>The pagination cursor overrides.</returns>
        [ValidatePagination(DefaultPageSize = 7, MaxPageSize = 77)]
        [HttpGet("CheckPaginationCursorOverrides")]
        public IApiObjectResponse<PaginationCursor> GetPaginationCursorOverrides()
            => new Ok.Object<PaginationCursor>(CurrentPage);

        /// <summary>
        /// Missing the validation attribute on the action method will
        /// prevent accessing to the <see cref="ApiController.CurrentPage"/> page.
        /// </summary>
        /// <returns>The pagination cursor missing validation.</returns>
        [HttpGet("GetPaginationCursorMissingValidation")]
        public IApiObjectResponse<PaginationCursor> GetPaginationCursorMissingValidation()
            => new Ok.Object<PaginationCursor>(CurrentPage);

        [ValidatePagination]
        [HttpGet("GetNumbersPaginated")]
        public IApiEnumResponse<int> GetNumbersPaginated() {
            var itemsCount = 100;

            if (CurrentPage.PageSize * (CurrentPage.PageIndex -1) >= itemsCount)
            {
                throw new NoContent.Exception();
            }

            var items = Enumerable.Range((CurrentPage.PageIndex - 1) * CurrentPage.PageSize, CurrentPage.PageSize).ToList();

            return new Ok.Enum<int>(items, CurrentPage, itemsCount);
        }

        [ValidatePagination(DefaultPageSize = 100)]
        [HttpGet("GetFewerItemsThanPageSize")]
        public IApiEnumResponse<int> GetFewerItemsThanPageSize()
        {
            var itemsCount = 3;

            if (CurrentPage.PageSize * (CurrentPage.PageIndex-1) >= itemsCount)
            {
                throw new NoContent.Exception();
            }

            var items = Enumerable.Range(0, itemsCount).ToList();

			return new Ok.Enum<int>(items, CurrentPage, itemsCount);
        }

        [ValidatePagination]
        [HttpGet("GetNumbers")]
        public IApiEnumResponse<int> GetNumbers()
        {
            var items = Enumerable.Range((CurrentPage.PageIndex - 1) * CurrentPage.PageSize, CurrentPage.PageSize).ToList();

            return new Ok.Enum<int>(items, CurrentPage, true);
        }

        [ValidatePagination]
        [HttpGet("GetEmptyResult")]
        public IApiEnumResponse<int> GetEmptyResult() => new NoContent.Enum<int>();
    }
}