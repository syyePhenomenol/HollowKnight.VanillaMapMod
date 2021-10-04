using System;
using System.Collections.Generic;

namespace MapMod.Settings
{
    [Serializable]
    public class LocalSettings
    {
        public Dictionary<string, bool> ObtainedItems = new();
    }
}
