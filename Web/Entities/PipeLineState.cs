public class PipelineState
{
    public int Id { get; set; }
    public int PipelineDescriptorId { get; set; }
    public State State { get; set; } = State.NotStarted;
    public List<JobState> JobStates { get; set; } = new List<JobState>();
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
