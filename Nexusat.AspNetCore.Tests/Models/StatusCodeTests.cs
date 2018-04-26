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
            Assert.NotNull(new StatusCode(code));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("ok")] // lowercase codes are invalid
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
            Assert.ThrowsAny<Exception>(() => new StatusCode(code));
        }

        [Theory]
        [InlineData("OK", "OK")]
        [InlineData("KO", "KO")]
        [InlineData("OK_SOMETHING", "OK_SOMETHING")]
        [InlineData("KO_SOME", "KO_SOME")]
        public void TestEquality(string lhs, string rhs)
        {
            StatusCode c1 = lhs;
            StatusCode c2 = rhs;

            Assert.Equal(lhs, rhs);
            Assert.Equal(c1, rhs);
            Assert.Equal(c2, rhs);
            Assert.Equal(c1, lhs);
            Assert.Equal(c2, lhs);
            Assert.Equal(c1, c2);
        }
    }
}
