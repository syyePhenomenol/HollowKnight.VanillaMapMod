using Modding;
using UnityEngine.SceneManagement;

namespace VanillaMapMod.UI
{
    public static class GUI
    {
        public static void Hook()
        {
            ModHooks.AfterSavegameLoadHook += AfterSavegameLoadHook;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
        }

        public static void AfterSavegameLoadHook(SaveGameData self)
        {
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
