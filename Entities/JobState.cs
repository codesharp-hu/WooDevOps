public class JobState
{
    public int Id { get; set; }
    public int PipelineStateId { get; set; }
    public State State { get; set; } = State.NotStarted;
    public List<string> Messages { get; set; } = new List<string>();
    public DateTime Date { get; set; } = DateTime.UtcNow;
}

public enum State { NotStarted, InProgress, Failed, Succeeded }