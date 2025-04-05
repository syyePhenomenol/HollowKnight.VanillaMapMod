using ConnectionMetadataInjector.Util;
using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod;

internal class PoolButton(PoolGroup poolGroup) : ExtraButton(poolGroup.FriendlyName(), nameof(VanillaMapMod))
{
    internal PoolGroup PoolGroup { get; } = poolGroup;

    protected override void OnClick()
    {
        VanillaMapMod.LS.TogglePoolGroupSetting(PoolGroup);
    }

    public override void Update()
    {
        Button.Content =
            PoolGroup.FriendlyName().Replace(" ", "\n") + "\n" + VmmPinManager.GetPoolGroupCounter(PoolGroup);

        if (VanillaMapMod.LS.GetPoolGroupSetting(PoolGroup))
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_On);
        }
        else
        {
            Button.ContentColor = Colors.GetColor(ColorSetting.UI_Neutral);
        }
    }
}
