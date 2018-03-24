using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Models
{
    public class StatusTests
    {
        [Fact]
        public void StandardStatusOk()
        {
            Assert.NotNull(Status.Ok);
            Assert.Equal(200, Status.Ok.HttpCode);
            Assert.Equal("OK", Status.Ok.Code);
        }
        [Fact]
        public void StandardStatusKo()
        {
            Assert.NotNull(Status.Ko);
            Assert.Equal(200, Status.Ko.HttpCode);
            Assert.Equal("KO", Status.Ko.Code);
        }
    }
}
