using System;
using System.Linq;
using System.Reflection;
using VanillaMapMod.PauseMenu;
using VanillaMapMod.Trackers;
using UnityEngine;

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
        }

        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            //GiveFullMap(self);

            SyncPlayerDataSettings();

            if (self != null)
            {
                if (goCustomPins == null)
                {
                    VanillaMapMod.Instance.Log("Adding Pin Group and Populating...");
                    goCustomPins = new GameObject($"Map Mod Pin Group");
                    goCustomPins.AddComponent<PinsCustom>();
                    goCustomPins.transform.SetParent(self.transform);

                    CustomPins.MakePins(self);
                    VanillaMapMod.Instance.Log("Adding Pins done.");
                }
                else
                {
                    goCustomPins.transform.SetParent(self.transform);
                }

                GUIController.Setup();
                GUIController.Instance.BuildMenus();
            }
        }

        private static void GameMap_WorldMap(On.GameMap.orig_WorldMap orig, GameMap self)
        {
            orig(self);

            UpdateMap(self, "WorldMap");
        }

        private static void GameMap_SetupMapMarkers(On.GameMap.orig_SetupMapMarkers orig, GameMap self)
        {
            orig(self);

            if (goCustomPins != null)
            {
                CustomPins.Show();
            }
        }

        private static void GameMap_DisableMarkers(On.GameMap.orig_DisableMarkers orig, GameMap self)
        {
            if (goCustomPins != null)
            {
                CustomPins.Hide();
            }

            orig(self);
        }

        public static void UpdateMap(GameMap gameMap, string mapArea)
        {
            ItemTracker.UpdateObtainedItems();

            foreach (string scene in PlayerData.instance.scenesVisited)
            {
                if (!PlayerData.instance.scenesMapped.Contains(scene))
                {
                    PlayerData.instance.scenesMapped.Add(scene);
                }
            }

            gameMap.SetupMap();

            PinsVanilla.SetPinsPersistent(gameMap.gameObject);
            PinsVanilla.RefreshGroups();

            if (goCustomPins != null)
            {
                CustomPins.UpdatePins(mapArea);
                CustomPins.RefreshGroups();
            }
        }

        // If the mod is installed for an existing game
        public static void SyncPlayerDataSettings()
        {
            //foreach (string group in PinsVanilla.GetGroups())
            //{
            //    if (!VanillaMapMod.LS.GroupSettings.ContainsKey(group))
            //    {
            //        VanillaMapMod.LS.GroupSettings[group] = new();
            //    }
            //}

            foreach (string scene in PlayerData.instance.scenesVisited)
            {
                if (!PlayerData.instance.scenesMapped.Contains(scene))
                {
                    PlayerData.instance.scenesMapped.Add(scene);
                }
            }

            GameMap gameMap = GameObject.Find("Game_Map(Clone)").GetComponent<GameMap>();

            gameMap.SetupMap();

            VanillaMapMod.LS.GroupSettings["Bench"].Has = PlayerData.instance.hasPinBench;
            VanillaMapMod.LS.GroupSettings["Vendor"].Has = PlayerData.instance.hasPinShop;
            VanillaMapMod.LS.GroupSettings["Stag"].Has = PlayerData.instance.hasPinStag;
            VanillaMapMod.LS.GroupSettings["Spa"].Has = PlayerData.instance.hasPinSpa;
            VanillaMapMod.LS.GroupSettings["Root"].Has = PlayerData.instance.hasPinDreamPlant;
            VanillaMapMod.LS.GroupSettings["Grave"].Has = PlayerData.instance.hasPinGhost;
            VanillaMapMod.LS.GroupSettings["Tram"].Has = PlayerData.instance.hasPinTram;
            VanillaMapMod.LS.GroupSettings["Grub"].Has = PlayerData.instance.hasPinGrub;
            VanillaMapMod.LS.GroupSettings["Cocoon"].Has = PlayerData.instance.hasPinCocoon;
        }

        public static void GiveFullMap()
        {
            PlayerData.instance.hasMap = true;

            foreach (FieldInfo field in typeof(PlayerData).GetFields().Where(field => field.Name.StartsWith("map") && field.FieldType == typeof(bool)))
            {
                PlayerData.instance.SetBool(field.Name, true);
            }

            foreach (FieldInfo field in typeof(PlayerData).GetFields().Where(field => field.Name.StartsWith("corn") && field.Name.EndsWith("Left")))
            {
                PlayerData.instance.SetBool(field.Name, true);
            }

            // NPC events
            PlayerData.instance.metBanker = true;
            PlayerData.instance.metCharmSlug = true;
            PlayerData.instance.metHunter = true;
            PlayerData.instance.metCornifer = true;
            PlayerData.instance.corniferIntroduced = true;
            PlayerData.instance.corniferAtHome = true;
            PlayerData.instance.jijiMet = true;
            PlayerData.instance.metJinn = true;
            PlayerData.instance.metLegEater = true;
            PlayerData.instance.metElderbug = true;
            PlayerData.instance.openedMapperShop = true;
            PlayerData.instance.metIselda = true;
            PlayerData.instance.metMoth = true;
            PlayerData.instance.metNailsmith = true;
            PlayerData.instance.metRelicDealer = true;
            PlayerData.instance.openedSlyShop = true;
            PlayerData.instance.metSlyShop = true;

            // Unlock pins
            PlayerData.instance.hasQuill = true;
            PlayerData.instance.hasPin = true;
            PlayerData.instance.hasPinBench = true;
            PlayerData.instance.hasPinBlackEgg = true;
            PlayerData.instance.hasPinCocoon = true;
            PlayerData.instance.hasPinDreamPlant = true;
            PlayerData.instance.hasPinGhost = true;
            PlayerData.instance.hasPinGrub = true;
            PlayerData.instance.hasPinGuardian = true;
            PlayerData.instance.hasPinShop = true;
            PlayerData.instance.hasPinSpa = true;
            PlayerData.instance.hasPinStag = true;
            PlayerData.instance.hasPinTram = true;

            // Reveal root and warrior pins
            foreach (string rootScene in _rootScenes)
            {
                if (!PlayerData.instance.scenesEncounteredDreamPlant.Contains(rootScene))
                {
                    PlayerData.instance.scenesEncounteredDreamPlant.Add(rootScene);
                }
            }

            PlayerData.instance.aladarPinned = true;
            PlayerData.instance.galienPinned = true;
            PlayerData.instance.huPinned = true;
            PlayerData.instance.markothPinned = true;
            PlayerData.instance.mumCaterpillarPinned = true;
            PlayerData.instance.noEyesPinned = true;
            PlayerData.instance.xeroPinned = true;

            VanillaMapMod.LS.SetFullMap();

            GameMap gameMap = GameObject.Find("Game_Map(Clone)").GetComponent<GameMap>();

            gameMap.SetupMap();

            PauseGUI.SetGUI();
        }

        private static readonly string[] _rootScenes =
        {
            "Crossroads_07",
            "Fungus1_13",
            "Fungus2_33",
            "Fungus2_17",
            "Deepnest_39",
            "Fungus3_11",
            "Deepnest_East_07",
            "Abyss_01",
            "Ruins1_17",
            "RestingGrounds_05",
            "RestingGrounds_08",
            "Mines_23",
            "Cliffs_01",
            "Crossroads_ShamanTemple",
            "Hive_02"
        };

        //private static void ForceMapUpdate(GameMap gameMap)
        //{
        //    PlayerData pd = PlayerData.instance;

        //    if (!pd.hasQuill)
        //    {
        //        try
        //        {
        //            // Give Quill, because it's required to...
        //            pd.SetBool(nameof(pd.hasQuill), true);

        //            // ... uncover the full map
        //            gameMap.SetupMap();

        //            // Remove Quill
        //            pd.SetBool(nameof(pd.hasQuill), false);
        //        }
        //        catch (Exception e)
        //        {
        //            VanillaMapMod.Instance.LogError(e);
        //        }
        //    }
        //}
    }
}