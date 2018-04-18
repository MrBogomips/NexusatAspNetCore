using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Utils
{
    /// <summary>
    /// Utility class to build links for the <see cref="Models.PaginationInfo.Links"/>.
    /// </summary>
    public sealed class PaginationInfoLinkBuilder
    {
        // The Pattern is applyed only to the qyery part without the leading '?'
        private const string PageIndexPattern = @"(^|&)({0}=(\d+))";
        private const string PageSizePattern = PageIndexPattern;

        /// <summary>
        /// The original url
        /// </summary>
        public string OriginalUrl { get; }
        /// <summary>
        /// Gets the URL path part.
        /// </summary>
        /// <value>The URL path part.</value>
        public string UrlPathPart { get; }
        /// <summary>
        /// Gets the URL query part.
        /// </summary>
        /// <value>The URL query part.</value>
        public string UrlQueryPart { get; }
        /// <summary>
        /// Gets the name of the page index key.
        /// </summary>
        /// <value>The name of the page index key.</value>
        public string PageIndexKeyName { get; }
        /// <summary>
        /// Gets the name of the page size key.
        /// </summary>
        /// <value>The name of the page size key.</value>
        public string PageSizeKeyName { get; }
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; }

        private readonly Regex RegexPageIndex;
        private readonly Regex RegexPageSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Utils.PaginationInfoLinkBuilder"/> class.
        /// </summary>
        /// <param name="originalUrl">Original URL.</param>
        /// <param name="pageIndexKeyName">Page index key name.</param>
        /// <param name="pageSizeKeyName">Page size key name.</param>
        /// <param name="pageSize">Page size.</param>
        public PaginationInfoLinkBuilder(string originalUrl, string pageIndexKeyName, string pageSizeKeyName, int pageSize)
        {
            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(FormatSystemMessage(ExceptionMessages.PaginationSizeMustBeNonNegative));
            }
            PageSize = pageSize;
            OriginalUrl = originalUrl ?? throw new ArgumentNullException(nameof(originalUrl));
            PageIndexKeyName = pageIndexKeyName ?? throw new ArgumentNullException(nameof(pageIndexKeyName));
            PageSizeKeyName = pageSizeKeyName ?? throw new ArgumentNullException(nameof(pageSizeKeyName));

            var rePageIndexPattern = string.Format(PageIndexPattern, Regex.Escape(pageIndexKeyName));
            var rePageSizePattern = string.Format(PageSizePattern, Regex.Escape(pageSizeKeyName));

            RegexPageIndex = new Regex(rePageIndexPattern, RegexOptions.Compiled);
            RegexPageSize = new Regex(rePageSizePattern, RegexOptions.Compiled);

            // Split the original URL into Path and Query part
            var urlParts = originalUrl.Split('?');

            UrlPathPart = urlParts[0];
            if (urlParts.Length == 2)
            {
                UrlQueryPart = urlParts[1];
                // The query Part is cleaned up from PageIndex and PageSize components
                UrlQueryPart = RegexPageIndex.Replace(UrlQueryPart, string.Empty);
                UrlQueryPart = RegexPageSize.Replace(UrlQueryPart, string.Empty);
                UrlQueryPart = UrlQueryPart.Trim('&');
            }
            else if (urlParts.Length > 2)
            {
                throw new ArgumentException(FormatSystemMessage(ExceptionMessages.InvalidUriTooManyQuestionMarks));
            }

        }

        /// <summary>
        /// Gets the link to the specific page
        /// </summary>
        /// <returns>The link.</returns>
        /// <param name="pageIndex">A Page index</param>
        public string GetLink(int pageIndex) {
            if (pageIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), FormatSystemMessage(ExceptionMessages.PaginationIndexMustBePositive));
            }
            var sb = new System.Text.StringBuilder();
            sb.Append(UrlPathPart);
            sb.Append('?');
            if (!string.IsNullOrEmpty(UrlQueryPart)) {
                sb.Append(UrlQueryPart);
                sb.Append('&');
            }
            sb.Append(PageIndexKeyName);
            sb.Append('=');
            sb.Append(pageIndex);
            sb.Append('&');
            sb.Append(PageSizeKeyName);
            sb.Append('=');
            sb.Append(PageSize);

            return sb.ToString();
        }
    }
}
