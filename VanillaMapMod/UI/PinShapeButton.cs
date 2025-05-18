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
            $"{"Pin Shape".L()}:\n" + VanillaMapMod.GS.PinShape.ToString().ToWhitespaced().L(),
            Colors.GetColor(ColorSetting.UI_Neutral)
        );
    }
}
