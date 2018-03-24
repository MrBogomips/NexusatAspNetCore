using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    public sealed class NavigationInfo
    {
        public sealed class NavigationLinks
        {
            public string First { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public string Last { get; set; }
        }

        public NavigationLinks Links { get; set; }
        public int? TotalCount { get; set; }
       }
}
