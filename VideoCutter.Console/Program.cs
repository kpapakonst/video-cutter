// See https://aka.ms/new-console-template for more information

using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VideoCutter.Console;
using VideoCutter.Core.Interfaces;
using VideoCutter.Core.Services;
using VideoCutter.Infrastructure;

Func<IServiceProvider, ClipsFactoryFromTabSeparatedText> BuildClipsFactory(string clipsInputFile)
{
    return _ =>
    {
        var clipsText = File.ReadAllText(clipsInputFile);
        return new ClipsFactoryFromTabSeparatedText(clipsText);
    };
}

IHost BuildHost(CommandLineOptions options)
{
    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
            services
                .AddSingleton<IVideoEditor, FFMpegVideoEditor>()
                .AddSingleton<IClipsFactory>(BuildClipsFactory(options.ClipsFile))
                .AddSingleton<IOutputFilePathBuilder, ClipFilePathBuilder>()
                .AddSingleton<ClipFilesBuilder>())
        .Build();
    return host;
}

await Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsedAsync(options =>
{
    using var host = BuildHost(options);
    var builder = host.Services.GetRequiredService<ClipFilesBuilder>();
    var clipsFactory = host.Services.GetRequiredService<IClipsFactory>();
    var clips = clipsFactory.Build();
    return builder.Process(options.VideoFile, clips);
});



