using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MapChanger.UI;

namespace VanillaMapMod;

internal class PoolsPanel : ExtraButtonPanel
{
    internal static PoolsPanel Instance { get; private set; }

    public PoolsPanel()
        : base("Pools Panel", nameof(VanillaMapMod), GetPoolButtons(), 395f, 10)
    {
        Instance = this;
    }

    private static IEnumerable<ExtraButton> GetPoolButtons()
    {
        return Enum.GetValues(typeof(PoolGroup))
            .Cast<PoolGroup>()
            .Except([PoolGroup.Other])
            .Select(p => new PoolButton(p));
    }
}
