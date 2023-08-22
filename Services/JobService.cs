namespace BashScriptRunner.HostedServices
{
    public class JobService : IJobService
    {
        public string Name { get; set; } = "Job";
        public PipelineState State { get; set; } = new PipelineState();
        public void Run()
        {
            Console.WriteLine($"Job {Name} is running.");
        }
    }
}
