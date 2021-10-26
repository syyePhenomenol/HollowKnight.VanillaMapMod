using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using VanillaMapMod.Settings;
using Vasi;

namespace VanillaMapMod.Map
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
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
        }

        // These are called every time we open the respective Quick Map
        private static void GameMap_QuickMapAncientBasin(On.GameMap.orig_QuickMapAncientBasin orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.ABYSS);
        }

        private static void GameMap_QuickMapCity(On.GameMap.orig_QuickMapCity orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.CITY);
        }

        private static void GameMap_QuickMapCliffs(On.GameMap.orig_QuickMapCliffs orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.CLIFFS);
        }

        private static void GameMap_QuickMapCrossroads(On.GameMap.orig_QuickMapCrossroads orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.CROSSROADS);
        }

        private static void GameMap_QuickMapCrystalPeak(On.GameMap.orig_QuickMapCrystalPeak orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.MINES);
        }

        private static void GameMap_QuickMapDeepnest(On.GameMap.orig_QuickMapDeepnest orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.DEEPNEST);
        }

        private static void GameMap_QuickMapDirtmouth(On.GameMap.orig_QuickMapDirtmouth orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.TOWN);
        }

        private static void GameMap_QuickMapFogCanyon(On.GameMap.orig_QuickMapFogCanyon orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.FOG_CANYON);
        }

        private static void GameMap_QuickMapFungalWastes(On.GameMap.orig_QuickMapFungalWastes orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.WASTES);
        }

        private static void GameMap_QuickMapGreenpath(On.GameMap.orig_QuickMapGreenpath orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.GREEN_PATH);
        }

        private static void GameMap_QuickMapKingdomsEdge(On.GameMap.orig_QuickMapKingdomsEdge orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.OUTSKIRTS);
        }

        private static void GameMap_QuickMapQueensGardens(On.GameMap.orig_QuickMapQueensGardens orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.ROYAL_GARDENS);
        }

        private static void GameMap_QuickMapRestingGrounds(On.GameMap.orig_QuickMapRestingGrounds orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.RESTING_GROUNDS);
        }

        private static void GameMap_QuickMapWaterways(On.GameMap.orig_QuickMapWaterways orig, GameMap self)
        {
            orig(self);

            WorldMap.UpdateMap(self, MapZone.WATERWAYS);
        }

        // Replace all PlayerData boolNames with our own so we can show all Quick Maps,
        // without changing the existing PlayerData settings
        private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.FsmName == "Quick Map")
            {
                foreach (FsmState state in self.FsmStates)
                {
                    if (SettingsUtil.IsFSMMapState(state.Name)) {
                        string boolString = FsmUtil.GetAction<PlayerDataBoolTest>(state, 0).boolName.ToString();
                        FsmUtil.GetAction<PlayerDataBoolTest>(state, 0).boolName = "VMM_" + boolString;
                    }
                }
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