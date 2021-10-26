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

            // Immediately update map with visited areas when quill is picked up, to avoid wasting time at bench
            if (boolName == "hasQuill" && orig)
            {
                foreach (string scene in PlayerData.instance.scenesVisited)
                {
                    if (!PlayerData.instance.scenesMapped.Contains(scene))
                    {
                        PlayerData.instance.scenesMapped.Add(scene);
                    }
                }
            }

            return orig;
        }
    }
}