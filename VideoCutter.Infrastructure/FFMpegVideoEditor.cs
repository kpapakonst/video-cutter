using FFMpegCore;
using FFMpegCore.Enums;
using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Models;

namespace VideoCutter.Infrastructure
{
    public class FFMpegVideoEditor : IVideoEditor
    {
        public async Task Cut(VideoCutRequest request)
        {
            CreateOutputFolderIfNotExists(request.OutputFilePath);
            await CutWithFFMpeg(request);
        }

        private static async Task CutWithFFMpeg(VideoCutRequest request)
        {
            await FFMpegArguments
                .FromFileInput(request.InputFilePath)
                .OutputToFile(request.OutputFilePath, true,
                    options => options.Seek(request.Clip.Start)
                        .WithDuration(request.Clip.Duration)
                        .WithVideoFilters(filterOptions => filterOptions
                            .Scale(VideoSize.FullHd)))
                .ProcessAsynchronously();
        }

        private static void CreateOutputFolderIfNotExists(string outputFilePath)
        {
            var path = Path.GetDirectoryName(outputFilePath);
            if (path == null || Directory.Exists(path))
            {
                return;
            }
            Directory.CreateDirectory(path);
        }
    }
}