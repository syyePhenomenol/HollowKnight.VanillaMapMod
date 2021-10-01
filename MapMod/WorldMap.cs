using System;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HutongGames.PlayMaker.Actions;
using Logger = Modding.Logger;

namespace MapMod
{
    public static class WorldMap
    {
        public static GameObject goPG = null;
        public static PinGroup PG => goPG?.GetComponent<PinGroup>();
        public static void Hook()
        {
            On.GameMap.Start += GameMap_Start;
            On.GameMap.WorldMap += GameMap_WorldMap;
            On.GameMap.SetupMapMarkers += GameMap_SetupMapMarkers;
            On.GameMap.DisableMarkers += GameMap_DisableMarkers;

            //if (callback != null) callbacks.Add(callback);
        }

        public static void GameMap_Start(On.GameMap.orig_Start orig, GameMap self)
        {
            orig(self);

            Logger.Log(self.name);

            RemoveBuggyPins(self);
            ForceMapUpdate(self);

            foreach (FieldInfo field in typeof(PlayerData).GetFields().Where(field => field.Name.StartsWith("map") && field.FieldType == typeof(bool)))
            {
                PlayerData.instance.SetBool(field.Name, true);
            }

            if (goPG == null)
            {
                Logger.Log("Adding Pin Group and Populating...");
                goPG = new GameObject($"Map Mod Pin Group");
                goPG.AddComponent<PinGroup>();
                goPG.transform.SetParent(self.transform);

                PG.MakePins(self);
            }
        }

        public static void GameMap_WorldMap(On.GameMap.orig_WorldMap orig, GameMap self)
        {
            orig(self);

            PG.UpdatePins("WorldMap");

            Logger.Log(self.name);
        }

        public static void GameMap_SetupMapMarkers(On.GameMap.orig_SetupMapMarkers orig, GameMap self)
        {
            orig(self);

            PG.Show();
        }

        public static void GameMap_DisableMarkers(On.GameMap.orig_DisableMarkers orig, GameMap self)
        {
            PG.Hide();

            orig(self);
        }

        public static void RemoveBuggyPins(GameMap gameMap)
        {
            foreach (Transform child in gameMap.transform)
            {
                if (child.gameObject.name == "Map Markers")
                {
                    child.transform.parent = null;
                    child.gameObject.SetActive(false);

                }

                Logger.Log(child.gameObject.name);
            }

            GameObject lifebloodPin = gameMap.transform.Find("Deepnest")?.Find("Deepnest_26")?.Find("pin_blue_health")?.gameObject;
            if (lifebloodPin != null)
            {
                lifebloodPin.transform.parent = null;
                lifebloodPin.SetActive(false);
            }
        }

        public static void ForceMapUpdate(GameMap gameMap)
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
                    Logger.LogError(e);
                }
            }
        }
    }
}
