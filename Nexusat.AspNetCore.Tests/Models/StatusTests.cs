using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Models
{
    public class StatusTests
    {
       

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
                //Code = statusCode1,
                Description = description1,
                UserDescription = userDescription1
            };
            var s2 = new Status
            {
                HttpCode = httpCode2,
                //Code = statusCode2,
                Description = description2,
                UserDescription = userDescription2
            };
            s1.Code = statusCode1;
            s2.Code = statusCode2;
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
                Description = description1,
                UserDescription = userDescription1
            };
            var s2 = new Status
            {
                HttpCode = httpCode2,
                //Code = statusCode2,
                Description = description2,
                UserDescription = userDescription2
            };
            s1.Code = statusCode1;
            s2.Code = statusCode2;
            Assert.NotEqual(s1, s2);
        }

        
    }
}
