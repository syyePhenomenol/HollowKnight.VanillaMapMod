using System.Collections;
using Modding;
using UnityEngine.SceneManagement;

namespace VanillaMapMod.UI
{
    public static class GUI
    {
        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
        }

        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            GUIController.Setup();
            GUIController.Instance.BuildMenus();
        }
    }
}