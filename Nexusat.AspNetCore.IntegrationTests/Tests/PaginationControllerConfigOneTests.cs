using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public class PaginationControllerConfigOneTests : PaginationBaseTests<StartupConfigurationOne>
    {
        public PaginationControllerConfigOneTests(ITestOutputHelper output
            ) : base(output) { }

        /// <summary>
        /// Calling a paginated method without pagination SHOULD work properly.
        /// </summary>
        [Fact]
        public async void CheckSimpleValidation()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP200
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
        }

        /// <summary>
        /// Calling a paginated method without pagination SHOULD work properly.
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithIntegers()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=10&p_ix=1");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP200
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
        }

        /// <summary>
        /// Calling a paginated method with an invalid page index SHOULD cause a 400 response
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithIndalidPageIndex()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=10&p_ix=ciccio");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_BAD_PAGE_INDEX", statusCode);
        }

        /// <summary>
        /// Calling a paginated method with an invalid page size SHOULD cause a 400 response
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithIndalidPageSize()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=ciccio&p_ix=1");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_BAD_PAGE_SIZE", statusCode);
        }

        /// <summary>
        /// Calling a paginated method with a page index leq to 0 SHOULD cause a 400 response
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithPageIndexLeq0()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=10&p_ix=0");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_BAD_PAGE_INDEX", statusCode);
        }

        /// <summary>
        /// Calling a paginated method with a page size less than 0 SHOULD cause a 400 response
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithPageSizeLessThan0()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=ciccio&p_ix=1");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_BAD_PAGE_SIZE", statusCode);
        }

        /// <summary>
        /// A page size greather than of MaxPageSize finite value is not accepted.
        /// A 400 response is returned in configuration one.
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationPageSizeOutOfRange()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?p_sz=1000&p_ix=1");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_PAGE_SIZE_OUT_OF_RANGE", statusCode);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 22)]
        public async void CheckPaginationCursorFetch(int pageIndex, int pageSize)
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursor?p_sz={0}&p_ix={1}", pageSize, pageIndex);
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationCursor = new PaginationCursor
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IsPageSizeBounded = true,
                IsPageSizeUnbounded = false
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP400
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
            Assert.Equal(paginationCursor, ExtractPaginationCursor(json));
        }

        /// <summary>
        /// A request without a page index will be interpreted as pageIndex == 1
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        [Fact]
        public async void CheckPaginationMissingPageIndex()
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursor?p_sz=10");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationCursor = new PaginationCursor
            {
                PageIndex = 1,
                PageSize = 10,
                IsPageSizeBounded = true,
                IsPageSizeUnbounded = false
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP400
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
            Assert.Equal(paginationCursor, ExtractPaginationCursor(json));
        }

        /// <summary>
        /// A request without a page index and page size will be interpreted as pageIndex == 1
        /// and will return app page_size default.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        [Fact]
        public async void CheckPaginationMissingPageCursor()
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursor");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationCursor = new PaginationCursor
            {
                PageIndex = 1,
                PageSize = 6, // default page size for setupOne
                IsPageSizeBounded = true,
                IsPageSizeUnbounded = false
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
            Assert.Equal(paginationCursor, ExtractPaginationCursor(json));
        }

        /// <summary>
        /// Checks the pagination cursor overrides.
        /// </summary>
        [Fact]
        public async void CheckPaginationCursorOverrides()
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursorOverrides");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationCursor = new PaginationCursor
            {
                PageIndex = 1,
                PageSize = 7, // custom page size for ther action method
                IsPageSizeBounded = true,
                IsPageSizeUnbounded = false
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
            Assert.Equal(paginationCursor, ExtractPaginationCursor(json));
        }

        /// <summary>
        /// Checks the pagination cursor overrides.
        /// </summary>
        [Fact]
        public async void CheckPaginationCursorPageSizeOverrides77OK()
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursorOverrides?p_sz=77");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationCursor = new PaginationCursor
            {
                PageIndex = 1,
                PageSize = 77, // custom page size for ther action method
                IsPageSizeBounded = true,
                IsPageSizeUnbounded = false
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
            Assert.Equal(paginationCursor, ExtractPaginationCursor(json));
        }

        /// <summary>
        /// Checks the pagination cursor overrides.
        /// </summary>
        [Fact]
        public async void CheckPaginationCursorPageSizeOverrides78KO()
        {
            // Act
            var url = string.Format("/Pagination/CheckPaginationCursorOverrides?p_sz=78");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("KO_PAGE_SIZE_OUT_OF_RANGE", statusCode);
        }

        [Fact]
        public async void CheckPaginationCursorMissingValidationAttribute() 
        {
            // Act
            var url = string.Format("/Pagination/GetPaginationCursorMissingValidation?p_ix=1&p_sz=10");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        #region Pagination over a Bounded Response (PageSize > 0)
        [Fact]
        public async void CheckGetNumbersPaginatedFirstPage()
        {
            // Act
            var url = string.Format("/Pagination/GetNumbersPaginated?p_ix=1&p_sz=10");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationInfo = ExtractPaginationInfo(json);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK_TEST_DEFAULT", statusCode);

            // PageSize bounded response
            // 1st page has all links except the prev
            Assert.NotNull(paginationInfo.Links.First);
            Assert.NotNull(paginationInfo.Links.Current);
            Assert.Null(paginationInfo.Links.Previous);
            Assert.NotNull(paginationInfo.Links.Next);
            Assert.NotNull(paginationInfo.Links.Last);
            Assert.Equal(100, paginationInfo.ItemsCount);
            Assert.Equal(10, paginationInfo.PagesCount);
            Assert.Equal(10, paginationInfo.PageSize);
        }
        [Fact]
        public async void CheckGetNumbersPaginatedSecondPage()
        {
            // Act
            var url = string.Format("/Pagination/GetNumbersPaginated?p_ix=2&p_sz=10");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationInfo = ExtractPaginationInfo(json);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK_TEST_DEFAULT", statusCode);

            // PageSize bounded response
            // 2nd page has all links (inner page)
            Assert.NotNull(paginationInfo.Links.First);
            Assert.NotNull(paginationInfo.Links.Current);
            Assert.NotNull(paginationInfo.Links.Previous);
            Assert.NotNull(paginationInfo.Links.Next);
            Assert.NotNull(paginationInfo.Links.Last);
            Assert.Equal(100, paginationInfo.ItemsCount);
            Assert.Equal(10, paginationInfo.PagesCount);
            Assert.Equal(10, paginationInfo.PageSize);
        }

        [Fact]
        public async void CheckGetNumbersPaginatedLastPage()
        {
            // Act
            var url = string.Format("/Pagination/GetNumbersPaginated?p_ix=10&p_sz=10");
            var response = await Client.GetAsync(url);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            var paginationInfo = ExtractPaginationInfo(json);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK_TEST_DEFAULT", statusCode);

            // PageSize bounded response
            // Last page has all links except the last one
            Assert.NotNull(paginationInfo.Links.First);
            Assert.NotNull(paginationInfo.Links.Current);
            Assert.NotNull(paginationInfo.Links.Previous);
            Assert.Null(paginationInfo.Links.Next);
            Assert.NotNull(paginationInfo.Links.Last);
            Assert.Equal(100, paginationInfo.ItemsCount);
            Assert.Equal(10, paginationInfo.PagesCount);
            Assert.Equal(10, paginationInfo.PageSize);
        }

        /// <summary>
        /// Requesting a page over the last one should produce 
        /// a NoContent response
        /// </summary>
        [Fact]
        public async void CheckGetNumbersPaginatedOverLastPage()
        {
            // Act
            var url = string.Format("/Pagination/GetNumbersPaginated?p_ix=11&p_sz=10");
            var response = await Client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Empty(body); // no body expected
        }
        #endregion Pagination over a Bounded Response (PageSize > 0)
    }
}
