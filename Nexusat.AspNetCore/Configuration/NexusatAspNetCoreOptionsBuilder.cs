using System;

namespace Nexusat.AspNetCore.Configuration;

/// <summary>
/// Nexusat ASP net core options builder.
/// This is the class by which client code configures the system.
/// </summary>
public class NexusatAspNetCoreOptionsBuilder
{
    private readonly NexusatAspNetCoreOptions Options;

    public NexusatAspNetCoreOptionsBuilder(NexusatAspNetCoreOptions options) =>
        Options = options ?? throw new ArgumentNullException(nameof(options));

    public void EnableRuntimeProfilation()
    {
        Options.IsRuntimeProfilationEnabled = true;
    }

    /// <summary>
    /// Sets the name of the pagination page index parameter used to parse the request's querystring.
    /// </summary>
    /// <param name="pageIndexName">Page index name.</param>
    public void SetPaginationPageIndexName(string pageIndexName) {
        if (null == pageIndexName)
        {
            throw new ArgumentNullException(nameof(pageIndexName));
        } 
        Options.PaginationPageIndexName = pageIndexName;
    }

    /// <summary>
    /// Sets the name of the pagination page size parameter used to parse the request's querystring.
    /// </summary>
    /// <param name="pageSizeName">Page size name.</param>
    public void SetPaginationPageSizeName(string pageSizeName) {
        if (null == pageSizeName) {
            throw new ArgumentNullException(nameof(pageSizeName));
        }
        Options.PaginationPageSizeName = pageSizeName;
    }

    /// <summary>
    /// Sets the size of the pagination default page. This value is used in case of a missing size
    /// on the querystring and a missing upper bound on the controller's request method
    /// </summary>
    /// <param name="defaultPageSize">Default page size. Use <code>0 (zero)</code> to mean no-pagination (fetch all)</param>
    public void SetPaginationDefaultPageSize(int defaultPageSize) {
        if (defaultPageSize < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(defaultPageSize), "Cannot be negative");
        }
        Options.PaginationDefaultPageSize = defaultPageSize;
    }

    /// <summary>
    /// Sets the size of the pagination default max page.
    /// </summary>
    /// <param name="defaultMaxPageSize">Default max page size.</param>
    public void SetPaginationDefaultMaxPageSize(int defaultMaxPageSize) {
        if (defaultMaxPageSize < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(defaultMaxPageSize), "Cannot be negative");
        }
        Options.PaginationDefaultMaxPageSize = defaultMaxPageSize;
    }

    /// <summary>
    /// Sets the pagination default bad request on page size overflow.
    /// </summary>
    /// <param name="blockRequest">If set to <c>true</c> block request generating a 404 bad request response, otherwise will assume the maximum page allowed.</param>
    public void SetPaginationDefaultBadRequestOnPageOutOfRange(bool blockRequest) {
        Options.PaginationDefaultBadRequestOnPageSizeOutOfRange = blockRequest;
    }

    /// <summary>
    /// Validates the options after having collected them.
    /// </summary>
    internal void ValidateOptions() {
        if (Options.PaginationPageIndexName == Options.PaginationPageSizeName)
        {
            throw new ArgumentException(nameof(Options.PaginationPageIndexName) + " cannot match with " + nameof(Options.PaginationPageSizeName));
        }
        /*
         * PaginationDefaultMaxPageSize (MPS)      PaginationDefaultPageSize (DPS)       Rule
         * >0                                      >0                                    MPS > DPS
         * =0                                      >=0                                   Always True
         * >0                                      =0                                    Always False
         */
        if (Options.PaginationDefaultMaxPageSize <= 0) return;
        if (Options.PaginationDefaultMaxPageSize < Options.PaginationDefaultPageSize)
        {
            throw new ArgumentException(nameof(Options.PaginationDefaultMaxPageSize) + " cannot be lesser than " + nameof(Options.PaginationDefaultPageSize));
        } else if (Options.PaginationDefaultPageSize == 0) {
            throw new ArgumentException(nameof(Options.PaginationDefaultMaxPageSize) + " cannot be lesser than " + nameof(Options.PaginationDefaultPageSize) + " (0 meeans 'infinite')");
        }
    }


}