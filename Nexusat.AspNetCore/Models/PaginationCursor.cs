using System;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Represents the pagination cursor of the current request
    /// </summary>
    public sealed class PaginationCursor: IEquatable<PaginationCursor>
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
        public int PageSize { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Nexusat.AspNetCore.Models.PaginationCurson"/> 
        /// is page size bounded.
        /// </summary>
        /// <value><c>true</c> if is page size bounded; otherwise, <c>false</c>.</value>
        public bool IsPageSizeBounded { get => PageSize != 0; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Nexusat.AspNetCore.Models.PaginationCurson"/>
        /// is page size unbounded.
        /// </summary>
        /// <value><c>true</c> if is page size unbounded; otherwise, <c>false</c>.</value>
        public bool IsPageSizeUnbounded { get => PageSize == 0; }

        public PaginationCursor(int pageIndex, int pageSize) {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), FormatSystemMessage(ExceptionMessages.PaginationIndexMustBePositive));
            }
            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), FormatSystemMessage(ExceptionMessages.PaginationSizeMustBeNonNegative));
            }
            if (pageSize == 0 && pageIndex > 1)
            {
                throw new ArgumentException(FormatSystemMessage(ExceptionMessages.PaginationIndexMustEqualsOneInCaseOfPageSizeZero));
            }
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        #region Equality pattern
        public bool Equals(PaginationCursor other)
        => other != null && PageSize == other.PageSize && PageIndex == other.PageIndex;

		public override bool Equals(object obj)
		=> Equals(obj as PaginationCursor);

        public override int GetHashCode()
        => PageIndex << 16 ^ PageSize;
        #endregion Equality pattern
	}
}
