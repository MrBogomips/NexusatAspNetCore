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
        [InlineData("OK_something")]
        public void TestValidCodes(string code)
        {
            Assert.True(StatusCode.CheckValidCode(code));
            Assert.NotNull(new StatusCode(code));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("ok")]
        [InlineData("ko")]
        [InlineData("ok_SOMETHING")]
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

            Assert.Equal(lhs, rhs); // pleonatistc!
            Assert.Equal(c1, rhs);
            Assert.Equal(c2, rhs);
            Assert.Equal(c1, lhs);
            Assert.Equal(c2, lhs);
            Assert.Equal(c1, c2);
        }

        [Fact]
        public void TestEqualityMethods() {
            /*
             * public bool Equals(string other) => other == Code;
             * public bool Equals(StatusCode other) => other?.Code == Code;
             * public override bool Equals(object obj) => Equals(obj as StatusCode);
             * public override int GetHashCode() => Code.GetHashCode();
             */
            StatusCode code = "OK";
            StatusCode code2 = "OK";
            string scode = "OK";
            object ocode = "OK";


            Assert.False(code.Equals(null as string));
            Assert.False(code.Equals(null as StatusCode));
            Assert.True(code.Equals(scode));
            Assert.True(code.Equals(ocode));
            Assert.True(code.Equals(code2));
        }

        [Fact]
        public void TestEqualityOperators()
        {
            /*
             * public bool Equals(string other) => other == Code;
             * public bool Equals(StatusCode other) => other?.Code == Code;
             * public override bool Equals(object obj) => Equals(obj as StatusCode);
             * public override int GetHashCode() => Code.GetHashCode();
             */
            StatusCode code = "OK";
            StatusCode code2 = "OK";
            string scode = "OK";
            object ocode = "OK";


            //Assert.False(code == (null as string));
            //Assert.False(code == (null as StatusCode));
//Assert.True(code == scode);
            //Assert.True(code == ocode);
            Assert.True(code == code2);
        }
    }
}
