using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers;

[Produces("application/json")]
[Route("ApiResponse")]
public class ApiResponseController : ApiController
{
    /// <summary>
    /// The fast way to return a generic OK response
    /// </summary>
    /// <returns>The get200.</returns>
    [HttpGet("200")]
    public IApiResponse Get200() => new Ok.Response();

    /// <summary>
    /// A custom response without a data payload
    /// </summary>
    /// <returns>The custom response.</returns>
    [HttpGet("299CustomRespone")]
    public IApiResponse Get299CustomResponse() 
        => new ApiResponse(299, "OK_299_CUSTOM", "Description", "UserDescription");


    /// <summary>
    /// A custom response with a data payload
    /// </summary>
    /// <returns>The custom response.</returns>
    [HttpGet("299CustomStringRespone")]
    public IApiObjectResponse<string> Get299CustomStringResponse() 
        => new ApiObjectResponse<string>(299, "OK_299_CUSTOM", "Hello World!", "Description", "UserDescription");

    /// <summary>
    /// A custom response with a data payload
    /// </summary>
    /// <returns>The custom response.</returns>
    [HttpGet("299CustomEnumStringRespone")]
    [ValidatePagination]
    public IApiEnumResponse<string> Get299CustomEnumStringResponse() 
        => new ApiEnumResponse<string>(299, CurrentPage, true, statusCode: "OK_299_CUSTOM", data: new[] { "Hello", "World" }, description: "Description", userDescription: "UserDescription");

    [HttpGet("200OkResponseWithException")]
    public IApiResponse Get200OkResponseWithException()
    {
        var response = new Ok.Response();

        try
        {
            throw new Exception("Fake exception");
        }
        catch (Exception ex)
        {
            response.SetException(ex);
        }

        return response;
    }

    [HttpGet("500KoUnhandledException")]
    public IApiResponse Get500KoUnhandledException()
    {
        var response = new Ok.Response();

        var num = 1;
        var den = 0;
        var res = num / den;

        return response;
    }


    #region Ok (HTTP 200) Helper Methods flavours
    [HttpGet("200OkResponseWithoutPayload")]
    public IApiResponse GetOkResponseWithourPayload() => new Ok.Response();

    [HttpGet("200OkResponseWithObject")]
    public IApiObjectResponse<string> GetOkResponseWithObject() 
        => new Ok.Object<string>(data: "Ciccio");

    [HttpGet("200OkResponseWithManyObjects")]
    [ValidatePagination]
    public IApiEnumResponse<string> GetOkResponseWithManyObjects() 
        => new Ok.Enum<string>(new[] { "Ciccio", "buffo" }, CurrentPage, 2);
    #endregion Ok Helper Methods falvours

    #region Accpepted (HTTP 202) Helper Methods flavours
    [HttpGet("202OkResponseWithoutPayload")]
    public IApiResponse GetAcceptedResponseWithoutPayload() 
        => new Accepted.ResponseAtUri("http://www.google.com");
    [HttpGet("202OkResponseWithObject")]
    public IApiObjectResponse<string> GetAcceptedResponseWithObject() 
        => new Accepted.ObjectAtUri<string>(data: "payload", uri: "http://www.google.com");
    #endregion Accpepted (HTTP 202) Helper Methods flavours

    #region BadRequest (HTTP 400) Helper Methods flavours
    [HttpGet("400KoResponseWithoutPayload")]
    public IApiResponse GetKoResponseWithourPayload() => new BadRequest.Response();

    [HttpGet("400KoResponseWithObject")]
    public IApiObjectResponse<string> GetKoResponseWithObject()
        => new BadRequest.Object<string>(data: "Ciccio");

    [HttpGet("400KoResponseWithManyObjects")]
    [ValidatePagination]
    public IApiEnumResponse<string> GetKoResponseWithManyObjects()
        => new BadRequest.Enum<string>(new[] { "Ciccio", "buffo" }, CurrentPage, 2);
    #endregion BadRequest (HTTP 400) Helper Methods flavours

}