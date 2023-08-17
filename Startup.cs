using System.Threading.Channels;
using BashScriptRunner.HostedServices;
using BashScriptRunner.Service;
using Microsoft.AspNetCore.HttpOverrides;

namespace BashScriptRunner;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton(Channel.CreateUnbounded<JobTask>());
        services.AddSingleton<ScriptState>();
        services.AddSingleton<PipelineState>();
        services.AddHostedService<JobService>();
        services.AddScoped<ProjectService>();
        services.AddSignalR();
        services.AddSingleton(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if (env.IsDevelopment())
        {
            app.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
            });
        }
    }
}
