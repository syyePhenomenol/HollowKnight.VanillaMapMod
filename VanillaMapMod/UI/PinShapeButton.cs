using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;
using VanillaMapMod.Settings;

namespace VanillaMapMod;

internal class PinShapeButton : MainButton
{
    public PinShapeButton()
        : base("Pin Shape", nameof(VanillaMapMod), 1, 2) { }

    protected override void OnClick()
    {
        VanillaMapMod.GS.TogglePinShape();
    }

    public override void Update()
    {
        base.Update();

        var text = $"Pin Shape\n";

        switch (VanillaMapMod.GS.PinShape)
        {
            case PinShape.Circle:
                text += "circle";
                break;

            case PinShape.Diamond:
                text += "diamond";
                break;

            case PinShape.Square:
                text += "square";
                break;

            case PinShape.Pentagon:
                text += "pentagon";
                break;

            case PinShape.Hexagon:
                text += "hexagon";
                break;

            case PinShape.NoBorder:
                text += "no borders";
                break;
        }

        Button.ContentColor = Colors.GetColor(ColorSetting.UI_Neutral);
        Button.Content = text;
    }
}
