using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Route("Accepted")]
    public class AcceptedController : ApiController
    {
        [HttpGet("index/{id:int}", Name = "accepted_controller_index")]
		public IApiObjectResponse<string> Get(int id) => AcceptedObject(id);

        [HttpPost("{id:int}")]
        public IApiResponse AcceptedResponse(int id)
		=> new Accepted.ResponseAtUri(uri: "/Accepted/" + id.ToString());

        [HttpPost("object/{id:int}")]
        public IApiObjectResponse<string> AcceptedObject(int id)
        {
            Uri uri;
            Uri.TryCreate("/Accepted/" + id.ToString(), UriKind.Relative, out uri);
			return new Accepted.ObjectAtUri<string>(data: "data: " + id.ToString(), uri: uri);
        }

        [HttpPost("action/{id:int}")]
        public IApiResponse AcceptedAtActionFake(int id)
        => new Accepted.ResponseAtAction("Get", routeValues: new { id = id });

        [HttpPost("route/{id:int}")]
        public IApiResponse AcceptedRouteFake(int id)
        => new Accepted.ResponseAtRoute("accepted_controller_index", routeValues: new { id = id });
    }
}
