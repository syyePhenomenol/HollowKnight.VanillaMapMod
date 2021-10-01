using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Logger = Modding.Logger;

namespace MapMod.MapData
{
    public static class Data
    {
        private static Dictionary<string, PinDef> _pins;

        public static PinDef GetPinDef(string name)
        {
            if (_pins.TryGetValue(name, out PinDef def))
            {
                return def;
            }

            Logger.LogWarn($"Unable to find ItemDef for {name}.");
            return null;
        }

        public static PinDef[] GetPinArray()
        {
            return _pins.Values.ToArray();
        }

        public static bool IsPin(string item)
        {
            return _pins.ContainsKey(item);
        }

        public static void Load()
        {
            _pins = JsonUtil.Deserialize<Dictionary<string, PinDef>>("MapMod.Resources.pins.json");

            return;
        }

    }
}
