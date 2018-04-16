using System;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Exceptions
{
    /// <summary>
    /// API response exception.
    /// Any exception derived from this one are intercepted by the framework middleware and
    /// will be translated to an appropriate <see cref="Models.IApiResponse"/>.
    /// </summary>
    [Serializable]
    public class ApiResponseException : Exception, IFrameworkException
    {
        public string StatusCode { get; private set; }
        public string Description { get; set; }
        public string UserDescription { get; set; }
        public int HttpCode { get; private set; }

        public ApiResponseException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.ApiResponseException"/> class.
        /// </summary>
        /// <param name="httpCode">An Http status code.</param>
        /// <param name="statusCode">A valid KO Status code</param>
        public ApiResponseException(int httpCode, string statusCode) : base(HelperBuildMessage(httpCode, statusCode))
        {
            InternalInit(httpCode, statusCode);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.ApiResponseException"/> class.
        /// </summary>
        /// <param name="httpCode">An Http status code.</param>
        /// <param name="statusCode">A valid KO Status code.</param>
        /// <param name="inner">Inner.</param>
        public ApiResponseException(int httpCode, string statusCode, Exception inner) : base(HelperBuildMessage(httpCode, statusCode), inner)
        {
            InternalInit(httpCode, statusCode);
        }

        private void InternalInit(int httpCode, string statusCode)
        {
            if (statusCode == null) throw new ArgumentNullException(nameof(statusCode));
            switch (httpCode / 100)
            {
                case 4:
                case 5:
                case 6:
                    break; // OK
                default:
                    throw new ArgumentException("Invalid httpCode: must me a 4xx, 5xx or 6xx");
            }
            HttpCode = httpCode;
            Models.StatusCode.CheckValidKoCodeOrThrow(statusCode);
            StatusCode = statusCode;
        }
        private static string HelperBuildMessage(int httpCode, string subcode)
        {
            return string.Format("ResponseException with Http {0} and Status Code KO_'{1}'", httpCode, subcode);
        }

        protected ApiResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }    

}
