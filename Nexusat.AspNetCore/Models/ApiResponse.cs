using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Basic API response without any payload
    /// </summary>
    public class ApiResponse : ActionResult, IApiResponse
    {
        public Status Status { get;}
        public ExceptionInfo Exception { get; set; }
        public ValidationErrorsInfo ValidationErrors { get; set; }
        public RuntimeInfo Runtime { get; set; }

        /// <summary>
        /// For responses for wich the body will not be produced.
        /// </summary>
        /// <value><c>true</c> if has body; otherwise, <c>false</c>.</value>
        public bool HasBody { get; set; } = true;
        public virtual string Location { get; set; }

        public ApiResponse(Status status)
           => Status = status ?? throw new ArgumentNullException(nameof(status));

        public ApiResponse(int httpCode, string statusCode = null, string description = null, string userDescription = null)
            : this(new Status(httpCode, statusCode, description, userDescription)) { }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<ApiResponseExecutor>();
            return executor.ExecuteAsync(context, this);
        }
    }
}
