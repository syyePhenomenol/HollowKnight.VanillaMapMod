namespace MapMod.Trackers
{
	// Items are tracked even if the mod is installed after a game is started
    public static class OnGameLoad
	{
		public static void Hook()
		{
            Modding.ModHooks.AfterSavegameLoadHook += ModHooks_AfterSavegameLoadHook;
		}

        private static void ModHooks_AfterSavegameLoadHook(SaveGameData obj)
        {
			if (obj.playerData.hasDreamNail)
            {
				MapMod.LS.ObtainedItems["Moth NPC" + "Dream_Nailcollection"] = true;
			}

			if (obj.playerData.hasSuperDash)
            {
				MapMod.LS.ObtainedItems["Super Dash Get" + "Mines_31"] = true;
			}

			if (obj.playerData.hasDoubleJump)
			{
				MapMod.LS.ObtainedItems["Shiny Item DJ" + "Abyss_21"] = true;
			}

			if (obj.playerData.hasShadowDash)
			{
				MapMod.LS.ObtainedItems["Dish Plat" + "Abyss_10"] = true;
			}

			if (obj.playerData.hasCyclone)
			{
				MapMod.LS.ObtainedItems["NM Mato NPC" + "Room_nailmaster"] = true;
			}

			if (obj.playerData.hasDashSlash)
			{
				MapMod.LS.ObtainedItems["NM Sheo NPC" + "Room_nailmaster_02"] = true;
			}

			if (obj.playerData.hasUpwardSlash)
			{
				MapMod.LS.ObtainedItems["NM Oro NPC" + "Room_nailmaster_03"] = true;
			}

			// Doesn't seem to be a way to tell apart the duplicate cases
			foreach (GeoRockData grd in obj.sceneData.geoRocks)
			{
				if (grd.hitsLeft == 0)
				{
					MapMod.LS.ObtainedItems[grd.id + grd.sceneName] = true;
				}
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
	}
}
