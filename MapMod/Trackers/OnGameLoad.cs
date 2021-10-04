namespace MapMod.Trackers
{
    public static class OnGameLoad
	{
		public static void Hook()
		{
			//UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
			//On.GameManager.LoadGame += GameManager_LoadGame;
            Modding.ModHooks.AfterSavegameLoadHook += ModHooks_AfterSavegameLoadHook;
		}

        private static void ModHooks_AfterSavegameLoadHook(SaveGameData obj)
        {
			if (obj.playerData.hasCyclone)
			{
				MapMod.LS.ObtainedItems["NM Mato NPC" + "Room_nailmaster"] = true;
			}

			if (obj.playerData.hasDashSlash)
            {
				MapMod.LS.ObtainedItems["NM Oro NPC" + "Room_nailmaster_03"] = true;
			}

			if (obj.playerData.hasDreamNail)
            {
				MapMod.LS.ObtainedItems["Moth NPC" + "Dream_Nailcollection"] = true;
			}

			if (obj.playerData.hasShadowDash)
            {
				MapMod.LS.ObtainedItems["Dish Plat" + "Abyss_10"] = true;
			}

			if (obj.playerData.hasSuperDash)
            {
				MapMod.LS.ObtainedItems["Super Dash Get" + "Mines_31"] = true;
			}

			foreach (PersistentBoolData pbd in obj.sceneData.persistentBoolItems)
			{
				if (pbd.id.Contains("Shiny Item") && pbd.activated)
				{
					MapMod.LS.ObtainedItems[pbd.id + pbd.sceneName] = true;
				}

				//MapMod.Instance.Log("- " + pbd.id);
				//MapMod.Instance.Log("- " + pbd.sceneName);
				//MapMod.Instance.Log("- - " + pbd.activated);
			}

			//foreach (PersistentIntData pid in obj.sceneData.persistentIntItems)
			//{
   //             MapMod.Instance.Log("- " + pid.id);
   //             MapMod.Instance.Log("- " + pid.sceneName);
   //             MapMod.Instance.Log("- - " + pid.value);
   //         }
		}

        //// This updates whether Shiny Items have been picked up, per scene change
        //private static void HandleSceneChanges(Scene from, Scene to)
        //{
        //	if (to.name != "Quit_To_Menu")
        //          {

        //	}
        //}

  //      private static void GameManager_LoadGame(On.GameManager.orig_LoadGame orig, GameManager self, int saveSlot, Action<bool> callback)
  //      {
		//	orig(self, saveSlot, callback);
		//}
	}
}
