using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod;

internal class VanillaPinsButton : MainButton
{
    public VanillaPinsButton()
        : base("Vanilla Toggle", nameof(VanillaMapMod), 0, 2) { }

    protected override void OnClick()
    {
        VanillaMapMod.LS.ToggleVanillaPins();
    }

    public override void Update()
    {
        base.Update();

        var text = $"Vanilla Pins:\n";

        if (VanillaMapMod.LS.VanillaPinsOn)
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_On);
            text += "On";
        }
        else
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_Neutral);
            text += "Off";
        }

        Button.Content = text;
    }
}
