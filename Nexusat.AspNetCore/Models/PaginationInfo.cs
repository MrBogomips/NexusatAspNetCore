using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Utils;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Pagination info.
    /// </summary>
    public sealed class PaginationInfo
    {
        public sealed class PaginationLinks
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string First { get; internal set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Current { get; internal set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Next { get; internal set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Previous { get; internal set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Last { get; internal set; }
        }

        /// <summary>
        /// Pagination links are generated only in case of PageSize bounded requests
        /// </summary>
        /// <value>The links.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaginationLinks Links { get; internal set; }
        /// <summary>
        /// Represents the number of items found.
        /// The <code>null</code> value stands for unknown.
        /// </summary>
        /// <value>The items count.</value>
        public int? ItemsCount { get; internal set; }
        /// <summary>
        /// Represents the number of pages expected.
        /// The <code>null</code> value stands for unknown.
        /// </summary>
        /// <value>The pages count.</value>
        public int? PagesCount { get; internal set; }
        /// <summary>
        /// Represents the maximum number of items per page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; internal set; }

        [JsonIgnore]
        internal bool HasNextPage { get; set; }
        [JsonIgnore]
        internal PaginationCursor PaginationCursor { get; set; }

        /// <summary>
        /// Sets the pagination links.
        /// </summary>
        /// <param name="options">Application Global settings.</param>
        /// <param name="httpContext">The Http Context of the request</param>
        internal void SetLinks(NexusatAspNetCoreOptions options, HttpContext context) {
            string requestUrl = context.Request.GetEncodedPathAndQuery();

            // Only one of the following condition can hold
            /// <see cref="Builders.ApiEnumResponseBuilder{T}"/>
            Debug.Assert(ItemsCount.HasValue ^ HasNextPage);

            PageSize = PaginationCursor.PageSize;

            if (PaginationCursor.IsPageSizeBounded) // If unbounded links are not generated
            {
                Links = new PaginationLinks();
                var linkBuilder = new PaginationInfoLinkBuilder(
                        context.Request.GetEncodedPathAndQuery(),
                        options.PaginationPageIndexName,
                        options.PaginationPageSizeName,
                        PaginationCursor.PageSize
                    );
                // First
                Links.First = linkBuilder.GetLink(1);
                // Current
                Links.Current = linkBuilder.GetLink(PaginationCursor.PageIndex);
                // Previous
                if (PaginationCursor.PageIndex > 1)
                {
                    Links.Previous = linkBuilder.GetLink(PaginationCursor.PageIndex - 1);
                }

                if (ItemsCount.HasValue)
                { // calculate links and pageCount
                    int pageCount =
                        (ItemsCount.Value + PaginationCursor.PageSize - 1) / PaginationCursor.PageSize;
                    PagesCount = pageCount;
                    if (pageCount > PaginationCursor.PageIndex) {
                        
                        // Next
                        if (pageCount > PaginationCursor.PageIndex)
                        {
                            Links.Next = linkBuilder.GetLink(PaginationCursor.PageIndex + 1);
                        }
                        // Last
                        Links.Last = linkBuilder.GetLink(pageCount);
                    } 
                } 
                else if (HasNextPage) // implies hasn't itemsCount
                { // calculate links except the "last"
                    Links.Next = linkBuilder.GetLink(PaginationCursor.PageIndex + 1);
                }
            } else { // Unbounded requests have only 1 page of data
                PagesCount = 1;
            }

            context.Request.GetDisplayUrl();
        }
    }
}
