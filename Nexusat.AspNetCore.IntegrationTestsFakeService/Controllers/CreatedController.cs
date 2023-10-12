using System;
using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers;

[Route("Created")]
public class CreatedController : ApiController
{
	[HttpGet("index/{id:int}", Name = "created_controller_index")]
	public IApiObjectResponse<string> Get(int id) => CreatedObject(id);

	[HttpPost("{id:int}")]
	public IApiResponse CreatedResponse(int id) 
		=> new Created.ResponseAtUri(uri: "/Created/" + id.ToString());

	[HttpPost("object/{id:int}")]
	public IApiObjectResponse<string> CreatedObject(int id) {
		Uri uri;
		Uri.TryCreate("/Created/" + id.ToString(), UriKind.Relative, out uri);
		return new Created.ObjectAtUri<string>(data: "data: " + id.ToString(), uri: uri);
	}

	[HttpPost("action/{id:int}")]
	public IApiResponse CreatedAtActionFake(int id)
		=> new Created.ResponseAtAction("Get", routeValues: new { id = id });

	[HttpPost("route/{id:int}")]
	public IApiResponse CreatedRouteFake(int id)
		=> new Created.ResponseAtRoute("created_controller_index", routeValues: new { id = id });
}