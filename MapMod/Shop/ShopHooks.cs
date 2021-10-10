using VanillaMapMod.Data;
using VanillaMapMod.Settings;
using Modding;

namespace VanillaMapMod.Shop
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
                //return SettingsUtil.GetMapModSettingFromBoolName(boolName);
                return VanillaMapMod.LS.GetHasFromGroup(boolName);
            }

            return orig;
        }

        private static bool BoolSetOverride(string boolName, bool orig)
        {
            //SettingsUtil.SetMapModSetting(boolName, orig);
            VanillaMapMod.LS.SetHasFromGroup(boolName, orig);

            return orig;
        }
    }
}