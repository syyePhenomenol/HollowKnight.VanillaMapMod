using MapChanger;
using MapChanger.UI;
using UnityEngine;

namespace VanillaMapMod.UI;

internal class PoolsGridControlButton : GridControlButton<PoolsGrid>
{
    protected override TextFormat GetTextFormat()
    {
        return new(
            "Customize\nPins".L(),
            (PoolsGrid.Instance?.Grid?.IsEffectivelyVisible ?? false)
                ? Colors.GetColor(ColorSetting.UI_Custom)
                : Colors.GetColor(ColorSetting.UI_Neutral)
        );
    }

    protected override Color GetBorderColor()
    {
        return Colors.GetColor(ColorSetting.UI_Neutral);
    }
}
