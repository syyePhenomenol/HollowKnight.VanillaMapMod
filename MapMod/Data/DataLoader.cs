using System.Collections.Generic;
using System.Linq;
using Modding;

namespace MapMod.Data
{
    public static class DataLoader
    {
        private static Dictionary<string, PinDef> _pins;
        private static Dictionary<string, ShopDef> _shop;
        private static Dictionary<string, Dictionary<string, string>> _languageStrings;

        

        public static PinDef GetPinDef(string name)
        {
            if (_pins.TryGetValue(name, out PinDef def))
            {
                return def;
            }

            MapMod.Instance.LogWarn($"Unable to find ItemDef for {name}.");
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
            if (_languageStrings.ContainsKey(sheet))
            {
                if (_languageStrings[sheet].ContainsKey(key))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetCustomLanguage(string sheet, string key)
        {
            return _languageStrings[sheet][key];
        }

        public static void Load()
        {
            _pins = JsonUtil.Deserialize<Dictionary<string, PinDef>>("MapMod.Resources.pins.json");
            _shop = JsonUtil.Deserialize<Dictionary<string, ShopDef>>("MapMod.Resources.shop.json");
            _languageStrings = JsonUtil.Deserialize<Dictionary<string, Dictionary<string, string>>>("MapMod.Resources.language.json");

            foreach (KeyValuePair<string, Dictionary<string, string>> entry in _languageStrings)
            {
                MapMod.Instance.Log(entry.Key);
                foreach (KeyValuePair<string, string> entry2 in entry.Value)
                {
                    MapMod.Instance.Log("- " + entry2.Key);
                    MapMod.Instance.Log("- " + entry2.Value);
                }
            }

            return;
        }

    }
}
