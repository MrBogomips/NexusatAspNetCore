using System;
namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Represents the pagination cursor of the current request
    /// </summary>
    public class PaginationCursor
    {
        /// <summary>
        /// The page required.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; private set; }
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int? PageSize { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Nexusat.AspNetCore.Models.PaginationCurson"/> 
        /// is page size bounded.
        /// </summary>
        /// <value><c>true</c> if is page size bounded; otherwise, <c>false</c>.</value>
        public bool IsPageSizeBounded { get => PageSize != null; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Nexusat.AspNetCore.Models.PaginationCurson"/>
        /// is page size unbounded.
        /// </summary>
        /// <value><c>true</c> if is page size unbounded; otherwise, <c>false</c>.</value>
        public bool IsPageSizeUnbounded { get => PageSize == null; }

        internal PaginationCursor(int? pageIndex, int? pageSize) {
            PageIndex = pageIndex ?? 1;
            PageSize = pageSize;
        }
    }
}
