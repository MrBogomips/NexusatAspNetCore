namespace Nexusat.AspNetCore.Configuration;

/// <summary>
/// Nexusat ASP net core options.
/// </summary>
public class NexusatAspNetCoreOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether this
    /// <see cref="T:Nexusat.AspNetCore.Configuration.NexusatAspNetCoreOptions"/> is runtime profilation enabled.
    /// </summary>
    /// <value><c>true</c> if is runtime profilation enabled; otherwise, <c>false</c>.</value>
    public bool IsRuntimeProfilationEnabled { get; internal set; } = Defaults.IsRuntimeProfilationEnabled;

    /// <summary>
    /// Gets or sets the name of the pagination page index used to parse the request querystring
    /// </summary>
    /// <value>The name of the pagination page index. Default is <code>pageIndex</code></value>
    public string PaginationPageIndexName { get; internal set; } = Defaults.PaginationPageIndexName;

    /// <summary>
    /// Gets or sets the name of the pagination page size used to parse the request querystring
    /// </summary>
    /// <value>The name of the pagination page size. Default is <code>pageSize</code></value>
    public string PaginationPageSizeName { get; internal set; } = Defaults.PaginationPageSizeName;

    /// <summary>
    /// Gets or sets the size of the pagination default page.
    /// </summary>
    /// <value>The size of the pagination default page. Default is <code>10</code></value>
    public int PaginationDefaultPageSize { get; internal set; } = Defaults.PaginationDefaultPageSize;

    /// <summary>
    /// Gets or sets the size of the pagination default max page.
    /// </summary>
    /// <value>The size of the pagination default max page.</value>
    public int PaginationDefaultMaxPageSize { get; internal set; } = Defaults.PaginationDefaultMaxPageSize;

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// <see cref="T:Nexusat.AspNetCore.Configuration.NexusatAspNetCoreOptions"/> pagination default bad request on
    /// invalid request.
    /// </summary>
    /// <value><c>true</c> will block an page request too large generating a Bad Request response; otherwise, <c>false</c> will
    /// imply the maximum value allowed for the request</value>
    public bool PaginationDefaultBadRequestOnPageSizeOutOfRange { get; internal set; } = Defaults.PaginationDefaultBadRequestOnPageSizeOverflow;
}