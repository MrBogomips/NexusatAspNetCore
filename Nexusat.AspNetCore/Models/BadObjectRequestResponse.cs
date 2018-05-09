using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Bad request response with optional <see cref="Data"/> payload.
    /// </summary>
    public class BadObjectRequestResponse<T> : BadRequestResponse, IApiObjectResponse<T>
    {
        public T Data { get; }

        public BadObjectRequestResponse(string statusCode, T data = default(T), string description = null, string userDescription = null)
            : base(statusCode, description, userDescription)
        {
			Data = data;
		}

        public BadObjectRequestResponse(T data = default(T), string description = null, string userDescription = null)
            : base(description, userDescription)
        {
            Data = data;
        }

        public BadObjectRequestResponse(ModelStateDictionary modelState, T data = default(T))
            : base(modelState)
        {
            Data = data;
        }
    }
}
