using ReactiveUI;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace LyricGeneratorApp.ViewModels
{
    public class LyricFormViewModel : ViewModelBase
    {
        private int _bottomSpace;
        private int _drawRed;
        private int _drawGreen;
        private int _drawBlue;
        private int _drawAlpha;
        private int _fadeRed;
        private int _fadeGreen;
        private int _fadeBlue;
        private int _fadeAlpha;
        private int _height;
        private string _lyrics;
        private int _width;

        [Range(0, int.MaxValue)]
        public int BottomSpace
        {
            get => _bottomSpace;
            set => this.RaiseAndSetIfChanged(ref _bottomSpace, value);
        }

        [Range(0, 255)]
        public int DrawRed
        {
            get => _drawRed;
            set => this.RaiseAndSetIfChanged(ref _drawRed, value);
        }

        [Range(0, 255)]
        public int DrawGreen
        {
            get => _drawGreen;
            set => this.RaiseAndSetIfChanged(ref _drawGreen, value);
        }

        [Range(0, 255)]
        public int DrawBlue
        {
            get => _drawBlue;
            set => this.RaiseAndSetIfChanged(ref _drawBlue, value);
        }

        [Range(0, 255)]
        public int DrawAlpha
        {
            get => _drawAlpha;
            set => this.RaiseAndSetIfChanged(ref _drawAlpha, value);
        }

        [Range(0, 255)]
        public int FadeRed
        {
            get => _fadeRed;
            set => this.RaiseAndSetIfChanged(ref _fadeRed, value);
        }

        [Range(0, 255)]
        public int FadeGreen
        {
            get => _fadeGreen;
            set => this.RaiseAndSetIfChanged(ref _fadeGreen, value);
        }

        [Range(0, 255)]
        public int FadeBlue
        {
            get => _fadeBlue;
            set => this.RaiseAndSetIfChanged(ref _fadeBlue, value);
        }

        [Range(0, 255)]
        public int FadeAlpha
        {
            get => _fadeAlpha;
            set => this.RaiseAndSetIfChanged(ref _fadeAlpha, value);
        }

        [Range(0, int.MaxValue)]
        public int Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        [Required]
        public string Lyrics
        {
            get => _lyrics;
            set => this.RaiseAndSetIfChanged(ref _lyrics, value);
        }

        [Range(0, int.MaxValue)]
        public int Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        public ReactiveCommand<Unit, Unit> Reset { get; }

        private void ResetForm()
        {
            // TODO: Load from preferences.
            BottomSpace = LyricGeneratorLib.LyricGenerator.DEFAULT_BOTTOM_SPACE;
            DrawRed = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            DrawGreen = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            DrawBlue = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            DrawAlpha = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            FadeRed = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            FadeGreen = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            FadeBlue = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            FadeAlpha = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_FADE;
            Height = LyricGeneratorLib.LyricGenerator.DEFAULT_HEIGHT;
            Width = LyricGeneratorLib.LyricGenerator.DEFAULT_WIDTH;
            Lyrics = string.Empty;
        }

        public LyricFormViewModel()
        {
            Reset = ReactiveCommand.Create(ResetForm);
            _bottomSpace = LyricGeneratorLib.LyricGenerator.DEFAULT_BOTTOM_SPACE;
            _drawRed = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _drawGreen = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _drawBlue = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _drawAlpha = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _fadeRed = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _fadeGreen = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _fadeBlue = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_WHITE;
            _fadeAlpha = LyricGeneratorLib.LyricGenerator.DEFAULT_COLOR_FADE;
            _height = LyricGeneratorLib.LyricGenerator.DEFAULT_HEIGHT;
            _width = LyricGeneratorLib.LyricGenerator.DEFAULT_WIDTH;
            _lyrics = string.Empty;
        }
    }
}
