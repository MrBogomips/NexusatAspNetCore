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
            return OkEnum(Data.Count, Data);
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

        /// <summary>
        /// Overrides Application defaults
        /// </summary>
        /// <returns>The pagination cursor overrides.</returns>
        [ValidatePagination(DefaultPageSize = 7, MaxPageSize = 77)]
        [HttpGet("CheckPaginationCursorOverrides")]
        public IApiObjectResponse<PaginationCursor> GetPaginationCursorOverrides()
            => OkObject(CurrentPage);

        /// <summary>
        /// Missing the validation attribute on the action method will
        /// prevent accessing to the <see cref="ApiController.CurrentPage"/> page.
        /// </summary>
        /// <returns>The pagination cursor missing validation.</returns>
        [HttpGet("GetPaginationCursorMissingValidation")]
        public IApiObjectResponse<PaginationCursor> GetPaginationCursorMissingValidation()
            => OkObject(CurrentPage);

        [ValidatePagination]
        [HttpGet("GetNumbersPaginated")]
        public IApiEnumResponse<int> GetNumbersPaginated() {
            var itemsCount = 100;

            if (CurrentPage.PageSize * (CurrentPage.PageIndex -1) >= itemsCount)
            {
                throw new NoContentResponseException();
            }

            var items = Enumerable.Range((CurrentPage.PageIndex - 1) * CurrentPage.PageSize, CurrentPage.PageSize).ToList();

            return OkEnum(itemsCount, items);
        }

        [ValidatePagination(DefaultPageSize = 100)]
        [HttpGet("GetFewerItemsThanPageSize")]
        public IApiEnumResponse<int> GetFewerItemsThanPageSize()
        {
            var itemsCount = 3;

            if (CurrentPage.PageSize * (CurrentPage.PageIndex-1) >= itemsCount)
            {
                throw new NoContentResponseException();
            }

            var items = Enumerable.Range(0, itemsCount).ToList();

            return OkEnum(itemsCount, items);
        }

        [ValidatePagination]
        [HttpGet("GetNumbers")]
        public IApiEnumResponse<int> GetNumbers()
        {
            var items = Enumerable.Range((CurrentPage.PageIndex - 1) * CurrentPage.PageSize, CurrentPage.PageSize).ToList();

            return OkEnum(true, items);
        }

        [ValidatePagination]
        [HttpGet("GetEmptyResult")]
        public IApiEnumResponse<int> GetEmptyResult() => OkEmptyEnum<int>();
    }
}