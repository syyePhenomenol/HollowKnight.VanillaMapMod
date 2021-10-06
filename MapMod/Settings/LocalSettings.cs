using System;
using System.Collections.Generic;

namespace MapMod.Settings
{
    [Serializable]
    public class LocalSettings
    {
        public Dictionary<string, bool> ObtainedItems = new();

        public bool HasSkillPin = false;
        public bool HasCharmPin = false;
        public bool HasKeyPin = false;
        public bool HasMaskPin = false;
        public bool HasVesselPin = false;
        public bool HasNotchPin = false;
        public bool HasOrePin = false;
        public bool HasEggPin = false;
        public bool HasRelicPin = false;
        public bool HasEssenceBossPin = false;
        public bool HasCocoonPin = false;
        public bool HasRockPin = false;
        public bool HasTotemPin = false;
        public bool HasLorePin = false;

    }
}
