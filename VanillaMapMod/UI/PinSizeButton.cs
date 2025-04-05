using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;
using VanillaMapMod.Settings;

namespace VanillaMapMod;

internal class PinSizeButton : MainButton
{
    public PinSizeButton()
        : base("Pin Size", nameof(VanillaMapMod), 1, 1) { }

    protected override void OnClick()
    {
        VanillaMapMod.GS.TogglePinSize();
    }

    public override void Update()
    {
        base.Update();

        var text = $"Pin Size\n";

        switch (VanillaMapMod.GS.PinSize)
        {
            case PinSize.Tiny:
                text += "tiny";
                break;

            case PinSize.Small:
                text += "small";
                break;

            case PinSize.Medium:
                text += "medium";
                break;

            case PinSize.Large:
                text += "large";
                break;

            case PinSize.Huge:
                text += "huge";
                break;
        }

        Button.ContentColor = Colors.GetColor(ColorSetting.UI_Neutral);
        Button.Content = text;
    }
}
