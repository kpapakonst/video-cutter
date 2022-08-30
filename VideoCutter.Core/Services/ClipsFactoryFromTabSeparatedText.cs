using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Models;

namespace VideoCutter.Core.Services;

public class ClipsFactoryFromTabSeparatedText : IClipsFactory
{
    private readonly string _input;
    private const string RangeSeparator = "\t";

    public ClipsFactoryFromTabSeparatedText(string input)
    {
        _input = input;
    }

    public Clip[] Build()
    {
        var lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return lines.Select(CreateClip).ToArray();
    }

    private static Clip CreateClip(string line)
    {
        var range = GetRangeAsText(line);
        var start = ExtractTimeSpanFromText(range.start);
        var end = ExtractTimeSpanFromText(range.end);
        return new Clip(start, end);
    }

    private static (string start, string end) GetRangeAsText(string line)
    {
        var tabParts = line.Split(RangeSeparator);
        if (tabParts.Length != 2)
        {
            throw new InvalidOperationException($"Malformed line: {line}. A single tab is allowed per line.");
        }

        return (tabParts[0], tabParts[1]);
    }

    private static TimeSpan ExtractTimeSpanFromText(string timespanText)
    {
        if (!TimeSpan.TryParse(timespanText, out var result))
        {
            throw new InvalidOperationException($"Malformed clip start/end: {timespanText}");
        }
        return result;
    }
}