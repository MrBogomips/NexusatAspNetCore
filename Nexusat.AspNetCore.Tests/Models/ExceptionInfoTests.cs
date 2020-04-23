using System;
using Nexusat.AspNetCore.Models;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Models
{
    public class ExceptionInfoTests
    {
        [Fact]
        public void ExceptionInfoWithInnerTests()
        {
            // Setup

            var myException1 = new Exception("Exception 1");
            var myException2 = new Exception("Exception 2", myException1);
            var myException3 = new Exception("Exception 3", myException2);

            // Act
            ExceptionInfo exceptionInfo = ExceptionInfo.GetFromException(myException3);

            // Assert
            Assert.NotNull(exceptionInfo.Inner);
            Assert.NotNull(exceptionInfo.Inner.Inner);
            Assert.Null(exceptionInfo.Inner.Inner.Inner);

            Assert.Equal("Exception 3", exceptionInfo.Message);
            Assert.Equal("Exception 2", exceptionInfo.Inner.Message);
            Assert.Equal("Exception 1", exceptionInfo.Inner.Inner.Message);
        }
    }
}
