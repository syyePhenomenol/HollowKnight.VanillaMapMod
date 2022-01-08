using Modding;
using System;
using VanillaMapMod.Data;

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
            if (Enum.TryParse(boolName, out Pool group))
            {
                return VanillaMapMod.LS.GetHasFromGroup(group);
            }

            return orig;
        }

        private static bool BoolSetOverride(string boolName, bool orig)
        {
            VanillaMapMod.LS.SetHasFromGroup(boolName, orig);

            return orig;
        }
    }
}