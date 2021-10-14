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
            if (VanillaMapMod.LS.IsGroup(boolName))
            {
                return VanillaMapMod.LS.GetHasFromGroup(boolName);
            }

            //if (boolName == "hasQuill" && orig == true)
            //{
                //foreach (string scene in PlayerData.instance.scenesVisited)
                //{
                //    if (!PlayerData.instance.scenesMapped.Contains(scene))
                //    {
                //        PlayerData.instance.scenesMapped.Add(scene);
                //    }
                //}
            //}

            return orig;
        }

        private static bool BoolSetOverride(string boolName, bool orig)
        {
            VanillaMapMod.LS.SetHasFromGroup(boolName, orig);

            return orig;
        }
    }
}