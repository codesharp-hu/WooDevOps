using System.Diagnostics;

namespace BashScriptRunner.Entities;

    public class Job
    {
        public string? Name { get; set; }
        public PipelineState State { get; set; } = new PipelineState();

        public void Run()
        {
            Console.WriteLine($"Job: {Name} is running.");

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
 
             process.OutputDataReceived += /*async*/ (sender, e) =>
             {
                 if (e.Data != null)
                 {
                     //await hubContext.Clients.All.SendAsync("outputReceived", e.Data);
                     Console.WriteLine($"Output: {e.Data}");
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

            process.WaitForExit();
        }
    }
