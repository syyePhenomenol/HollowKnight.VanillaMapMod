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
            On.QuitToMenu.Start += OnQuitToMenu;
            //UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
        }

        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            GUIController.Setup();
            GUIController.Instance.BuildMenus();
        }

        private static IEnumerator OnQuitToMenu(On.QuitToMenu.orig_Start orig, QuitToMenu self)
        {
            GUIController.Unload();

            return orig(self);
        }

        //private static void HandleSceneChanges(Scene from, Scene to)
        //{
        //    if (to.name == "Quit_to_Menu")
        //    {
        //        GUIController.Unload();
        //    }
        //}
    }
}