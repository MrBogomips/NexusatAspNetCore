using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nexusat.AspNetCore.Implementations
{
    /// <summary>
    /// Basic API response without any payload
    /// </summary>
    internal class ApiResponse : ActionResult, IApiResponse, IApiResponseInternal
    {
        public Status Status { get; set; } = new Status();
        public ExceptionInfo Exception { get; set; }
        public ValidationErrorsInfo ValidationErrors { get; set; }
        public RuntimeInfo Runtime { get; set; }

        /// <summary>
        /// For responses for wich the body will not be produced.
        /// </summary>
        /// <value><c>true</c> if has body; otherwise, <c>false</c>.</value>
        public bool HasBody { get; set; } = true;
        public virtual string Location { get; set; }

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
