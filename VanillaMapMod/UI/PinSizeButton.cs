using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class PinSizeButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.GS.TogglePinSize();
    }

    protected override TextFormat GetTextFormat()
    {
        return new(
            $"Pin Size\n" + VanillaMapMod.GS.PinSize.ToString().ToWhitespaced(),
            Colors.GetColor(ColorSetting.UI_Neutral)
        );
    }
}
