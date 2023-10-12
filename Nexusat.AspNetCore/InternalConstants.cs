namespace Nexusat.AspNetCore;

/// <summary>
/// Internal constants.
/// </summary>
internal static class InternalConstants
{
    private const string PREFIX_ = "@@NXS_INTERNAL_";
    /// <summary>
    /// The pagination cursor key used to fetch PaginationCursor
    /// from the <see cref="Microsoft.AspNetCore.Http.HttpContext.Items"/>.
    /// </summary>
    public const string PaginationCursorKey = PREFIX_ + nameof(PaginationCursorKey);
}