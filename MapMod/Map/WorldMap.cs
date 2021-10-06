using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapMod.Map
{
    public static class WorldMap
    {

        public static GameObject goCustomPins = null;
        public static PinsCustom CustomPins => goCustomPins?.GetComponent<PinsCustom>();
        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
            //On.GameManager.SetGameMap += GameManager_SetGameMap;
            On.GameMap.WorldMap += GameMap_WorldMap;
            On.GameMap.SetupMapMarkers += GameMap_SetupMapMarkers;
            On.GameMap.DisableMarkers += GameMap_DisableMarkers;
        }

        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            //PlayerData.instance.hasMap = true;

            //foreach (FieldInfo field in typeof(PlayerData).GetFields().Where(field => field.Name.StartsWith("map") && field.FieldType == typeof(bool)))
            //{
            //    PlayerData.instance.SetBool(field.Name, true);
            //}

            //PlayerData.instance.CheckAllMaps();
            //PlayerData.instance.UpdateGameMap();

            //PlayerData.instance.hasPin = true;
            //PlayerData.instance.hasPinBench = true;
            //PlayerData.instance.hasPinBlackEgg = true;
            //PlayerData.instance.hasPinCocoon = true;
            //PlayerData.instance.hasPinDreamPlant = true;
            //PlayerData.instance.hasPinGhost = true;
            //PlayerData.instance.hasPinGrub = true;
            //PlayerData.instance.hasPinGuardian = true;
            //PlayerData.instance.hasPinShop = true;
            //PlayerData.instance.hasPinSpa = true;
            //PlayerData.instance.hasPinStag = true;
            //PlayerData.instance.hasPinTram = true;
            //PlayerData.instance.collectorDefeated = true;

            //MapMod.LS.GetAllPins();

            //ForceMapUpdate(self);

            if (goCustomPins == null)
            {
                MapMod.Instance.Log("Adding Pin Group and Populating...");
                goCustomPins = new GameObject($"Map Mod Pin Group");
                goCustomPins.AddComponent<PinsCustom>();
                goCustomPins.transform.SetParent(self.transform);

                CustomPins.MakePins(self);
                MapMod.Instance.Log("Adding Pins done.");
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

            PinsVanilla.SetBenchSpritesRecursive(self.gameObject);

            if (goCustomPins != null)
            {
                CustomPins.UpdatePins("WorldMap");
            }

            self.SetupMap();
        }

        private static void GameMap_SetupMapMarkers(On.GameMap.orig_SetupMapMarkers orig, GameMap self)
        {
            orig(self);

            if (goCustomPins == null)
            {

            }
            else
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
        //            MapMod.Instance.LogError(e);
        //        }
        //    }
        //}
    }
}
