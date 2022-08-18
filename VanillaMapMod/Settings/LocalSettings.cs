using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;

namespace VanillaMapMod.Settings
{
    public class LocalSettings
    {
        public bool InitializedPreviously = false;

        public Dictionary<PoolGroup, bool> PoolSettings = Enum.GetValues(typeof(PoolGroup))
               .Cast<PoolGroup>()
               .Where(poolGroup => poolGroup is not PoolGroup.Other)
               .ToDictionary(t => t, t => true);

        public bool VanillaPinsOn = false;

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
            if (!PoolSettings.ContainsKey(poolGroup)) return;

            PoolSettings[poolGroup] = !PoolSettings[poolGroup];
        }

        internal void ToggleAllPools()
        {
            bool value = false;

            if (PoolSettings.Values.Any(value => !value))
            {
                value = true;
            }

            foreach (PoolGroup poolGroup in Enum.GetValues(typeof(PoolGroup)).Cast<PoolGroup>().Where(poolGroup => poolGroup is not PoolGroup.Other))
            {
                PoolSettings[poolGroup] = value;
            }
        }

        internal void ToggleVanillaPins()
        {
            VanillaPinsOn = !VanillaPinsOn;
        }
    }
}