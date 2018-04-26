using System;
using System.Text.RegularExpressions;
using Nexusat.AspNetCore.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.Tests.Utils
{
    public class PaginationInfoLinkBuilderTests
    {
        // The Pattern is applyed only to the qyery part without the leading '?'
        private const string PageIndexPattern = @"[?&]({0}=(?<number>\d+))";
        private const string PageSizePattern = PageIndexPattern;

        private readonly Regex RegexPageIndex;
        private readonly Regex RegexPageSize;

        private const string pageIndexKeyName = "pageIndex";
        private const string pageSizeKeyName = "pageSize";

        private readonly ITestOutputHelper Output;
        public PaginationInfoLinkBuilderTests(ITestOutputHelper outputHelper)
        {
            Output = outputHelper;

            var rePageIndexPattern = string.Format(PageIndexPattern, Regex.Escape(pageIndexKeyName));
            var rePageSizePattern = string.Format(PageSizePattern, Regex.Escape(pageSizeKeyName));

            RegexPageIndex = new Regex(rePageIndexPattern, RegexOptions.Compiled);
            RegexPageSize = new Regex(rePageSizePattern, RegexOptions.Compiled);
        }


        [Theory]
        [InlineData("http::localhost/segment", 6, 66)]
        [InlineData("http::localhost/segment/", 6, 66)]
        [InlineData("/", 6, 66)]
        [InlineData("/segment", 6, 66)]
        [InlineData("/segment?pageIndex=1", 6, 66)]
        [InlineData("/?pageIndex=1", 6, 66)]
        [InlineData("/segmnet?pageIndex=1&pageSize=10", 6, 66)]
        [InlineData("/segmnet?pageSize=10&pageIndex=10", 6, 66)]
        [InlineData("/segmnet?ciccio=buffo&pageSize=10&pageIndex=10", 6, 66)]
        [InlineData("/segmnet?pageSize=10&pageIndex=10&ciccio=buffo", 6, 66)]
        [InlineData("/segmnet?buffo=ciccio&pageSize=10&pageIndex=10&ciccio=buffo", 6, 66)]
        public void TestUrlLinks(string originalLink, int pageIndex, int pageSize) {
            // Setup
            var linkBuilder = new PaginationInfoLinkBuilder(originalLink, pageIndexKeyName, pageSizeKeyName, pageSize);

            Output.WriteLine("Original URL: {0}", originalLink);

            // Act
            var actual = linkBuilder.GetLink(pageIndex);
            Output.WriteLine("Altered URL: {0}", actual);

            // Check
            Assert.Contains("?", actual);
            Assert.Contains("&", actual);
            Assert.DoesNotContain("&?", actual);
            Assert.DoesNotContain("?&", actual);
            Assert.DoesNotContain("&&", actual);
            Assert.DoesNotContain("??", actual);

            Assert.True(CheckPageIndex(actual, pageIndex));
            Assert.True(CheckPageSize(actual, pageSize));
        }

        private bool CheckPageIndex(string url, int pageIndex) {
            Output.WriteLine("Checking PageIndex {0} Presence in '{1}'", pageIndex, url);

            // Url Must Contains the page
            Assert.Matches(RegexPageIndex, url);

            // The page Matches tghe value
            int pageIndexFound = int.Parse(RegexPageIndex.Match(url).Groups["number"].Value);
            Assert.Equal(pageIndex, pageIndexFound);

            return true;
        }

        private bool CheckPageSize(string url, int pageSize) {
            Output.WriteLine("Checking PageSize {0} Presence in '{1}'", pageSize, url);

            // Url Must Contains the page
            Assert.Matches(RegexPageSize,url);

            // The page Matches tghe value
            int pageIndexFound = int.Parse(RegexPageSize.Match(url).Groups["number"].Value);
            Assert.Equal(pageSize, pageIndexFound);

            return true;
        }
    }
}
