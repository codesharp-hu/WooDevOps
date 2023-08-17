public class JobState
{
    public State State { get; set; } = State.NotStarted;
    public List<string> Messages { get; set; } = new List<string>();
    public DateTime Date { get; set; } = DateTime.Now;
}

public enum State { NotStarted, InProgress, Failed, Succeeded }