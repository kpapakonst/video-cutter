using Moq;
using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Models;
using VideoCutter.Core.Services;

namespace VideoCutter.Core.Tests
{
    public class ClipFilesBuilderTests
    {
        private ClipFilesBuilder _clipFilesBuilder = null!;
        private readonly Mock<IVideoEditor> _videoCutter = new();
        private readonly Mock<IOutputFilePathBuilder> _outputFilePathBuilder = new();

        [SetUp]
        public void Setup()
        {
            _clipFilesBuilder = new ClipFilesBuilder(_videoCutter.Object, _outputFilePathBuilder.Object);
        }

        [Test]
        public async Task ShouldMakeTheExpectedVideoCutRequests()
        {
            // Arrange
            const string inputFilePath = @"c:\sample\path\to\video.mp4";
            var clips = new[]
            {
                new Clip(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(7)),
                new Clip(TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(33)),
            };
            
            _outputFilePathBuilder.Setup(o => o.GetOutputFilePath(inputFilePath, It.IsAny<int>()))
                .Returns<string, int>((_, index) => $@"c:\sample\clips\clip{index + 1}.mp4");
            
            var expectedVideoCutRequest1 = new VideoCutRequest(inputFilePath, @"c:\sample\clips\clip1.mp4", clips[0]);
            var expectedVideoCutRequest2 = new VideoCutRequest(inputFilePath, @"c:\sample\clips\clip2.mp4", clips[1]);

            // == Equality check will work, as VideoCutRequest is a record
            Func<VideoCutRequest, VideoCutRequest, bool> matchesExpectedVideoRequest = (received, expected) => received == expected;

            // Act
            await _clipFilesBuilder.Process(inputFilePath, clips);

            // Assert
            Assert.Multiple(() =>
            {
                
                _videoCutter.Verify(
                    v => v.Cut(It.Is<VideoCutRequest>(_ => matchesExpectedVideoRequest(_, expectedVideoCutRequest1))),
                    Times.Once());
                _videoCutter.Verify(
                    v => v.Cut(It.Is<VideoCutRequest>(_ => matchesExpectedVideoRequest(_, expectedVideoCutRequest2))),
                    Times.Once());
                _videoCutter.VerifyNoOtherCalls();
            });
        }
    }
}