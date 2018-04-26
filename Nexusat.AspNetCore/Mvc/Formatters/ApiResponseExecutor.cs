using System;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
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
        /// <param name="result">The <see cref="JsonResult"/>.</param>
        /// <returns>A <see cref="Task"/> which will complete when writing has completed.</returns>
        public virtual Task ExecuteAsync(ActionContext context, IApiResponse result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var httpContext = context.HttpContext;

            RenderResponse(httpContext, result);

            return Task.CompletedTask;
        }

        public void RenderResponse(HttpContext httpContext, IApiResponse result) {
            var response = httpContext.Response;

            Internals.ResponseContentTypeHelper.ResolveContentTypeAndEncoding(
                response.ContentType,
                DefaultContentType,
                out var resolvedContentType,
                out var resolvedContentTypeEncoding);

            response.ContentType = resolvedContentType;

            response.StatusCode = result.Status.HttpCode;

            if (result.Location != null)
            {
                response.Headers[HeaderNames.Location] = result.Location;
            }

            if (result is IApiEnumResponse) // build Navigation Links
            {
                var _result = result as IApiEnumResponse;
                NexusatAspNetCoreOptions options = 
                    (httpContext.RequestServices.GetService(typeof(IOptions<NexusatAspNetCoreOptions>)) as IOptions<NexusatAspNetCoreOptions>).Value;

                _result.Navigation?.SetLinks(options, httpContext);
            }

            var serializerSettings = Options.SerializerSettings;

            //Logger.JsonResultExecuting(result.Value);
            if (result.HasBody)
            {
                using (var writer = WriterFactory.CreateWriter(response.Body, resolvedContentTypeEncoding))
                {
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        jsonWriter.ArrayPool = _charPool;
                        jsonWriter.CloseOutput = false;
                        jsonWriter.AutoCompleteOnClose = false;

                        var jsonSerializer = JsonSerializer.Create(serializerSettings);
                        jsonSerializer.Serialize(jsonWriter, result);
                    }
                }
            }
        }
    }
}
