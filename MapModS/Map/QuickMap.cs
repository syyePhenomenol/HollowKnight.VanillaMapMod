using System.Linq;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using MapModS.Settings;
using Vasi;

namespace MapModS.Map
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
            On.GameManager.SetGameMap += GameManager_SetGameMap;
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

        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            GameObject quickMapGameObject = GameObject.Find("Quick Map");
            PlayMakerFSM quickMapFSM = quickMapGameObject.LocateMyFSM("Quick Map");

            // Replace all PlayerData boolNames with our own so we can show all Quick Maps,
            // without changing the existing PlayerData settings

            foreach (FsmState state in quickMapFSM.FsmStates)
            {
                if (SettingsUtil.IsFSMMapState(state.Name))
                {
                    string boolString = FsmUtil.GetAction<PlayerDataBoolTest>(state, 0).boolName.ToString();
                    FsmUtil.GetAction<PlayerDataBoolTest>(state, 0).boolName = "VMM_" + boolString;
                }
            }

            // Patch custom area quick map behaviour

            GameMap gameMap = go_gameMap.GetComponent<GameMap>();

            if (quickMapFSM.FsmStates.Any(state => state.Name == "WHITE_PALACE"))
            {
                MapModS.Instance.Log("AdditionalMaps WHITE_PALACE area detected");
                FsmUtil.AddAction(FsmUtil.GetState(quickMapFSM, "WHITE_PALACE"), new QuickMapCustomArea(MapZone.WHITE_PALACE, gameMap));
            }

            if (quickMapFSM.FsmStates.Any(state => state.Name == "GODS_GLORY"))
            {
                MapModS.Instance.Log("AdditionalMaps GODS_GLORY area detected");
                FsmUtil.AddAction(FsmUtil.GetState(quickMapFSM, "GODS_GLORY"), new QuickMapCustomArea(MapZone.GODS_GLORY, gameMap));
            }
        }
    }

    public class QuickMapCustomArea : FsmStateAction
    {
        private readonly MapZone _customMapZone;
        private readonly GameMap _GameMap;
        public QuickMapCustomArea(MapZone mapZone, GameMap gameMap)
        {
            _customMapZone = mapZone;
            _GameMap = gameMap;
        }

        public override void OnEnter()
        {
            // Need to force show the map for RevealFullMap
            foreach (Transform child in _GameMap.transform)
            {
                if (child.name == _customMapZone.ToString())
                {
                    child.gameObject.SetActive(true);
                }
            }

            WorldMap.UpdateMap(_GameMap, _customMapZone);
            _GameMap.SetupMapMarkers();

            Finish();
        }
    }
}
