namespace Nexusat.AspNetCore.Configuration;

public static class Defaults
{
    public const bool IsRuntimeProfilationEnabled = false;
    public const string PaginationPageIndexName = "pageIndex";
    public const string PaginationPageSizeName = "pageSize";
    public const int PaginationDefaultPageSize = 10;
    public const int PaginationDefaultMaxPageSize = 100;
    public const bool PaginationDefaultBadRequestOnPageSizeOverflow = false;
}