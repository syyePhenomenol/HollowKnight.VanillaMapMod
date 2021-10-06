using System.Collections.Generic;
using System.Linq;
using Modding;
using MapMod.Data;
using MapMod.Settings;

namespace MapMod.Shop
{
    public static class ShopHooks
    {
        public static void Hook()
        {
            ModHooks.LanguageGetHook += GetLanguageString;
            ModHooks.SetPlayerBoolHook += BoolSetOverride;
        }

        private static string GetLanguageString(string key, string sheetTitle, string orig)
        {
            if (DataLoader.IsCustomLanguage(sheetTitle, key))
            {
                return DataLoader.GetCustomLanguage(sheetTitle, key).Replace("\\n", "\n");
            }

            return orig;
        }

        private static bool BoolSetOverride(string boolName, bool orig)
        {
            SettingsUtil.SetMapModSetting(boolName, orig);

            return orig;
        }
    }
}