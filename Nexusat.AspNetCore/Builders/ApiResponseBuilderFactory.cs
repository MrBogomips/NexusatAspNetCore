using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Builders
{
    internal class ApiResponseBuilderFactory : IApiResponseBuilderFactory
    {
        /// <summary>
        /// Returns a builder on a fresh instance
        /// </summary>
        /// <returns>The API enum response builder.</returns>
        /// <typeparam name="T">The payload data type</typeparam>
        public IApiEnumResponseBuilder<T> GetApiEnumResponseBuilder<T>()
        => GetApiEnumResponseBuilder(new ApiEnumResponse<T>());

        /// <summary>
        /// Returns a builder on a fresh instance
        /// </summary>
        /// <returns>The API object response builder.</returns>
        /// <typeparam name="T">The payload data type</typeparam>
        public IApiObjectResponseBuilder<T> GetApiObjectResponseBuilder<T>()
        => GetApiObjectResponseBuilder(new ApiObjectResponse<T>());

        /// <summary>
        /// Returns a builder on a fresh instance
        /// </summary>
        /// <returns>The API response builder.</returns>
        public IApiResponseBuilder GetApiResponseBuilder()
        => GetApiResponseBuilder(new ApiResponse());

        /// <summary>
        /// Returns a builder on a pre-allocated instance
        /// </summary>
        /// <returns>The API enum response builder.</returns>
        /// <typeparam name="T">The payload data type</typeparam>
        public IApiEnumResponseBuilder<T> GetApiEnumResponseBuilder<T>(IApiEnumResponse<T> obj)
        => new ApiEnumResponseBuilder<T>(obj);

        /// <summary>
        /// Returns a builder on a pre-allocated instance
        /// </summary>
        /// <returns>The API object response builder.</returns>
        /// <typeparam name="T">The payload data type</typeparam>
        public IApiObjectResponseBuilder<T> GetApiObjectResponseBuilder<T>(IApiObjectResponse<T> obj)
        => new ApiObjectResponseBuilder<T>(obj);

        /// <summary>
        /// Returns a builder on a pre-allocated instance
        /// </summary>
        /// <returns>The API response builder.</returns>
        public IApiResponseBuilder GetApiResponseBuilder(IApiResponse obj)
        => new ApiResponseBuilder(obj);

    }
}
