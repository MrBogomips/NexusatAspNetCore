using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Route("Created")]
    public class CreatedController : ApiController
    {
        [HttpGet("index/{id:int}", Name = "created_controller_index")]
        public IApiObjectResponse<string> Get(int id) => OkObject(id.ToString());

        [HttpPost("{id:int}")]
        public IApiResponse CreatedResponse(int id) 
        => Created(uri: "/Created/" + id.ToString());

        [HttpPost("object/{id:int}")]
        public IApiObjectResponse<string> CreatedObject(int id) {
            Uri uri;
            Uri.TryCreate("/Created/" + id.ToString(), UriKind.Relative, out uri);
            return CreatedObject(data: "data: " + id.ToString(), uri: uri);
        }

    }
}
