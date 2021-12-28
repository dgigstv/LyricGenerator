using LyricGeneratorLib;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace LyricGeneratorApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private LyricGenerator LyricGenerator;
        public LyricFormViewModel Lyrics { get; }
        public ReactiveCommand<Unit, Unit> Generate { get; }

        public MainWindowViewModel()
        {
            Generate = ReactiveCommand.CreateFromTask(GenerateAsync);
            LyricGenerator = new LyricGenerator();
            Lyrics = new LyricFormViewModel();
        }

        public Task GenerateAsync()
        {
            return Task.Run(() =>
            {
                using FileStream fileStream = new FileStream(@"C:\Users\User\Desktop\test.zip", FileMode.Create);
                using Stream zip = LyricGenerator.GenerateLyrics(Lyrics.Lyrics);
                zip.Seek(0, SeekOrigin.Begin);
                zip.CopyTo(fileStream);
            });
        }
    }
}
