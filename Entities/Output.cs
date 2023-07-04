using System;

public class Output
{
    public string? Text { get; set; }
    public DateTime Timestamp { get; set; }

    public Output(string? text)
    {
        Text = text;
        Timestamp = DateTime.Now;
    }
}