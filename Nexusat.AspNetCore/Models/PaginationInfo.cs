using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Nexusat.AspNetCore.Configuration;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Pagination info.
    /// </summary>
    public sealed class PaginationInfo
    {
        public sealed class PaginationLinks
        {
            public string First { get; private set; }
            public string Next { get; private set; }
            public string Previous { get; private set; }
            public string Last { get; private set; }
        }

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
        internal void SetLinks(NexusatAspNetCoreOptions options, HttpContext httpContext) {
            throw new NotImplementedException();
        }
       }
}
