using System;
using System.Collections.Generic;
using Nexusat.AspNetCore.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Mvc
{
    /// <summary>
    /// API controller base extensions.
    /// </summary>
    public partial class ApiController
    {
        /// <summary>
        /// Produce a generic API Response without a payload
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="httpCode">Http code.</param>
        /// <param name="statusCode">Status code.</param>
        protected IApiResponse Response(int httpCode, string statusCode = null, string description = null, string userDescription = null)
        => ApiResponse(r => {
            r.SetHttpCode(httpCode)
             .SetStatusCode(statusCode ?? FrameworkOptions.DefaultUnsetStatusCode)
             .SetDescription(description)
             .SetUserDescription(userDescription);
        });


        /// <summary>
        /// Produce a generic API Response with, optionally, a payload
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="httpCode">Http code.</param>
        /// <typeparam name="T">The payload type expected by the method's signature</typeparam>
        protected IApiObjectResponse<T> ObjectResponse<T>(int httpCode, string statusCode = null, T data = default(T), string description = null, string userDescription = null) 
        => ApiObjectResponse<T>(r => {
            r.SetHttpCode(httpCode)
             .SetStatusCode(statusCode ?? FrameworkOptions.DefaultUnsetStatusCode)
             .SetDescription(description)
             .SetUserDescription(userDescription)
             .SetData(data);
        });

        protected IApiEnumResponse<T> EnumResponse<T>(int httpCode, string statusCode = null, IEnumerable<T> data = null, string description = null, string userDescription = null) 
        => ApiEnumResponse<T>(r => { 
            r.SetHttpCode(httpCode)
             .SetStatusCode(statusCode ?? FrameworkOptions.DefaultUnsetStatusCode)
             .SetDescription(description)
             .SetUserDescription(userDescription)
             .SetData(data);
        });


        #region Response Helper Methods
        // Ok 200
        protected IApiResponse OkResponse(string description = null, string userDescription = null) 
        => Response(Status200OK, 
                    FrameworkOptions.DefaultOkStatusCode, 
                    description: description, 
                    userDescription: userDescription);
        protected IApiObjectResponse<T> OkObjectResponse<T>(T data = default(T), string description = null, string userDescription = null)
        => ObjectResponse<T>(Status200OK,
                             FrameworkOptions.DefaultOkStatusCode,
                             data: data,
                             description: description,
                             userDescription: userDescription
                            );
        protected IApiEnumResponse<T> OkEnumResponse<T>(IEnumerable<T> data = null, string description = null, string userDescription = null)
        => EnumResponse<T>(Status200OK,
                           FrameworkOptions.DefaultOkStatusCode,
                           data: data,
                             description: description,
                             userDescription: userDescription
                            );

        #endregion Response Helper Methods

    }
}
