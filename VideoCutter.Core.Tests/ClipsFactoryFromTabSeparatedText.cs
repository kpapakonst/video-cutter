using VideoCutter.Core.Models;
using VideoCutter.Core.Services;

namespace VideoCutter.Core.Tests;

public class ClipsFactoryFromTabSeparatedTextTests
{
    [Test]
    public void ShouldNotBuildAnyClipsOnEmptyText()
    {
        // Arrange
        var factory = new ClipsFactoryFromTabSeparatedText(string.Empty);

        // Act
        var result = factory.Build();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ShouldBuildTheExpectedClipsWhenTextHasASingleRange()
    {
        // Arrange
        const string text = "00:01:12\t00:02:21";
        var factory = new ClipsFactoryFromTabSeparatedText(text);
        var expectedClip = new Clip(new TimeSpan(0, 0, 1, 12), new TimeSpan(0, 0, 2, 21));
        
        // Act
        var clips = factory.Build();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(clips, Has.Length.EqualTo(1));
            Assert.That(clips[0], Is.EqualTo(expectedClip));
        });
    }

    [Test]
    public void ShouldBuildTheExpectedClipsWhenTextHasMultipleRanges()
    {
        // Arrange
        var text = $"00:01:12\t00:02:21{Environment.NewLine}00:00:12\t00:00:21";
        var factory = new ClipsFactoryFromTabSeparatedText(text);

        // Act
        var clips = factory.Build();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(clips, Has.Length.EqualTo(2));
            Assert.That(clips, Is.EquivalentTo(new[]
            {
                new Clip(new TimeSpan(0, 0, 1, 12), new TimeSpan(0, 0, 2, 21)),
                new Clip(new TimeSpan(0, 0, 0, 12), new TimeSpan(0, 0, 0, 21))
            }));
        });
    }
}