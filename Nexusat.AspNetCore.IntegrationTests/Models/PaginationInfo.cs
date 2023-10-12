namespace Nexusat.AspNetCore.IntegrationTests.Models;

public sealed class PaginationInfo
{
    public sealed class PaginationLinks
    {
        public string First { get; set; }
        public string Current { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public string Last { get; set; }
    }

    public PaginationLinks Links { get; private set; } = new PaginationLinks();
    public int? ItemsCount { get; set; }
    public int? PagesCount { get; set; }
    public int PageSize { get; set; }
}