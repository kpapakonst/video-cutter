namespace VideoCutter.Core.Models;

public record VideoCutRequest(string InputFilePath, string OutputFilePath, Clip Clip);