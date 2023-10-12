using Microsoft.AspNetCore.Mvc;
using Nexusat.AspNetCore.IntegrationTestsFakeService.Models;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService.Controllers;

[Route("BadRequest")]
public class BadRequestController : ApiController
{
	[HttpPost("ModelStateManualValidation")]
	public IApiObjectResponse<string> PostModelStateManualValidation([FromBody] HelloWorldRequest request) {
		if (!ModelState.IsValid)
		{
			throw new BadRequest.Exception(ModelState);
		}

		return new Ok.Object<string>(data: $"Hello {request.Name} {request.Surname}");
	}

	[HttpPost("ModelStateAutoValidation")]
	[ValidateRequest]
	public IApiObjectResponse<string> ModelStateAutoValidation([FromBody] HelloWorldRequest request)
	{         
		return new Ok.Object<string>(data: $"Hello {request.Name} {request.Surname}");
	}
}