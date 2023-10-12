using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Models;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Mvc;

public class ValidatePaginationAttribute: ActionFilterAttribute
{
    public ValidatePaginationAttribute()
    {
    }

    /// <summary>
    /// Gets or sets the max size allowed for a page.
    /// Set to 0 to allow any value.
    /// If missing the application wide setting will be used.
    /// </summary>
    /// <value>The size of the max page. Use <code>0 (zero)</code> to mean any value.</value>
    public int MaxPageSize { get; set; } = -1;

    /// <summary>
    /// Gets or sets the default size of the page.
    /// If missing the application wide setting will be used.
    /// </summary>
    /// <value>The default size of the page.</value>
    public int DefaultPageSize { get; set; } = -1;

    /// <summary>
    /// Gets or sets the return bad request on out of range.
    /// If missing the application wide setting will be used.
    /// </summary>
    /// <value><code>true</code> will produce a BadRequest response in case of violation of the limits</value>
    public bool? ReturnBadRequestOnOutOfRange { get; set; } = null;

    public bool IsReusable => false;

    private static void ParsePageCursor(NexusatAspNetCoreOptions options, IQueryCollection query, out int? pageSize, out int? pageIndex) {
        StringValues values;
        pageSize = pageIndex = null;
        int _size, _index;
        if (query.TryGetValue(options.PaginationPageSizeName, out values))
        {
            if (!int.TryParse(values[0], out _size))
            {
                var ex = new BadRequest.Exception("KO_BAD_PAGE_SIZE");
                ex.Description = "Page Size is not a valid integer";
                throw ex;
            }
            if (_size < 0) {
                var ex = new BadRequest.Exception("KO_BAD_PAGE_SIZE");
                ex.Description = "Page Size must be a non negative integer";
                throw ex;
            }
            pageSize = _size;
        }
        if (query.TryGetValue(options.PaginationPageIndexName, out values))
        {
            if (!int.TryParse(values[0], out _index))
            {
                var ex = new BadRequest.Exception("KO_BAD_PAGE_INDEX");
                ex.Description = "Page Index is not a valid integer";
                throw ex;
            }
            if (_index < 1) 
            {
                var ex = new BadRequest.Exception("KO_BAD_PAGE_INDEX");
                ex.Description = "Page Index must be a positive integer";
                throw ex;
            }
                    
            pageIndex = _index;
        }
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<ValidatePaginationAttribute>)) as ILogger<ValidatePaginationAttribute>;
        var ioptions = context.HttpContext.RequestServices.GetService(typeof(IOptions<NexusatAspNetCoreOptions>)) as IOptions<NexusatAspNetCoreOptions>;
        var options = ioptions.Value;

        int? p_size, p_index;

        ParsePageCursor(options, context.HttpContext.Request.Query, out p_size, out p_index);

        // Evaluate if blocking a pagesize outofrange exception
        if (ReturnBadRequestOnOutOfRange ?? options.PaginationDefaultBadRequestOnPageSizeOutOfRange)
        {
            var maxPageSize = MaxPageSize != -1 ? MaxPageSize : options.PaginationDefaultMaxPageSize;
            logger.LogDebug("MaxPageSize allowed: {0}", maxPageSize);

            if (maxPageSize > 0 && p_size.HasValue && p_size.Value > maxPageSize)
            {
                var ex = new BadRequest.Exception("KO_PAGE_SIZE_OUT_OF_RANGE");
                ex.Description = FormatSystemMessage("Page Size {0} is greather than the maximum allowed ({1})", p_size.Value, maxPageSize);
                throw ex;
            }
        }

        int actual_p_index = p_index ?? 1;
        int actual_p_size = p_size ?? (DefaultPageSize != -1 ? DefaultPageSize : options.PaginationDefaultPageSize);
        logger.LogDebug("Actual page cursor: Index({0}), Size({1})", actual_p_size, actual_p_size);

        // Set Pagination Cursor for the current request
        context.HttpContext.Items[InternalConstants.PaginationCursorKey] = new PaginationCursor(actual_p_index, actual_p_size);
    }
}