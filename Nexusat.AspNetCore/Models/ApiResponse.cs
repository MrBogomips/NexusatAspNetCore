﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Mvc.Formatters;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Basic API response without any payload
/// </summary>
public class ApiResponse : ActionResult, IApiResponse
{
    public Status Status { get;}
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExceptionInfo Exception { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ValidationErrorsInfo ValidationErrors { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RuntimeInfo Runtime { get; set; }

    /// <summary>
    /// For responses for wich the body will not be produced.
    /// </summary>
    /// <value><c>true</c> if has body; otherwise, <c>false</c>.</value>
    [JsonIgnore]
    public bool HasBody { get; set; } = true;

    public ApiResponse(Status status)
        => Status = status ?? throw new ArgumentNullException(nameof(status));

    public ApiResponse(int httpCode, string statusCode = null, string description = null, string userDescription = null)
        : this(new Status(httpCode, statusCode, description, userDescription)) { }

    protected NexusatAspNetCoreOptions GetAspNetCoreOptions(HttpContext httpContext) {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }
        return httpContext.RequestServices.GetRequiredService<IOptions<NexusatAspNetCoreOptions>>().Value;
    }


    /// <inheritdoc />
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var services = context.HttpContext.RequestServices;
        var executor = services.GetRequiredService<ApiResponseExecutor>();
        await executor.ExecuteAsync(context, this).ConfigureAwait(false);
    }

    /// <summary>
    /// This method is called before the formatter writes to the output stream.
    /// </summary>
    public virtual void OnFormatting(ActionContext context) {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        var response = context.HttpContext.Response;
        response.StatusCode = Status.HttpCode;
    }
}