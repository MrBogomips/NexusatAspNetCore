﻿using Nexusat.AspNetCore.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class NexusatAspNetCoreMvcBuilderExtensions
{
    private static readonly Action<NexusatAspNetCoreOptionsBuilder> DefaultSetupAction = (builder) => { };

    /// <summary>
    /// Adds the nexusat ASP net core to the MVC pipeline.
    /// </summary>
    /// <returns>The MVC builder</returns>
    /// <param name="mvcBuilder">Mvc builder.</param>
    /// <param name="setupAction">Setup action.</param>
    public static IMvcBuilder AddNexusatAspNetCore(this IMvcBuilder mvcBuilder, Action<NexusatAspNetCoreOptionsBuilder> setupAction = null)
    {
        var builder = new NexusatAspNetCoreSystemBuilder(mvcBuilder, setupAction ?? DefaultSetupAction);

        builder
            .AddSystemService()
            .AddFrameworkServices();

        return mvcBuilder;
    }


}