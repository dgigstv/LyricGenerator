using System.CommandLine;
using System.CommandLine.Binding;

namespace LyricGeneratorCLI
{
    internal class LyricGeneratorArgsBinder : BinderBase<LyricGeneratorArgs>
    {
        private readonly Option<int> _spaceOption;
        private readonly Option<int> _heighOption;
        private readonly Option<int> _widthOption;
        private readonly Option<int> _fontSizeOption;
        private readonly Option<byte> _drawRedOption;
        private readonly Option<byte> _fadeRedOption;
        private readonly Option<byte> _drawGreenOption;
        private readonly Option<byte> _fadeGreenOption;
        private readonly Option<byte> _drawBlueOption;
        private readonly Option<byte> _fadeBlueOption;
        private readonly Option<byte> _drawAlphaOption;
        private readonly Option<byte> _fadeAlphaOption;

        public LyricGeneratorArgsBinder(Option<int> spaceOption, Option<int> heightOption, Option<int> widthOption, Option<int> fontSizeOption, Option<byte> drawRedOption, Option<byte> fadeRedOption, Option<byte> drawGreenOption, Option<byte> fadeGreenOption, Option<byte> drawBlueOption, Option<byte> fadeBlueOption, Option<byte> drawAlphaOption, Option<byte> fadeAlphaOption)
        {
            _spaceOption = spaceOption;
            _heighOption = heightOption;
            _widthOption = widthOption;
            _fontSizeOption = fontSizeOption;
            _drawRedOption = drawRedOption;
            _fadeRedOption = fadeRedOption;
            _drawGreenOption = drawGreenOption;
            _fadeGreenOption = fadeGreenOption;
            _drawBlueOption = drawBlueOption;
            _fadeBlueOption = fadeBlueOption;
            _drawAlphaOption = drawAlphaOption;
            _fadeAlphaOption = fadeAlphaOption;
        }

        protected override LyricGeneratorArgs GetBoundValue(BindingContext bindingContext) => new LyricGeneratorArgs
        {
            Space = bindingContext.ParseResult.GetValueForOption(_spaceOption),
            Height = bindingContext.ParseResult.GetValueForOption(_heighOption),
            Width = bindingContext.ParseResult.GetValueForOption(_widthOption),
            FontSize = bindingContext.ParseResult.GetValueForOption(_fontSizeOption),
            DrawRed = bindingContext.ParseResult.GetValueForOption(_drawRedOption),
            FadeRed = bindingContext.ParseResult.GetValueForOption(_fadeRedOption),
            DrawGreen = bindingContext.ParseResult.GetValueForOption(_drawGreenOption),
            FadeGreen = bindingContext.ParseResult.GetValueForOption(_fadeGreenOption),
            DrawBlue = bindingContext.ParseResult.GetValueForOption(_drawBlueOption),
            FadeBlue = bindingContext.ParseResult.GetValueForOption(_fadeBlueOption),
            DrawAlpha = bindingContext.ParseResult.GetValueForOption(_drawAlphaOption),
            FadeAlpha = bindingContext.ParseResult.GetValueForOption(_fadeAlphaOption),
        };
    }
}
