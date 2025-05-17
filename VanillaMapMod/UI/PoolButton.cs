using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class PoolButton(PoolGroup poolGroup) : ExtraButton
{
    public override string Name => $"{Layout.Mod} : {PoolGroup}";
    internal PoolGroup PoolGroup { get; } = poolGroup;

    protected override void OnClick()
    {
        VanillaMapMod.LS.TogglePoolGroupSetting(PoolGroup);
    }

    protected override TextFormat GetTextFormat()
    {
        return new(
            PoolGroup.FriendlyName().Replace(" ", "\n") + "\n" + VmmPinManager.GetPoolGroupCounter(PoolGroup),
            Colors.GetColor(
                VanillaMapMod.LS.GetPoolGroupSetting(PoolGroup) ? ColorSetting.UI_On : ColorSetting.UI_Neutral
            )
        );
    }
}
