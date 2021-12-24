using GlobalEnums;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using UnityEngine;
using VanillaMapMod.Settings;
using VanillaMapMod.Trackers;
using Vasi;

namespace VanillaMapMod.Map
{
    public static class WorldMap
    {
        public static GameObject goCustomPins = null;
        public static PinsCustom CustomPins => goCustomPins?.GetComponent<PinsCustom>();

        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
            On.GameMap.WorldMap += GameMap_WorldMap;
            On.GameMap.SetupMapMarkers += GameMap_SetupMapMarkers;
            On.GameMap.DisableMarkers += GameMap_DisableMarkers;
            On.GameManager.UpdateGameMap += UpdateGameMap;
        }

        // The function that is called every time a new GameMap is created (once per save load)
        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            if (self == null) return;

            // Necessary if player goes straight to Pause Menu
            SyncMap(self);

            if (goCustomPins != null)
            {
                goCustomPins.transform.SetParent(self.transform);
                return;
            }

            VanillaMapMod.Instance.Log("Adding Custom Pins...");

            goCustomPins = new GameObject($"VMM Custom Pin Group");
            goCustomPins.AddComponent<PinsCustom>();

            // Setting parent here is only for controlling local position,
            // not active/not active (need separate mechanism)
            goCustomPins.transform.SetParent(self.transform);

            CustomPins.MakePins(self);

            VanillaMapMod.Instance.Log("Adding Custom Pins done.");
        }

        // Called every time we open the World Map
        private static void GameMap_WorldMap(On.GameMap.orig_WorldMap orig, GameMap self)
        {
            orig(self);

            // Easiest way to force AdditionalMaps custom areas to show
            if (VanillaMapMod.LS.RevealFullMap)
            {
                foreach (Transform child in self.transform)
                {
                    if (child.name == "WHITE_PALACE"
                        || child.name == "GODS_GLORY")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            UpdateMap(self, MapZone.NONE);
        }

        // Following two behaviours necessary since GameMap is actually persistently active
        private static void GameMap_SetupMapMarkers(On.GameMap.orig_SetupMapMarkers orig, GameMap self)
        {
            orig(self);

            if (goCustomPins == null) return;

            CustomPins.gameObject.SetActive(true);
        }

        private static void GameMap_DisableMarkers(On.GameMap.orig_DisableMarkers orig, GameMap self)
        {
            if (goCustomPins == null) return;

            CustomPins.gameObject.SetActive(false);

            orig(self);
        }

        // The main method for updating map objects and pins when opening either World Map or Quick Map
        public static void UpdateMap(GameMap gameMap, MapZone mapZone)
        {
            ItemTracker.UpdateObtainedItems();

            PinsVanilla.UpdatePins(gameMap.gameObject);

            SyncMap(gameMap);

            PinsVanilla.RefreshGroups();
            PinsVanilla.ResizePins();

            if (goCustomPins == null) return;

            CustomPins.UpdatePins(mapZone);
            CustomPins.RefreshGroups();
            CustomPins.ResizePins();
        }

        public static void SyncMap(GameMap gameMap)
        {
            // If the mod is installed for an existing game
            SettingsUtil.SyncPlayerDataSettings();

            // Refresh map
            gameMap.SetupMap();
        }

        // Remove the "Map Updated" idle animation, since it occurs when the return value is true
        public static bool UpdateGameMap(On.GameManager.orig_UpdateGameMap orig, GameManager self)
        {
            orig(self);

            return false;
        }
    }
}