using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Nexusat.AspNetCore.Models;


namespace Nexusat.AspNetCore.Tests.Models
{
    public class StatusCodeTests
    {
        [Theory]
        [InlineData("OK")]
        [InlineData("KO")]
        public void TestValidCodes(string code)
        {
            Assert.True(StatusCode.CheckValidCode(code));
        }
    }
}
