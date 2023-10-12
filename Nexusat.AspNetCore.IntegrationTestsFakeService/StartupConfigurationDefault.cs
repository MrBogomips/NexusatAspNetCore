using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nexusat.AspNetCore.IntegrationTestsFakeService;

public class StartupConfigurationDefault
{
    public StartupConfigurationDefault(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMvc()
            .AddNexusatAspNetCore();
            
        services.Configure<JsonSerializerOptions>(o => {
            o.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseNexusatAspNetCoreExceptionHandling();
        //app.UseMvc();
        app.UseRouting();
        app.UseEndpoints(options => options.MapControllers());
        app.UseNexusatAspNetCore();
    }
}