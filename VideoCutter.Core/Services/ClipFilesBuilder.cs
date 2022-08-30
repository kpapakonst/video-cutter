using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Models;

namespace VideoCutter.Core.Services;

public class ClipFilesBuilder
{
    private readonly IVideoEditor _videoEditor;
    private readonly IOutputFilePathBuilder _outputFilePathBuilder;

    public ClipFilesBuilder(IVideoEditor videoEditor, IOutputFilePathBuilder outputFilePathBuilder)
    {
        _videoEditor = videoEditor;
        _outputFilePathBuilder = outputFilePathBuilder;
    }

    public async Task Process(string inputFilePath, Clip[] clips)
    {
        var requests = clips.Select((clip, index) => BuildVideoCutRequest(inputFilePath, index, clip));
        var tasks = requests.Select(request => _videoEditor.Cut(request));
        await Task.WhenAll(tasks);
    }

    private VideoCutRequest BuildVideoCutRequest(string inputFilePath, int index, Clip clip)
    {
        var outputFilePath = _outputFilePathBuilder.GetOutputFilePath(inputFilePath, index);
        return new VideoCutRequest(inputFilePath, outputFilePath, clip);
    }
}