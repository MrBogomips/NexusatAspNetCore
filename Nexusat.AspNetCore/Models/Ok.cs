using System;
using System.Collections.Generic;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Models
{
    public static class Ok
    {
        public const string DefaultStatusCode = CommonStatusCodes.OK;
        public const int HttpStatusCode = StatusCodes.Status200OK;

        public class Response : ApiResponse
        {
            public Response() : base(HttpStatusCode, DefaultStatusCode) { }
            public Response(string statusCode, string description = null, string userDescription = null)
                : base(HttpStatusCode, statusCode, description, userDescription)
            {
                if (statusCode == null)
                {
                    throw new ArgumentNullException(nameof(statusCode));
                }
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        }

        public class Object<T> : ApiObjectResponse<T>
        {
            public Object(T data = default(T), string description = null, string userDescription = null)
                : base(HttpStatusCode, DefaultStatusCode, data, description, userDescription) { }
            public Object(string statusCode, T data = default(T), string description = null, string userDescription = null)
                : base(HttpStatusCode, statusCode, data, description, userDescription)
            {
                if (statusCode == null)
                {
                    throw new ArgumentNullException(nameof(statusCode));
                }
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        }

        public class Enum<T> : ApiEnumResponse<T>
        {
            public Enum(IEnumerable<T> data, PaginationCursor current, bool hasNextPage, string description = null, string userDescription = null)
				: base(HttpStatusCode, current, hasNextPage, data, statusCode: DefaultStatusCode, description: description, userDescription: userDescription) { }
            public Enum(string statusCode, IEnumerable<T> data, PaginationCursor current, bool hasNextPage, string description = null, string userDescription = null)
                : base(HttpStatusCode, current, hasNextPage, data, statusCode: statusCode, description: description, userDescription: userDescription)
            {
                if (statusCode == null)
                {
                    throw new ArgumentNullException(nameof(statusCode));
                }
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
            public Enum(IEnumerable<T> data, PaginationCursor current, int itemsCount, string description = null, string userDescription = null)
				: base(HttpStatusCode, current, itemsCount, data, statusCode: DefaultStatusCode, description: description, userDescription: userDescription) { }
            public Enum(string statusCode, IEnumerable<T> data, PaginationCursor current, int itemsCount, string description = null, string userDescription = null)
                : base(HttpStatusCode, current, itemsCount, data, statusCode: statusCode, description: description, userDescription: userDescription)
            {
                if (statusCode == null)
                {
                    throw new ArgumentNullException(nameof(statusCode));
                }
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        }

        
    }
}
