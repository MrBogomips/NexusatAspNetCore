using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;
using Nexusat.AspNetCore.SampleRestApi.Models.HelloWorld;

namespace Nexusat.AspNetCore.SampleRestApi.Controllers;

[Route("api/[controller]")]
public class HelloWorldController : ApiController
{
    /// <summary>
    /// Returns a greeting 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <returns></returns>
    [HttpGet]
    public IApiObjectResponse<Response> Get([FromQuery] string name, [FromQuery] string surname)
    {
        return new Ok.Object<Response>();

           
        /*
        return responseBuilder
            .SetData(new Response
            {
                Greetings = string.Format("Hello {0} {1}", name, surname)
            })
            .SetHttpCode(200)
            .SetStatusCodeSuccess("Hello")
            .Build();
            */
    }

        
    [HttpGet("simpleGreeting")]
    public IApiObjectResponse<string> GetSimpleGreeting([FromQuery] string name, [FromQuery] string surname)
    {
        throw new NotImplementedException();
        /*
        var responseFactory = new ApiResponseBuilderFactory();
        var responseBuilder = responseFactory.GetApiObjectResponseBuilder<string>();

        return responseBuilder
            .SetData(string.Format("Hello {0} {1}", name, surname))
            .SetHttpCode(200)
            .SetStatusCodeSuccess("Hello")
            .Build();
            */
    }

        
    [HttpGet("objectGreeting")]
    public IApiObjectResponse<object> GetObjectGreeting([FromQuery] string name, [FromQuery] string surname)
    {
        throw new NotImplementedException();
        /*
        var responseFactory = new ApiResponseBuilderFactory();
        var responseBuilder = responseFactory.GetApiObjectResponseBuilder<object>();

        return responseBuilder
            .SetData(new {
                Name = name, Surname = name
            })
            .SetHttpCode(200)
            .SetStatusCodeSuccess("Hello")
            .Build();
            */
    }


    [HttpGet("manyGreetings")]
    public IApiEnumResponse<Response> GetMany([FromQuery] string name, [FromQuery] string surname)
    {
        throw new NotImplementedException();

        /*
        var responseFactory = new ApiResponseBuilderFactory();
        var responseBuilder = responseFactory.GetApiEnumResponseBuilder<Response>();

        return responseBuilder
            .SetData(new[] {new Response
            {
                Greetings = string.Format("Hello {0} {1}", name, surname)
            }})
            .SetHttpCode(200)
            .SetStatusCodeSuccess("Hello")
            .Build();
            */
    }

    [HttpPost]
    public Response Post([FromBody] Request request)
    {
        throw new NotImplementedException();
        // return Get(request.Name, request.Surname);
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