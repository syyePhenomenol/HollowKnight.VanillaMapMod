using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapMod
{
    public static class WorldMap
    {
        public static GameObject goPG = null;
        public static PinGroup PG => goPG?.GetComponent<PinGroup>();
        public static void Hook()
        {
            // This is the approximate order in which the callbacks occur
            On.GameMap.Start += GameMap_Start;
            On.GameManager.SetGameMap += GameManager_SetGameMap;
            On.GameMap.WorldMap += GameMap_WorldMap;
            On.GameMap.SetupMapMarkers += GameMap_SetupMapMarkers;
            On.GameMap.DisableMarkers += GameMap_DisableMarkers;
        }

        private static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            RemoveBuggyPins(self);
        }

        // AdditionalMaps also uses this hook, but because mods are loaded alphabetically VMM should be the second subscriber
        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            GameMap gameMap = go_gameMap.GetComponent<GameMap>();

            //foreach (Transform areaObj in go_gameMap.transform)
            //{
            //    Logger.Log($"-{areaObj.gameObject.name}");

            //    foreach (Transform roomObj in areaObj.transform)
            //    {
            //        Logger.Log($"--{roomObj.gameObject.name}");
            //    }
            //}

            foreach (FieldInfo field in typeof(PlayerData).GetFields().Where(field => field.Name.StartsWith("map") && field.FieldType == typeof(bool)))
            {
                PlayerData.instance.SetBool(field.Name, true);
            }

            ForceMapUpdate(gameMap);

            if (goPG == null)
            {
                MapMod.Instance.Log("Adding Pin Group and Populating...");
                goPG = new GameObject($"Map Mod Pin Group");
                goPG.AddComponent<PinGroup>();
                goPG.transform.SetParent(go_gameMap.GetComponent<GameMap>().transform);

                PG.MakePins(go_gameMap.GetComponent<GameMap>());
                MapMod.Instance.Log("Adding Pins done.");
            }
        }

        private static void GameMap_WorldMap(On.GameMap.orig_WorldMap orig, GameMap self)
        {
            orig(self);

            if (goPG != null)
            {
                PG.UpdatePins("WorldMap");
            }
        }

        private static void GameMap_SetupMapMarkers(On.GameMap.orig_SetupMapMarkers orig, GameMap self)
        {
            orig(self);

            if (goPG != null)
            {
                PG.Show();
            }
        }

        private static void GameMap_DisableMarkers(On.GameMap.orig_DisableMarkers orig, GameMap self)
        {
            if (goPG != null)
            {
                PG.Hide();
            }

            orig(self);
        }

        private static void RemoveBuggyPins(GameMap gameMap)
        {
            foreach (Transform child in gameMap.transform)
            {
                if (child.gameObject.name == "Map Markers")
                {
                    child.transform.parent = null;
                    child.gameObject.SetActive(false);

                }

                //Logger.Log(child.gameObject.name);
            }

            GameObject lifebloodPin = gameMap.transform.Find("Deepnest")?.Find("Deepnest_26")?.Find("pin_blue_health")?.gameObject;
            if (lifebloodPin != null)
            {
                lifebloodPin.transform.parent = null;
                lifebloodPin.SetActive(false);
            }
        }

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
                    MapMod.Instance.LogError(e);
                }
            }
        }
    }
}
