using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using Newtonsoft.Json;

namespace VanillaMapMod.Settings;

public class LocalSettings
{
    [JsonProperty]
    public Dictionary<PoolGroup, bool> PoolSettings { get; private set; } =
        Enum.GetValues(typeof(PoolGroup))
            .Cast<PoolGroup>()
            .Where(poolGroup => poolGroup is not PoolGroup.Other)
            .ToDictionary(t => t, t => true);

    [JsonProperty]
    public bool VanillaPinsOn { get; private set; } = false;

    internal bool GetPoolGroupSetting(PoolGroup poolGroup)
    {
        if (PoolSettings.ContainsKey(poolGroup))
        {
            return PoolSettings[poolGroup];
        }

        return false;
    }

    internal void SetPoolGroupSetting(PoolGroup poolGroup, bool value)
    {
        if (PoolSettings.ContainsKey(poolGroup))
        {
            PoolSettings[poolGroup] = value;
        }
    }

    internal void TogglePoolGroupSetting(PoolGroup poolGroup)
    {
        if (!PoolSettings.ContainsKey(poolGroup))
        {
            return;
        }

        PoolSettings[poolGroup] = !PoolSettings[poolGroup];
    }

    internal void ToggleAllPools()
    {
        var value = false;

        if (PoolSettings.Values.Any(value => !value))
        {
            value = true;
        }

        foreach (
            var poolGroup in Enum.GetValues(typeof(PoolGroup))
                .Cast<PoolGroup>()
                .Where(poolGroup => poolGroup is not PoolGroup.Other)
        )
        {
            PoolSettings[poolGroup] = value;
        }
    }

    internal void ToggleVanillaPins()
    {
        VanillaPinsOn = !VanillaPinsOn;
    }
}
