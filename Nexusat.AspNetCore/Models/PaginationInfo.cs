using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Pagination info.
    /// </summary>
    public sealed class PaginationInfo
    {
        public sealed class PaginationLinks
        {
            public string First { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public string Last { get; set; }
        }

        public PaginationLinks Links { get; set; }
        /// <summary>
        /// Represents the number of items found.
        /// The <code>null</code> value stands for unknown.
        /// </summary>
        /// <value>The items count.</value>
        public int? ItemsCount { get; set; }
        /// <summary>
        /// Represents the number of pages expected.
        /// The <code>null</code> value stands for unknown.
        /// </summary>
        /// <value>The pages count.</value>
        public int? PagesCount { get; set; }
        /// <summary>
        /// Represents the maximum number of items per page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }
       }
}
