using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MagicUI.Elements;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class PoolsGrid : ExtraButtonGrid
{
    internal static PoolsGrid Instance { get; private set; }
    public override int RowSize => 8;

    protected override DynamicUniformGrid Build(PauseMenuLayout layout)
    {
        Instance = this;
        return base.Build(layout);
    }

    protected override IEnumerable<ExtraButton> GetButtons()
    {
        return Enum.GetValues(typeof(PoolGroup))
            .Cast<PoolGroup>()
            .Except([PoolGroup.Other])
            .Select(p => new PoolButton(p));
    }
}
