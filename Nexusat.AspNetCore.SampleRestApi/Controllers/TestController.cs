using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.SampleRestApi.Models.HelloWorld;

namespace Nexusat.AspNetCore.SampleRestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [Produces(typeof(Response))]
        [HttpGet]
        public ActionResult Get()
        {
            return Accepted("ciccio", new Response { Greetings = "Hello World" });
        }
    }
}