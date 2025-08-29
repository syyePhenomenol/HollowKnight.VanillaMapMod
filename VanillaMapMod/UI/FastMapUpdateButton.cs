using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class FastMapUpdateButton : MainButton
{
    public override void Update()
    {
        base.Update();
        if (ModeManager.CurrentMode() is FullMapMode)
        {
            Button.Visibility = MagicUI.Core.Visibility.Collapsed;
        }
        else
        {
            Button.Visibility = MagicUI.Core.Visibility.Visible;
        }
    }

    protected override void OnClick()
    {
        VanillaMapMod.GS.ToggleFastMapUpdate();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"{"Fast Map\nUpdate".L()}: ";

        return VanillaMapMod.GS.FastMapUpdate
            ? new(text + "On".L(), Colors.GetColor(ColorSetting.UI_On))
            : new(text + "Off".L(), Colors.GetColor(ColorSetting.UI_Neutral));
    }
}
