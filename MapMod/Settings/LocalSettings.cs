using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modding;
using SFCore;

namespace MapMod.Settings
{
    [Serializable]
    public class LocalSettings
    {
        public Dictionary<string, bool> ObtainedItems = new();
    }
}
