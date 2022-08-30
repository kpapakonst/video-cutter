using VideoCutter.Core.Models;

namespace VideoCutter.Core.Interfaces;

public interface IClipsFactory
{
    Clip[] Build();
}