using System;
namespace Nexusat.AspNetCore.IntegrationTests.Models
{
    /// <summary>
    /// Represents the pagination cursor of the current request
    /// </summary>
    public class PaginationCursor: IEquatable<PaginationCursor>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsPageSizeBounded { get; set; }
        public bool IsPageSizeUnbounded { get; set; }

        public bool Equals(PaginationCursor other)
        {
            return other != null
                && PageIndex == other.PageIndex
                && PageSize == other.PageSize
                && IsPageSizeBounded == other.IsPageSizeBounded
                && IsPageSizeUnbounded == other.IsPageSizeUnbounded;
        }

		public override bool Equals(object obj)
		{
			return Equals(obj as PaginationCursor);
		}

		public override int GetHashCode()
		{
            throw new NotImplementedException();
		}
	}
}
