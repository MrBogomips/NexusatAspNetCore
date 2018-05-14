using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Nexusat.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Route("/hello_world")]
    public class HelloWorldController: ApiController
    {
        [HttpGet]
        public string Get() => "Hello World!";

        [HttpGet("api_response")]
		public IApiObjectResponse<string> GetObject() => new Ok.Object<string>(data: "Hello World!");
    }
}
