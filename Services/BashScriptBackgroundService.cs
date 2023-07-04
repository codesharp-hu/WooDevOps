using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace BashScriptRunner;
public class BashScriptBackgroundService : BackgroundService
{

    private Channel<ScriptTask> Channel { get; }
    private Channel<ScriptState> ScriptStateChannel { get; set; }

    public BashScriptBackgroundService(Channel<ScriptTask> channel, Channel<ScriptState> scriptStateChannel)
    {
        Channel = channel;
        ScriptStateChannel = scriptStateChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scriptState = new ScriptState { Running = true, Output = new List<string>() };
        await foreach (var scriptTask in Channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"getInfo.sh",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine($"Output: {e.Data}");
                    scriptState.Output.Add(e.Data);
                    ScriptStateChannel.Writer.TryWrite(scriptState);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine($"Error: {e.Data}");
                }
            };

            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(stoppingToken);
        }
    }
}

public class ScriptTask
{
    public string Script => "hello";
}
public class ScriptState
{
    public bool Running { get; set; }
    public required List<string> Output { get; set; }
}