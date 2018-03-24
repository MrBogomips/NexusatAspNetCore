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

        [Theory]
        [InlineData(
            /* 1 */ 200, "OK", "Description", null, 
            /* 2 */ 200, "OK", "Description", null)]
        [InlineData(
            /* 1 */ 201, "OK_K", "Description", null,
            /* 2 */ 201, "OK_K", "Description", "something")]
        [InlineData(
            /* 1 */ 203, "KO", "Description", "user description",
            /* 2 */ 203, "KO", "Description", "is not considered")]
        public void Equal(
            int httpCode1, string statusCode1, string description1, string userDescription1,
            int httpCode2, string statusCode2, string description2, string userDescription2
            )
        {
            var s1 = new Status
            {
                HttpCode = httpCode1,
                Code = statusCode1,
                Description = description1,
                UserDescription = userDescription1
            };
            var s2 = new Status
            {
                HttpCode = httpCode2,
                Code = statusCode2,
                Description = description2,
                UserDescription = userDescription2
            };
            Assert.Equal(s1, s2);
            Assert.NotSame(s1, s2);
        }

        [Theory]
        [InlineData(
            /* 1 */ 200, "OK", "Description", null,
            /* 2 */ 201, "OK", "Description", null)]
        [InlineData(
            /* 1 */ 200, "OK", "Description", null,
            /* 2 */ 200, "KO", "Description", null)]
        [InlineData(
            /* 1 */ 201, "OK_K", "Description1", null,
            /* 2 */ 201, "OK_K", "Description2", "something")]
        public void NotEqual(
            int httpCode1, string statusCode1, string description1, string userDescription1,
            int httpCode2, string statusCode2, string description2, string userDescription2
            )
        {
            var s1 = new Status
            {
                HttpCode = httpCode1,
                Code = statusCode1,
                Description = description1,
                UserDescription = userDescription1
            };
            var s2 = new Status
            {
                HttpCode = httpCode2,
                Code = statusCode2,
                Description = description2,
                UserDescription = userDescription2
            };
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void CommonStatusFactory()
        {
            var ok1 = Status.Ok;
            var ok2 = Status.Ok;

            Assert.Equal(ok1, ok2);
            Assert.NotSame(ok1, ok2);

            var ko1 = Status.Ko;
            var ko2 = Status.Ko;

            Assert.Equal(ko1, ko2);
            Assert.NotSame(ko1, ko2);
        }
    }
}
