public class PipeLineState
{
    public State State { get; set; }
    public List<JobState> JobStates { get; set; } = new List<JobState>();
    public DateTime Date { get; set; } = DateTime.Now;
}
