public class JobState
{
    public State State { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
    public DateTime Date { get; set; }
}

public enum State { NotStarted, InProgress, Failed, Succeeded }