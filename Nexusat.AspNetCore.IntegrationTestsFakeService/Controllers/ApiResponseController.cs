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
        public IApiResponse Get200() => OkResponse();

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
            r.SetData(new [] {"Hello", "World"});
        });
    }
}