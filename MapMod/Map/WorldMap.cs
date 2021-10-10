using System;
using System.Linq;
using System.Reflection;
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

            GiveFullMap(self);

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
        }

        //private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        //{
        //    orig(self, go_gameMap);
        //}

        //private static void GetChildRecursive(GameObject obj)
        //{
        //    if (obj == null)
        //        return;

        //    foreach (Transform child in obj.transform)
        //    {
        //        if (child == null)
        //            continue;

        //        if (child.name.Contains("pin") || child.name.Contains("Grub"))
        //        {
        //            MapMod.Instance.Log(child.gameObject.name);

        //            try
        //            {
        //                PlayMakerFSM[] FSMs = child.gameObject.GetComponents<PlayMakerFSM>();

        //                foreach (PlayMakerFSM FSM in FSMs)
        //                {
        //                    MapMod.Instance.Log("-" + FSM.FsmName);

        //                    foreach (FsmState state in FSM.FsmStates)
        //                    {
        //                        MapMod.Instance.Log("- -" + state.Name);
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                MapMod.Instance.LogError(e);
        //            }
        //        }

        //        GetChildRecursive(child.gameObject);
        //    }
        //}

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

            PinsVanilla.SetVanillaSpritesPersistent(gameMap.gameObject);

            if (goCustomPins != null)
            {
                CustomPins.UpdatePins(mapArea);
            }

            foreach (string rootScene in PlayerData.instance.scenesEncounteredDreamPlant)
            {
                VanillaMapMod.Instance.Log(rootScene);
            }

            foreach (string rootScene in PlayerData.instance.scenesEncounteredDreamPlantC)
            {
                VanillaMapMod.Instance.Log(rootScene);
            }
        }

        public static void GiveFullMap(GameMap gameMap)
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

            //foreach (string rootScene in PlayerData.instance.scenesEncounteredDreamPlant)
            //{
            //    VanillaMapMod.Instance.Log(rootScene);
            //}

            //foreach (string rootScene in PlayerData.instance.scenesEncounteredDreamPlantC)
            //{
            //    VanillaMapMod.Instance.Log(rootScene);
            //}

            VanillaMapMod.LS.SetFullMap();

            ForceMapUpdate(gameMap);
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

        private static void ForceMapUpdate(GameMap gameMap)
        {
            PlayerData pd = PlayerData.instance;

            if (!pd.hasQuill)
            {
                try
                {
                    // Give Quill, because it's required to...
                    pd.SetBool(nameof(pd.hasQuill), true);

                    // ... uncover the full map
                    gameMap.SetupMap();

                    // Remove Quill
                    pd.SetBool(nameof(pd.hasQuill), false);
                }
                catch (Exception e)
                {
                    VanillaMapMod.Instance.LogError(e);
                }
            }
        }
    }
}