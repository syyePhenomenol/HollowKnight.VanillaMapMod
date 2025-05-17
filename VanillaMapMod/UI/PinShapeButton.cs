using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class PinShapeButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.GS.TogglePinShape();
    }

    protected override TextFormat GetTextFormat()
    {
        return new(
            $"Pin Shape:\n" + VanillaMapMod.GS.PinShape.ToString().ToWhitespaced() + "s",
            Colors.GetColor(ColorSetting.UI_Neutral)
        );
    }
}
