using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class VanillaPinsButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.LS.ToggleVanillaPins();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"Vanilla Pins:\n";

        return VanillaMapMod.LS.VanillaPinsOn
            ? new(text + "On", Colors.GetColor(ColorSetting.UI_On))
            : new(text + "Off", Colors.GetColor(ColorSetting.UI_Neutral));
    }
}
