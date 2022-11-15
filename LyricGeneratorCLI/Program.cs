using System.CommandLine;
using LyricGeneratorLib;

namespace LyricGeneratorCLI
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Option<int> spaceOpt = new(
                name: "--space",
                description: "Sets the space between the bottom of the lyrics and the bottom of the generated image (bottom padding).",
                getDefaultValue: () => LyricGenerator.DEFAULT_BOTTOM_SPACE);
            Option<int> heightOpt = new(
                name: "--height",
                description: "Sets the height of the generated image.",
                getDefaultValue: () => LyricGenerator.DEFAULT_HEIGHT);
            Option<int> widthOpt = new(
                name: "--width",
                description: "Sets the width of the generated image.",
                getDefaultValue: () => LyricGenerator.DEFAULT_WIDTH);
            Option<byte> dRedOpt = new(
                name: "--draw-red",
                description: "Sets the RED draw color value for when a lyric is highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_DRAW_R);
            Option<byte> fRedOpt = new(
                name: "--fade-red",
                description: "Sets the RED draw color value for when a lyric is not yet highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_FADE_R);
            Option<byte> dGreenOpt = new(
                name: "--draw-green",
                description: "Sets the GREEN draw color value for when a lyric is highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_DRAW_G);
            Option<byte> fGreenOpt = new(
                name: "--fade-green",
                description: "Sets the GREEN draw color value for when a lyric is not yet highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_FADE_G);
            Option<byte> dBlueOpt = new(
                name: "--draw-blue",
                description: "Sets the BLUE draw color value for when a lyric is highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_DRAW_B);
            Option<byte> fBlueOpt = new(
                name: "--fade-blue",
                description: "Sets the BLUE draw color value for when a lyric is not yet highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_FADE_B);
            Option<byte> dAlphaOpt = new(
                name: "--draw-alpha",
                description: "Sets the ALPHA draw color value for when a lyric is highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_DRAW_A);
            Option<byte> fAlphaOpt = new(
                name: "--fade-alpha",
                description: "Sets the ALPHA draw color value for when a lyric is not yet highlighted.",
                getDefaultValue: () => LyricGenerator.DEFAULT_FADE_A);
            Option<FileInfo?> pathOpt = new(
                name: "--output",
                description: "Path to create output archive file.",
                isDefault: true,
                parseArgument: result =>
                {
                    try
                    {
                        string filePath = result.Tokens.Single().Value;

                        if (File.Exists(filePath))
                        {
                            result.ErrorMessage = "File exists already. Will not overwrite.";
                            return null;
                        }

                        return new FileInfo(filePath);
                    }
                    catch (Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
                    {
                        result.ErrorMessage = "Invalid input for path.";
                        return null;
                    }
                });

            RootCommand rootCommand = new("Lyric Generator CLI");
            rootCommand.AddOption(spaceOpt);
            rootCommand.AddOption(heightOpt);
            rootCommand.AddOption(widthOpt);
            rootCommand.AddOption(dRedOpt);
            rootCommand.AddOption(fRedOpt);
            rootCommand.AddOption(dGreenOpt);
            rootCommand.AddOption(fGreenOpt);
            rootCommand.AddOption(dBlueOpt);
            rootCommand.AddOption(fBlueOpt);
            rootCommand.AddOption(dAlphaOpt);
            rootCommand.AddOption(fAlphaOpt);
            rootCommand.AddOption(pathOpt);

            rootCommand.SetHandler((fileInfo, argsBinder) =>
            {
                if (fileInfo != null)
                {
                    LyricGenerator generator = new();

                    string lyrics = string.Empty;
                    int input;
                    
                    while ((input = Console.Read()) != -1)
                    {
                        lyrics += (char)input;
                    }

                    using FileStream fileStream = fileInfo.OpenWrite();
                    using Stream zip = generator.GenerateLyrics(lyrics);
                    zip.Seek(0, SeekOrigin.Begin);
                    zip.CopyTo(fileStream);
                }
                else
                {
                    Console.WriteLine("No path specified");
                }

            }, pathOpt, new LyricGeneratorArgsBinder(
                spaceOption: spaceOpt,
                heightOption: heightOpt,
                widthOption: widthOpt,
                drawRedOption: dRedOpt,
                fadeRedOption: fRedOpt,
                drawGreenOption: dGreenOpt,
                fadeGreenOption: fGreenOpt,
                drawBlueOption: dBlueOpt,
                fadeBlueOption: fBlueOpt,
                drawAlphaOption: dAlphaOpt,
                fadeAlphaOption: fAlphaOpt));

            await rootCommand.InvokeAsync(args);
        }
    }
}