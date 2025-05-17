using System.Linq;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class ModPinsButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.LS.ToggleAllPools();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"Mod Pins:\n";

        if (VanillaMapMod.LS.PoolSettings.Values.All(value => value))
        {
            return new(text + "On", Colors.GetColor(ColorSetting.UI_On));
        }
        else if (VanillaMapMod.LS.PoolSettings.Values.All(value => !value))
        {
            return new(text + "Off", Colors.GetColor(ColorSetting.UI_Neutral));
        }

        return new(text + "Custom", Colors.GetColor(ColorSetting.UI_Custom));
    }
}
