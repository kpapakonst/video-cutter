# Video Cutter

Video Cutter is a tool to facilitate and automate the process of cutting of a video file into multiple clips.

## Description

Video Cutter expects a text file as input, containing time ranges to build clips for a video file.
This text file should define a single time range in each line.
Time ranges should follow the following format `hh:mm:ss   hh:mm:ss` to define the start and end time separated by tab.
For example to create 2 clips:

- one starting at 00:00:05 and ending at 00:00:09 and
- another one starting at 00:01:03 and ending at  00:01:10

a text file with the following contents would have to be used:

00:00:05    00:00:09
00:01:03    00:01:10

You can find an example clips file [here](sample/clips.txt)

## Getting Started

### Dependencies

- [FFmpeg](https://ffmpeg.org/download.html) has to be installed and added to PATH.
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### How to Build

Video Cutter is implemented in .NET 6.0 / C#.
To build you can either use Visual Studio and build [VideoCutter.sln](VideoCutter.sln) or run the following command from the root directory:

```shell
dotnet build
```

### Executing program

After building the application, to locate the executables, please visit the `VideoCutter.Console\bin\[Configuration]\net6.0` folder.

Video Cutter supports two required command line arguments:

- `v` which should contain the path to the video file to create clips for
- `c` which should contain the path to the text file containing the clips information

On Windows, the following command can be used to start the application:

```shell
videocutter -v "c:\path\to\video.mp4" -c "c:\path\to\clipsfile.txt"
```

On non-Windows but also on Windows environments, you can use the following command to start the application:

```shell
dotnet videocutter.dll -v "path/to/video.mp4" -c "path/to/clipsfile.txt"
```

## Help

To make videocutter print information about the supported command line arguments, you can use the following command: 

On Windows:

```shell
videocutter --help
```

On Non Windows:

```shell
dotnet videocutter.dll --help
```

## Acknowledgments

- [FFmpeg](https://github.com/FFmpeg)
- [Command Line Parser](https://github.com/commandlineparser/commandline)
- [Vidsplay](https://www.vidsplay.com/)