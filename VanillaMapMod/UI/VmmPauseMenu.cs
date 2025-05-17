using System.Collections.Generic;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

public class VmmPauseMenu : PauseMenuLayout
{
    public VmmPauseMenu()
        : base(nameof(VanillaMapMod), nameof(VmmPauseMenu)) { }

    protected override bool ActiveCondition()
    {
        return MapChangerMod.IsEnabled() && ModeManager.CurrentMode().Mod is nameof(VanillaMapMod);
    }

    protected override PauseMenuTitle GetTitle()
    {
        return new PauseMenuTitle();
    }

    protected override IEnumerable<MainButton> GetMainButtons()
    {
        return
        [
            new ModEnabledButton(),
            new ModPinsButton(),
            new VanillaPinsButton(),
            new PoolsGridControlButton(),
            new ModeButton(),
            new PinShapeButton(),
            new PinSizeButton(),
            new UIScaleButton(),
        ];
    }

    protected override IEnumerable<ExtraButtonGrid> GetExtraButtonGrids()
    {
        return [new PoolsGrid()];
    }
}
