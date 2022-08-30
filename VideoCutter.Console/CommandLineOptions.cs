using CommandLine;

namespace VideoCutter.Console;

public class CommandLineOptions
{
    [Option('v',"video", Required = true, HelpText = "The path to the video file")]
    public string VideoFile { get; set; }

    [Option(shortName:'c', "clips", Required = true, HelpText = "The path to the clips file")]
    public string ClipsFile { get; set; }
}