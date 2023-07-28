public class PipelineState
{
    public State State { get; set; } = State.NotStarted;
    public List<JobState> JobStates { get; set; } = new List<JobState>();
    public DateTime Date { get; set; } = DateTime.Now;
}
