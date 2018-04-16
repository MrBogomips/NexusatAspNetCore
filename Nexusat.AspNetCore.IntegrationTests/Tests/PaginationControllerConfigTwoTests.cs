﻿using Microsoft.Net.Http.Headers;
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
    public class PaginationControllerConfigTwoTests : BaseTests<StartupConfigurationTwo>
    {
        public PaginationControllerConfigTwoTests(ITestOutputHelper output
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
            Assert.Equal("OK_DEFAULT", statusCode);
        }

        /// <summary>
        /// Calling a paginated method without pagination SHOULD work properly.
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithIntegers()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=10&pageIndex=1");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP200
            Assert.Equal("OK_DEFAULT", statusCode);
        }

        /// <summary>
        /// Calling a paginated method with an invalid page index SHOULD cause a 400 response
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationWithIndalidPageIndex()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=10&pageIndex=ciccio");
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
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=ciccio&pageIndex=1");
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
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=10&pageIndex=0");
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
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=ciccio&pageIndex=1");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // HTTP400
            Assert.Equal("KO_BAD_PAGE_SIZE", statusCode);
        }

        /// <summary>
        /// A page size greather than of MaxPageSize finite value is not accepted.
        /// In this configuration the MaxPageSize is 0 that means infinity, 
        /// therefore we don't expect a BAD REQUEST reponse.
        /// </summary>
        [Fact]
        public async void CheckSimpleValidationPageSizeOutOfRange()
        {
            // Act
            var response = await Client.GetAsync("/Pagination/CheckSimpleValidation?pageSize=1000&pageIndex=1");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP400
            Assert.Equal("OK_DEFAULT", statusCode);
        }
    }
}