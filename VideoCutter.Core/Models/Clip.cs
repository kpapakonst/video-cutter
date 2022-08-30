namespace VideoCutter.Core.Models;

public record Clip(TimeSpan Start, TimeSpan End)
{
    public TimeSpan Duration => End - Start;
}