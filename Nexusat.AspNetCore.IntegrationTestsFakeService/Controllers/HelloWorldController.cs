using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers
{
    [Route("/hello_world")]
    public class HelloWorldController
    {
        [HttpGet]
        public string Get() => "Hello World!";
    }
}
