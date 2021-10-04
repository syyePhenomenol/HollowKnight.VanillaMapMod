namespace MapMod.Map
{
    public static class QuickMap
    {
        public static void Hook()
        {
			On.GameMap.QuickMapAncientBasin += GameMap_QuickMapAncientBasin;
			On.GameMap.QuickMapCity += GameMap_QuickMapCity;
			On.GameMap.QuickMapCliffs += GameMap_QuickMapCliffs;
			On.GameMap.QuickMapCrossroads += GameMap_QuickMapCrossroads;
			On.GameMap.QuickMapCrystalPeak += GameMap_QuickMapCrystalPeak;
			On.GameMap.QuickMapDeepnest += GameMap_QuickMapDeepnest;
			On.GameMap.QuickMapDirtmouth += GameMap_QuickMapDirtmouth;
			On.GameMap.QuickMapFogCanyon += GameMap_QuickMapFogCanyon;
			On.GameMap.QuickMapFungalWastes += GameMap_QuickMapFungalWastes;
			On.GameMap.QuickMapGreenpath += GameMap_QuickMapGreenpath;
			On.GameMap.QuickMapKingdomsEdge += GameMap_QuickMapKingdomsEdge;
			On.GameMap.QuickMapQueensGardens += GameMap_QuickMapQueensGardens;
			On.GameMap.QuickMapRestingGrounds += GameMap_QuickMapRestingGrounds;
			On.GameMap.QuickMapWaterways += GameMap_QuickMapWaterways;

			//On.GameManager.SetGameMap += GameManager_SetGameMap;
		}

		private static void GameMap_QuickMapAncientBasin(On.GameMap.orig_QuickMapAncientBasin orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
            {
				WorldMap.CustomPins.UpdatePins("Ancient_Basin");
			}
		}

		private static void GameMap_QuickMapCity(On.GameMap.orig_QuickMapCity orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("City_of_Tears");
			}
		}

		private static void GameMap_QuickMapCliffs(On.GameMap.orig_QuickMapCliffs orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Howling_Cliffs");
			}
		}

		private static void GameMap_QuickMapCrossroads(On.GameMap.orig_QuickMapCrossroads orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Forgotten_Crossroads");
			}
		}

		private static void GameMap_QuickMapCrystalPeak(On.GameMap.orig_QuickMapCrystalPeak orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Crystal_Peak");
			}
		}

		private static void GameMap_QuickMapDeepnest(On.GameMap.orig_QuickMapDeepnest orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Deepnest");
			}
		}

		private static void GameMap_QuickMapDirtmouth(On.GameMap.orig_QuickMapDirtmouth orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Dirtmouth");
			}
		}

		private static void GameMap_QuickMapFogCanyon(On.GameMap.orig_QuickMapFogCanyon orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Fog_Canyon");
			}
		}

		private static void GameMap_QuickMapFungalWastes(On.GameMap.orig_QuickMapFungalWastes orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Fungal_Wastes");
			}
		}

		private static void GameMap_QuickMapGreenpath(On.GameMap.orig_QuickMapGreenpath orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Greenpath");
			}
		}

		private static void GameMap_QuickMapKingdomsEdge(On.GameMap.orig_QuickMapKingdomsEdge orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Kingdoms_Edge");
			}
		}

		private static void GameMap_QuickMapQueensGardens(On.GameMap.orig_QuickMapQueensGardens orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Queens_Gardens");
			}
		}

		private static void GameMap_QuickMapRestingGrounds(On.GameMap.orig_QuickMapRestingGrounds orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Resting_Grounds");
			}
		}

		private static void GameMap_QuickMapWaterways(On.GameMap.orig_QuickMapWaterways orig, GameMap self)
		{
			orig(self);

			if (WorldMap.goCustomPins != null)
			{
				WorldMap.CustomPins.UpdatePins("Royal_Waterways");
			}
		}

		//private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject goGameMap)
		//{
		//	orig(self, goGameMap);

		//	GameObject goQuickMap = GameObject.Find("Quick Map");

		//	PlayMakerFSM fsmQuickMap = goQuickMap.LocateMyFSM("Quick Map");

  //          if (fsmQuickMap.FsmStates.Any(state => state.Name == "WHITE_PALACE"))
  //          {
		//		MapMod.Instance.Log("AdditionalMaps WHITE_PALACE map detected");

		//		FsmUtil.AddAction(fsmQuickMap, "WHITE_PALACE", new QuickMapWhitePalace());
		//	} else
  //          {
		//		MapMod.Instance.Log("AdditionalMaps WHITE_PALACE map NOT detected");
		//	}
  //      }

		//private class QuickMapWhitePalace : FsmStateAction
		//{
		//	public override void OnEnter()
		//	{
		//		WorldMap.PG.Show();

		//		if (WorldMap.goPG != null)
		//		{
		//			Log("Update Pins White Palace");
		//			WorldMap.PG.UpdatePins("White_Palace");
		//		}

		//		Finish();
		//	}
		//}
	}
}
