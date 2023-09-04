namespace BashScriptRunner.Entities
{
    public class Job : IJobService
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public PipelineState State { get; set; } = new PipelineState();
        public void Run()
        {
            Console.WriteLine($"JobService: {Name} is running.");
        }
    }
}
