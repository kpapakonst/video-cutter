using VideoCutter.Core.Models;

namespace VideoCutter.Core.Interfaces
{
    public interface IVideoEditor
    {
        Task Cut(VideoCutRequest request);
    }
}