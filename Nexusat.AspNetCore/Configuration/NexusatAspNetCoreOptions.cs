using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Configuration
{
    /// <summary>
    /// Nexusat ASP net core options.
    /// </summary>
    public class NexusatAspNetCoreOptions
    {
		/*
        /// <summary>
        /// Gets or sets the default ok status code.
        /// This status code is set whenever using a response method without
        /// providing a custom code and the semanthic of that method can be
        /// safely assumed to be «OK»
        /// </summary>
        /// <value>The default ok status code.</value>
        public string DefaultOkStatusCode { get; internal set; } = CommonStatusCodes.OK_DEFAULT;
        /// <summary>
        /// Gets or sets the default ko status code.
        /// This status code is set whenever using a response method without
        /// providing a custom code and the semanthic of that method can be
        /// safely assumed to be «KO»
        /// </summary>
        /// <value>The default ko status code.</value>
        public string DefaultKoStatusCode { get; internal set; } = CommonStatusCodes.KO_DEFAULT;
        /// <summary>
        /// Gets or sets the default unset status code.
        /// This status code is set whenever using a response method without
        /// providing a custom code and the semanthic of that method cannot be
        /// safely assumed to be «OK» or «KO».
        /// We suggest, for defensive approach, to use a «KO» code for this case.
        /// </summary>
        /// <value>The default unset status code.</value>
        public string DefaultUnsetStatusCode { get; internal set; } = CommonStatusCodes.UNK_DEFAULT;
        */


        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:Nexusat.AspNetCore.Configuration.NexusatAspNetCoreOptions"/> is runtime profilation enabled.
        /// </summary>
        /// <value><c>true</c> if is runtime profilation enabled; otherwise, <c>false</c>.</value>
        public bool IsRuntimeProfilationEnabled { get; internal set; } = Defaults.IsRuntimeProfilationEnabled;

        /// <summary>
        /// Gets or sets the name of the pagination page index used to parse the request querystring
        /// </summary>
        /// <value>The name of the pagination page index. Default is <code>pageIndex</code></value>
        public string PaginationPageIndexName { get; internal set; } = Defaults.PaginationPageIndexName;

        /// <summary>
        /// Gets or sets the name of the pagination page size used to parse the request querystring
        /// </summary>
        /// <value>The name of the pagination page size. Default is <code>pageSize</code></value>
        public string PaginationPageSizeName { get; internal set; } = Defaults.PaginationPageSizeName;

        /// <summary>
        /// Gets or sets the size of the pagination default page.
        /// </summary>
        /// <value>The size of the pagination default page. Default is <code>10</code></value>
        public int PaginationDefaultPageSize { get; internal set; } = Defaults.PaginationDefaultPageSize;

        /// <summary>
        /// Gets or sets the size of the pagination default max page.
        /// </summary>
        /// <value>The size of the pagination default max page.</value>
        public int PaginationDefaultMaxPageSize { get; internal set; } = Defaults.PaginationDefaultMaxPageSize;

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:Nexusat.AspNetCore.Configuration.NexusatAspNetCoreOptions"/> pagination default bad request on
        /// invalid request.
        /// </summary>
        /// <value><c>true</c> will block an page request too large generating a Bad Request response; otherwise, <c>false</c> will
        /// imply the maximum value allowed for the request</value>
        public bool PaginationDefaultBadRequestOnPageSizeOutOfRange { get; internal set; } = Defaults.PaginationDefaultBadRequestOnPageSizeOverflow;
    }
}
