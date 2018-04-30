using System;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc.Formatters.Internals;

namespace Nexusat.AspNetCore.Mvc.Formatters
{
    
    /// <summary>
    /// Executes a <see cref="IApiResponse"/> to write to the response.
    /// </summary>
    public class ApiResponseExecutor
    {
        private static readonly string DefaultContentType = new MediaTypeHeaderValue("application/json")
        {
            Encoding = Encoding.UTF8
        }.ToString();

        private readonly IArrayPool<char> _charPool;

        /// <summary>
        /// Creates a new <see cref="JsonResultExecutor"/>.
        /// </summary>
        /// <param name="writerFactory">The <see cref="IHttpResponseStreamWriterFactory"/>.</param>
        /// <param name="logger">The <see cref="ILogger{JsonResultExecutor}"/>.</param>
        /// <param name="options">The <see cref="IOptions{MvcJsonOptions}"/>.</param>
        /// <param name="charPool">The <see cref="ArrayPool{Char}"/> for creating <see cref="T:char[]"/> buffers.</param>
        public ApiResponseExecutor(
            IHttpResponseStreamWriterFactory writerFactory,
            ILogger<ApiResponseExecutor> logger,
            IOptions<MvcJsonOptions> options,
            ArrayPool<char> charPool)
        {
            if (writerFactory == null)
            {
                throw new ArgumentNullException(nameof(writerFactory));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (charPool == null)
            {
                throw new ArgumentNullException(nameof(charPool));
            }

            WriterFactory = writerFactory;
            Logger = logger;
            Options = options.Value;
            _charPool = new JsonArrayPool<char>(charPool);
        }

        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the <see cref="MvcJsonOptions"/>.
        /// </summary>
        protected MvcJsonOptions Options { get; }

        /// <summary>
        /// Gets the <see cref="IHttpResponseStreamWriterFactory"/>.
        /// </summary>
        protected IHttpResponseStreamWriterFactory WriterFactory { get; }

        /// <summary>
        /// Executes the <see cref="JsonResult"/> and writes the response.
        /// </summary>
        /// <param name="context">The <see cref="ActionContext"/>.</param>
        /// <param name="apiResponse">The <see cref="JsonResult"/>.</param>
        /// <returns>A <see cref="Task"/> which will complete when writing has completed.</returns>
        public virtual Task ExecuteAsync(ActionContext context, ApiResponse apiResponse)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (apiResponse == null)
            {
                throw new ArgumentNullException(nameof(apiResponse));
            }

            var httpContext = context.HttpContext;

            var response = httpContext.Response;

            Internals.ResponseContentTypeHelper.ResolveContentTypeAndEncoding(
                response.ContentType,
                DefaultContentType,
                out var resolvedContentType,
                out var resolvedContentTypeEncoding);

            response.ContentType = resolvedContentType;

            apiResponse.OnFormatting(context);

            var serializerSettings = Options.SerializerSettings;

            //Logger.JsonResultExecuting(result.Value);
            if (apiResponse.HasBody)
            {
                using (var writer = WriterFactory.CreateWriter(response.Body, resolvedContentTypeEncoding))
                {
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        jsonWriter.ArrayPool = _charPool;
                        jsonWriter.CloseOutput = false;
                        jsonWriter.AutoCompleteOnClose = false;

                        var jsonSerializer = JsonSerializer.Create(serializerSettings);
                        jsonSerializer.Serialize(jsonWriter, apiResponse);
                    }
                }
            }

            return Task.CompletedTask;
        }
        /// <summary>
        /// Helper method to produce a response with a an HttpContext only
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <param name="apiResponse">Result.</param>
        public void RenderResponse(HttpContext httpContext, ApiResponse apiResponse, RouteData routeData = null, ActionDescriptor actionDescriptor = null, ModelStateDictionary modelStateDictionary = null) {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(HttpContext));
            }
            if (apiResponse == null)
            {
                throw new ArgumentNullException(nameof(apiResponse));
            }
            ActionContext actionContext = new ActionContext(httpContext, routeData ?? new RouteData(), actionDescriptor ?? new ActionDescriptor(), modelStateDictionary ?? new ModelStateDictionary());
            ExecuteAsync(actionContext, apiResponse);
        }
    }
}
