using System.Collections.Generic;
using System.Linq;

namespace MapModS.Data
{
    public static class DataLoader
    {
        private static Dictionary<string, PinDef> _pins;
        private static Dictionary<string, ShopDef> _shop;
        private static Dictionary<string, Dictionary<string, string>> _languageStrings;

        public static PinDef GetPinDef(string name)
        {
            if (_pins.TryGetValue(name, out PinDef def)) return def;

            MapModS.Instance.LogWarn($"Unable to find ItemDef for {name}.");

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

        public static ShopDef[] GetShopArray()
        {
            return _shop.Values.ToArray();
        }

        public static bool IsCustomLanguage(string sheet, string key)
        {
            if (!_languageStrings.ContainsKey(sheet)) return false;

            if (!_languageStrings[sheet].ContainsKey(key)) return false;

            return true;
        }

        public static string GetCustomLanguage(string sheet, string key)
        {
            return _languageStrings[sheet][key];
        }

        public static void Load()
        {
            _pins = JsonUtil.Deserialize<Dictionary<string, PinDef>>("MapModS.Resources.pins.json");
            _shop = JsonUtil.Deserialize<Dictionary<string, ShopDef>>("MapModS.Resources.shop.json");
            _languageStrings = JsonUtil.Deserialize<Dictionary<string, Dictionary<string, string>>>("MapModS.Resources.language.json");

            foreach (KeyValuePair<string, PinDef> entry in _pins)
            {
                if (entry.Value.objectName == null
                    && entry.Value.pool != Pool.Cocoon
                    && entry.Value.pool != Pool.Totem
                    && entry.Value.pool != Pool.Lore)
                {
                    MapModS.Instance.LogWarn($"There is a pin with no objectName that should have one: {entry.Key}");
                }
            }

            return;
        }
    }
}