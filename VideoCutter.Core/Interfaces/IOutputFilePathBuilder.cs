namespace VideoCutter.Core.Interfaces;

public interface IOutputFilePathBuilder
{
    string GetOutputFilePath(string inputFilePath, int index);
}