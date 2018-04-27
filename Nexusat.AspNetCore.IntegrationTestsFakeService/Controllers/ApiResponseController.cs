using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Produces("application/json")]
    [Route("ApiResponse")]
    public class ApiResponseController : ApiController
    {
        /// <summary>
        /// The fast way to return a generic OK response
        /// </summary>
        /// <returns>The get200.</returns>
        [HttpGet("200")]
        public IApiResponse Get200() => Ok();

        /// <summary>
        /// A custom response without a data payload
        /// </summary>
        /// <returns>The custom response.</returns>
        [HttpGet("299CustomRespone")]
        public IApiResponse Get299CustomResponse() 
        => ApiResponse(299, "OK_299_CUSTOM", "Description", "UserDescription");


        /// <summary>
        /// A custom response with a data payload
        /// </summary>
        /// <returns>The custom response.</returns>
        [HttpGet("299CustomStringRespone")]
        public IApiObjectResponse<string> Get299CustomStringResponse() 
        => ApiObjectResponse<string>(299, "OK_299_CUSTOM", "Hello World!", "Description", "UserDescription");

        /// <summary>
        /// A custom response with a data payload
        /// </summary>
        /// <returns>The custom response.</returns>
        [HttpGet("299CustomEnumStringRespone")]
        [ValidatePagination]
        public IApiEnumResponse<string> Get299CustomEnumStringResponse() 
        => ApiEnumResponse<string>(299, true, "OK_299_CUSTOM", new[] { "Hello", "World" }, "Description", "UserDescription");

        [HttpGet("200OkResponseWithException")]
        public IApiResponse Get200OkResponseWithException()
        {
            var response = Ok();

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
            var response = Ok();

            var num = 1;
            var den = 0;
            var res = num / den;

            return response;
        }


        #region Ok (HTTP 200) Helper Methods flavours
        [HttpGet("200OkResponseWithoutPayload")]
        public IApiResponse GetOkResponseWithourPayload() => Ok();
        [HttpGet("200OkResponseWithObject")]
        public IApiObjectResponse<string> GetOkResponseWithObject() => OkObject(data: "Ciccio");
        [HttpGet("200OkResponseWithManyObjects")]
        [ValidatePagination]
        public IApiEnumResponse<string> GetOkResponseWithManyObjects() => OkEnum(2, data: new[] { "Ciccio", "buffo" });
        #endregion Ok Helper Methods falvours

        #region Accpepted (HTTP 202) Helper Methods flavours
        [HttpGet("202OkResponseWithoutPayload")]
        public IApiResponse GetAcceptedResponseWithoutPayload() => Accepted(uri: "some_uri");
        [HttpGet("202OkResponseWithObject")]
        public IApiObjectResponse<string> GetAcceptedResponseWithObject() => Accepted(data: "payload", uri: "some_uri");
        #endregion Accpepted (HTTP 202) Helper Methods flavours

    }
}