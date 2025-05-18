using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

public class ModeButton : MainButton
{
    protected override void OnClick()
    {
        ModeManager.ToggleMode();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"{"Mode".L()}\n";
        return ModeManager.CurrentMode() switch
        {
            NormalMode normalMode => new(text + normalMode.ModeName.L(), Colors.GetColor(ColorSetting.UI_Neutral)),
            FullMapMode fullMapMode => new(text + fullMapMode.ModeName.L(), Colors.GetColor(ColorSetting.UI_On)),
            _ => default,
        };
    }
}
