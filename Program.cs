using System.Threading.Channels;
using BashScriptRunner;
using BashScriptRunner.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<BashScriptBackgroundService>();
builder.Services.AddSingleton<Channel<ScriptTask>>(Channel.CreateUnbounded<ScriptTask>());
builder.Services.AddSingleton<ScriptState>();
builder.Services.AddHostedService<BashScriptBackgroundService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy  =>
    {
        policy.WithOrigins("http://localhost:8080");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ScriptStateHub>("/scriptStateHub");
});


app.Run();
