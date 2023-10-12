using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Models;

public class ApiResponseTests
{

    [Fact]
    public void ApiResponseWithExceptionExtension() {
        // Setup
        IApiResponse apiResponse = new ApiResponse(new Status(200));
        IApiObjectResponse<string> objectResponse = new ApiObjectResponse<string>(200, data: "Beatrice");
        PaginationCursor cursor = new PaginationCursor(1, 10);
        IApiEnumResponse<string> enumResponse = new ApiEnumResponse<string>(200, cursor, true, new List<string> {"Nausicaa", "Beatrice"});

        var ex1 = new InvalidCastException("ExceptionMessage");

        // Act
        apiResponse.SetException(ex1);
        objectResponse.SetException(ex1);
        enumResponse.SetException(ex1);

        // Assert
        Assert.NotNull(apiResponse.Exception);
        Assert.NotNull(objectResponse.Exception);
        Assert.NotNull(enumResponse.Exception);

        Assert.Equal("ExceptionMessage", apiResponse.Exception.Message);
        Assert.Equal("ExceptionMessage", objectResponse.Exception.Message);
        Assert.Equal("ExceptionMessage", enumResponse.Exception.Message);
    }
}