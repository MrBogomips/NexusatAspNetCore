using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.IntegrationTests.Controllers
{
    [Route("/")]
    class HelloWorldController: Controller
    {
        [HttpGet]
        public string Get() => "Hello World!";
    }
}
