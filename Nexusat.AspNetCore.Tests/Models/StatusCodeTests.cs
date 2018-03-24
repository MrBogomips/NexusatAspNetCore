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
        [InlineData("OK_SOMETHING")]
        [InlineData("KO_SOMETHING")]
        public void TestValidCodes(string code)
        {
            Assert.True(StatusCode.CheckValidCode(code));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("ok")]
        [InlineData("ko")]
        [InlineData("OK SOMETHING")]
        [InlineData("KO SOMETHING")]
        [InlineData("SOMETHING")]
        [InlineData(" OK")]
        [InlineData("OK ")]
        [InlineData(" KO")]
        [InlineData("KO ")]
        public void TestInvalidCodes(string code)
        {
            Assert.False(StatusCode.CheckValidCode(code));
        }
    }
}
