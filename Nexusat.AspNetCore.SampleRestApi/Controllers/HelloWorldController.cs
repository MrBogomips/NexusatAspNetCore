using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Nexusat.AspNetCore.SampleRestApi.Models.HelloWorld;

namespace Nexusat.AspNetCore.SampleRestApi.Controllers
{
    [Route("api/[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public Response Get([FromQuery] string name, [FromQuery] string surname)
        {
            return new Response
            {
                Greetings = string.Format("Hello {0} {1}", name, surname)
            };
        }

        [HttpPost]
        public Response Post([FromBody] Request request)
        {
            return Get(request.Name, request.Surname);
        }






#if DELETED
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
#endif
    }
}
