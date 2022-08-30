using VideoCutter.Core.Interfaces;

namespace VideoCutter.Core.Services;

public class ClipFilePathBuilder : IOutputFilePathBuilder
{

    public string GetOutputFilePath(string inputFilePath, int index)
    {
        var inputFileDirectory = Path.GetDirectoryName(inputFilePath) ?? string.Empty;
        var inputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
        var inputFileExtension = Path.GetExtension(inputFilePath);
        var outputFileName = $"{inputFileNameWithoutExtension}_{index + 1}{inputFileExtension}";
        return Path.Combine(inputFileDirectory, "clips", outputFileName);
    }
}