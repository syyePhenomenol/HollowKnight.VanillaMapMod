using System;
using Modding;
using UnityEngine.SceneManagement;

namespace VanillaMapMod.UI
{
    public static class GUI
    {
        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
        }

        public static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            GUIController.Setup();
            GUIController.Instance.BuildMenus();
        }

        private static void HandleSceneChanges(Scene from, Scene to)
        {
            if (to.name == "Quit_to_Menu")
            {
                GUIController.Unload();
            }
        }
    }
}