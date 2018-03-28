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
        public IApiResponse Get299CustomResponse() => ApiResponse(r =>
        {
            r.SetHttpCode(299);
            r.SetStatusCode("OK_299_CUSTOM");
            r.SetDescription("Description");
            r.SetUserDescription("UserDescription");
        });

        /// <summary>
        /// A custom response with a data payload
        /// </summary>
        /// <returns>The custom response.</returns>
        [HttpGet("299CustomStringRespone")]
        public IApiObjectResponse<string> Get299CustomStringResponse() => ApiObjectResponse<string>(r =>
        {
            r.SetHttpCode(299);
            r.SetStatusCode("OK_299_CUSTOM");
            r.SetDescription("Description");
            r.SetUserDescription("UserDescription");
            r.SetData("Hello World!");
        });

        /// <summary>
        /// A custom response with a data payload
        /// </summary>
        /// <returns>The custom response.</returns>
        [HttpGet("299CustomEnumStringRespone")]
        public IApiEnumResponse<string> Get299CustomEnumStringResponse() => ApiEnumResponse<string>(r =>
        {
            r.SetHttpCode(299);
            r.SetStatusCode("OK_299_CUSTOM");
            r.SetDescription("Description");
            r.SetUserDescription("UserDescription");
            r.SetData(new[] { "Hello", "World" });
        });

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

        #region Ok (HTTP 200) Helper Methods flavours
        [HttpGet("200OkResponseWithoutPayload")]
        public IApiResponse GetOkResponseWithourPayload() => Ok();
        [HttpGet("200OkResponseWithObject")]
        public IApiObjectResponse<string> GetOkResponseWithObject() => OkObject(data: "Ciccio");
        [HttpGet("200OkResponseWithManyObjects")]
        public IApiEnumResponse<string> GetOkResponseWithManyObjects() => OkEnum(data: new[] { "Ciccio", "buffo" });
        #endregion Ok Helper Methods falvours

        #region Accpepted (HTTP 202) Helper Methods flavours
        [HttpGet("202OkResponseWithoutPayload")]
        public IApiResponse GetAcceptedResponseWithoutPayload() => Accepted(uri: "some_uri");
        [HttpGet("202OkResponseWithObject")]
        public IApiObjectResponse<string> GetAcceptedResponseWithObject() => AcceptedObject(data: "payload", uri: "some_uri");
        [HttpGet("202OkResponseWithManyObjects")]
        public IApiEnumResponse<string> GetAcceptedResponseWithManyObject() => AcceptedEnum(data: new[] { "pay", "load" }, uri: "some_uri");
        #endregion Accpepted (HTTP 202) Helper Methods flavours
    }
}