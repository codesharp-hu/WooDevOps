namespace BashScriptRunner.HostedServices
{
    public class JobService : IJobService
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Job";
        public PipelineState State { get; set; } = new PipelineState();
        public void Run()
        {
            Console.WriteLine($"Job {Name} is running.");
        }
    }
}
