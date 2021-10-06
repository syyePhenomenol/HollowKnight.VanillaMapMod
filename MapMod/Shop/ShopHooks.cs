using System.Collections.Generic;
using System.Linq;
using Modding;
using MapMod.Data;
using MapMod.Settings;
using UnityEngine;

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
            if (SettingsUtil.SetMapModSetting(boolName, orig))
            {
                foreach (GameObject shopObj in Object.FindObjectsOfType<GameObject>())
                {
                    if (shopObj.name != "Shop Menu") continue;

                    ShopMenuStock shop = shopObj.GetComponent<ShopMenuStock>();

                    shop.UpdateStock();
                }
            }

            return orig;
        }
    }
}