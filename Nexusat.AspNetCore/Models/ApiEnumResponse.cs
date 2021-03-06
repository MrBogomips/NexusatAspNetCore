﻿using Newtonsoft.Json;
using Nexusat.AspNetCore.Models;
using static Nexusat.AspNetCore.Utils.StringFormatter;
using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Properties;
using Microsoft.AspNetCore.Http;
using Nexusat.AspNetCore.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Nexusat.AspNetCore.Models
{
    public class ApiEnumResponse<T> : ApiResponse, IApiEnumResponse<T>
    {
        public IEnumerable<T> Data { get; }
        public PaginationInfo Navigation { get; }

        public ApiEnumResponse(Status status, PaginationCursor current, int itemsCount, IEnumerable<T> data) : base(status) {
            Data = data;
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            if (itemsCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemsCount), FormatSystemMessage(ExceptionMessages.InvalidItemsCount));
            }
            Navigation = new PaginationInfo
            {
                PaginationCursor = current,
                ItemsCount = itemsCount
            };
        }
        public ApiEnumResponse(Status status, PaginationCursor current, bool hasNextPage, IEnumerable<T> data) : base(status)
        {
            Data = data;
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            Navigation = new PaginationInfo
            {
                PaginationCursor = current,
                HasNextPage = hasNextPage
            };
        }

        public ApiEnumResponse(int httpCode, PaginationCursor current, int itemsCount, IEnumerable<T> data, string statusCode = null, string description = null, string userDescription = null)
            : this(new Status(httpCode, statusCode, description, userDescription), current, itemsCount, data) { }

        public ApiEnumResponse(int httpCode, PaginationCursor current, bool hasNextPage, IEnumerable<T> data, string statusCode = null, string description = null, string userDescription = null)
            : this(new Status(httpCode, statusCode, description, userDescription), current, hasNextPage, data) { }

		public override void OnFormatting(ActionContext context)
		{
            base.OnFormatting(context);

            NexusatAspNetCoreOptions options = GetAspNetCoreOptions(context.HttpContext);

            Navigation?.SetLinks(options, context.HttpContext);
		}
	}
}
