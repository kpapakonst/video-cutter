using Moq;
using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Services;

namespace VideoCutter.Core.Tests;

public class ClipFilePathBuilderTests
{
    private ClipFilePathBuilder _builder = null!;

    [SetUp]
    public void Setup()
    {
        _builder = new ClipFilePathBuilder();
    }

    [Test]
    public void ShouldReturnTheExpectedOutputFilePath()
    {
        // Arrange
        const string inputFilePath = @"c:\sample\path\to\video.mp4";
        
        // Act
        var result = _builder.GetOutputFilePath(inputFilePath, 3);

        // Assert
        Assert.That(result, Is.EqualTo(@"c:\sample\path\to\clips\video_4.mp4"));
    }
}