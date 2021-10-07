using MapMod.Data;
using MapMod.Settings;
using Modding;

namespace MapMod.Shop
{
    public static class ShopHooks
    {
        public static void Hook()
        {
            ModHooks.LanguageGetHook += GetLanguageString;
            ModHooks.GetPlayerBoolHook += BoolGetOverride;
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

        public static bool BoolGetOverride(string boolName, bool orig)
        {
            if (SettingsUtil.IsMapModSetting(boolName))
            {
                return SettingsUtil.GetMapModSettingFromBoolName(boolName);
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