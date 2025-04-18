﻿using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod;

public class ModeButton : MainButton
{
    public ModeButton()
        : base("Mode", nameof(VanillaMapMod), 1, 0) { }

    protected override void OnClick()
    {
        MapChanger.Settings.ToggleMode();
    }

    public override void Update()
    {
        base.Update();

        var text = "Mode\n";

        if (MapChanger.Settings.CurrentMode() is NormalMode)
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_Neutral);
            text += "Normal";
        }

        if (MapChanger.Settings.CurrentMode() is FullMapMode)
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_On);
            text += "Full Map";
        }

        Button.Content = text;
    }
}
