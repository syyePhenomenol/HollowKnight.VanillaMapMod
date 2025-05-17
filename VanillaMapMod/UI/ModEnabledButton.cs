using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

public class ModEnabledButton : MainButton
{
    protected override void OnClick()
    {
        ModeManager.ToggleModEnabled();
    }

    protected override TextFormat GetTextFormat()
    {
        return new("Mod\nEnabled", Colors.GetColor(ColorSetting.UI_On));
    }
}
