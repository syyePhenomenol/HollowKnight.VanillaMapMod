using System.Collections;

namespace MapModS.UI
{
    public static class GUI
    {
        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
            On.QuitToMenu.Start += OnQuitToMenu;
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
    }
}