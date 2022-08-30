# How to run the sample

In this directory, a [sample mp4 file](atv-road-aerial.mp4) along with a clips text file are provided.
To create the clips defined in [clips](clips.txt), the following commands can be used, after building the solution:

On Windows:

```shell
videocutter -v [path_to_vidsplay-ocean-sunset-28] -c [path_to_clips.txt]
```

On Non Windows:

```shell
dotnet videocutter.dll -v [path_to_vidsplay-ocean-sunset-28] -c [path_to_clips.txt]
```
