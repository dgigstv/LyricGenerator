using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.IO.Compression;
using System.Text;

namespace LyricGeneratorLib
{
    public class LyricGenerator
    {
        private object _zipLock;

        public const int DEFAULT_BOTTOM_SPACE = 0;
        public const byte DEFAULT_COLOR_FADE = 175;
        public const byte DEFAULT_COLOR_WHITE = 255;
        public const byte DEFAULT_DRAW_R = 255;
        public const byte DEFAULT_DRAW_G = 255;
        public const byte DEFAULT_DRAW_B = 255;
        public const byte DEFAULT_DRAW_A = 255;
        public const byte DEFAULT_FADE_R = 255;
        public const byte DEFAULT_FADE_G = 255;
        public const byte DEFAULT_FADE_B = 255;
        public const byte DEFAULT_FADE_A = 175;
        public const string DEFAULT_FONT = "Arial Black";
        public const int DEFAULT_FONT_SIZE = 56;
        public const int DEFAULT_HEIGHT = 1080;
        public const int DEFAULT_WIDTH = 1920;

        public int BottomSpace { get; set; } = DEFAULT_BOTTOM_SPACE;
        public byte DrawColorA { get; set; } = DEFAULT_COLOR_WHITE;
        public byte DrawColorR { get; set; } = DEFAULT_COLOR_WHITE;
        public byte DrawColorG { get; set; } = DEFAULT_COLOR_WHITE;
        public byte DrawColorB { get; set; } = DEFAULT_COLOR_WHITE;
        public byte FadeColorR { get; set; } = DEFAULT_COLOR_WHITE;
        public byte FadeColorG { get; set; } = DEFAULT_COLOR_WHITE;
        public byte FadeColorB { get; set; } = DEFAULT_COLOR_WHITE;
        public byte FadeColorA { get; set; } = DEFAULT_COLOR_FADE;
        public string Font { get; set; } = DEFAULT_FONT;
        public int FontSize { get; set; } = DEFAULT_FONT_SIZE;
        public int Height { get; set; } = DEFAULT_HEIGHT;
        public int Width { get; set; } = DEFAULT_WIDTH;

        public LyricGenerator()
        {
            _zipLock = new object();
        }

        public Stream GenerateLyrics(string lyrics)
        {
            MemoryStream output = new MemoryStream();
            using ZipArchive archive = new ZipArchive(output, ZipArchiveMode.Create, true);
            string[] lyricLines = lyrics.Split(Environment.NewLine);

            Parallel.ForEach(lyricLines, (lyricLine, state, index) =>
            {
                var imageFiles = ProcessLine(lyricLine);
                string dirName = SanitizeFileName(lyricLine);

                foreach (var imageFile in imageFiles)
                {
                    string imageName = imageFile.Item1;
                    using MemoryStream imageStream = imageFile.Item2;

                    // Need mutex here since ZipArchive is not multithread friendly.
                    lock (_zipLock)
                    {
                        ZipArchiveEntry file = archive.CreateEntry(Path.Combine($"{index} {dirName}", imageName));

                        using Stream fileStream = file.Open();
                        imageStream.Seek(0, SeekOrigin.Begin);
                        imageStream.CopyTo(fileStream);
                    }
                }
            });

            return output;
        }

        private IEnumerable<(string, MemoryStream)> ProcessLine(string lyricLine)
        {
            List<(string, MemoryStream)> files = new();
            
            // First, add initial frame with no words highlighted.
            files.Add(($"frame_1.png", DrawImage(lyricLine, "")));

            // Now highlight the rest of the words.
            string brightLyric = "";
            string[] words = lyricLine.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                brightLyric += word + " ";

                files.Add(($"frame_{i + 2}.png", DrawImage(lyricLine, brightLyric.Trim())));
            }

            return files.ToArray();
        }

        private MemoryStream DrawImage(string fadeLyric, string brightLyric)
        {
            MemoryStream output = new();
            using Image image = new Image<Rgba32>(Width, Height);

            if (SystemFonts.TryFind(Font, out FontFamily? family))
            {
                Font font = family.CreateFont(FontSize, FontStyle.Bold);
                TextOptions options = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                Color fadeColor = new(new Rgba32(FadeColorR, FadeColorG, FadeColorB, FadeColorA));
                Color drawColor = new(new Rgba32(DrawColorR, DrawColorG, DrawColorB, DrawColorA));
                FontRectangle measurement = TextMeasurer.Measure(fadeLyric, new RendererOptions(font));
                PointF location = new((Width - measurement.Width) / 2, Height - measurement.Height - BottomSpace);

                image.Mutate(x => x.DrawText(fadeLyric, font, fadeColor, location));
                image.Mutate(x => x.DrawText(brightLyric, font, drawColor, location));
            }
            else
            {
                throw new LyricGeneratorException($"Could not find font ${Font}");
            }

            image.SaveAsPng(output);

            return output;
        }

        private static string SanitizeFileName(string fileName)
        {
            char[] invalid = Path.GetInvalidFileNameChars();
            StringBuilder newFileName = new StringBuilder(fileName);
            int idx;
            while ((idx = newFileName.ToString().IndexOfAny(invalid)) != -1)
            {
                newFileName[idx] = '_';
            }

            return newFileName.ToString();
        }
    }
}