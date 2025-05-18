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
        var text = $"{"Vanilla Pins".L()}:\n";

        return VanillaMapMod.LS.VanillaPinsOn
            ? new(text + "On".L(), Colors.GetColor(ColorSetting.UI_On))
            : new(text + "Off".L(), Colors.GetColor(ColorSetting.UI_Neutral));
    }
}
