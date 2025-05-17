using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class UIScaleButton : MainButton
{
    protected override void OnClick()
    {
        MapChangerMod.GS.ToggleUIScale();
    }

    protected override TextFormat GetTextFormat()
    {
        return new($"{"UI Scale"}\n{MapChangerMod.GS.UIScale:n1} ×", Colors.GetColor(ColorSetting.UI_Neutral));
    }
}
