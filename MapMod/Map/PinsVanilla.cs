using System;
using UnityEngine;

namespace VanillaMapMod.Map
{
    public static class PinsVanilla
    {
        public static void Hook()
        {
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
            On.GameManager.SetGameMap += GameManager_SetGameMap;
            On.GrubPin.OnEnable += On_GrubPin_OnEnable;
        }

        private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            // Disable toll bench FSM behaviour so we can implement our own check
            if (self.FsmName == "toll_bench_pin")
            {
                self.enabled = false;
            }
        }

        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            //GameMap gameMap = go_gameMap.GetComponent<GameMap>();

            //GameObject lifebloodPin = gameMap.transform.Find("Deepnest")?.Find("Deepnest_26")?.Find("pin_blue_health")?.gameObject;
            //if (lifebloodPin != null)
            //{
            //    lifebloodPin.transform.parent = null;
            //    lifebloodPin.SetActive(false);
            //}

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.name == "Map Markers"
                    || go.name == "Map Key")
                {
                    go.transform.parent = null;
                    go.gameObject.SetActive(false);
                }
            }

            SetVanillaSpritesRecursive(go_gameMap);
        }

        private static void SetVanillaSpritesRecursive(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                if (child.name.Contains("pin") && !child.name.Contains("rando"))
                {
                    //MapMod.Instance.Log(child.gameObject.name);

                    try
                    {
                        if (child.name == "pin_tram")
                        {
                            SetNewSprite(child.gameObject, "pinTramLocation");
                        }
                        else if (child.name == "pin_spa")
                        {
                            SetNewSprite(child.gameObject, "pinSpa");
                        }
                        else if (child.name.Contains("pin_stag_station"))
                        {
                            SetNewSprite(child.gameObject, "pinStag");
                        }
                        else if (child.name == "pin_colosseum")
                        {
                            SetNewSprite(child.gameObject, "pinColosseum");
                        }
                        else if (child.name == "pin_grub_king")
                        {
                            SetNewSprite(child.gameObject, "pinGrubKing");
                        }
                        else if (child.name == "pin_mapper")
                        {
                            SetNewSprite(child.gameObject, "pinShopMapper");
                        }
                        else if (child.name == "pin_dream_tree")
                        {
                            SetNewSprite(child.gameObject, "pinRoot");

                            if (child.transform.parent.name == "Crossroads_ShamanTemple")
                            {
                                MoveSprite(child.gameObject, new Vector3(0.15f, -0.3f));
                            }
                        }
                        else if (child.name == "Map Markers"
                            || child.name == "pin_blue_health")
                        {
                            child.transform.parent = null;
                            child.gameObject.SetActive(false);
                        }
                    }
                    catch (Exception e)
                    {
                        VanillaMapMod.Instance.LogError(e);
                    }
                }

                SetVanillaSpritesRecursive(child.gameObject);
            }
        }

        public static void SetBenchSpritesRecursive(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                if (child.gameObject.name == "pin_bench")
                {
                    if (child.parent.name == "Fungus3_50" && !PlayerData.instance.tollBenchQueensGardens
                        || child.parent.name == "Abyss_18" && !PlayerData.instance.tollBenchAbyss
                        || child.parent.name == "Ruins1_31" && !PlayerData.instance.tollBenchCity)
                    {
                        SetNewSprite(child.gameObject, "pinBenchGrey");
                    }
                    else
                    {
                        SetNewSprite(child.gameObject, "pinBench");

                        if (child.transform.parent.name == "Crossroads_06")
                        {
                            MoveSprite(child.gameObject, new Vector3(-0.15f, -0.3f));
                        }
                    }

                    //MapMod.Instance.Log(child.transform.parent.name);
                }

                SetBenchSpritesRecursive(child.gameObject);
            }
        }

        private static void On_GrubPin_OnEnable(On.GrubPin.orig_OnEnable orig, GrubPin self)
        {
            orig(self);

            SetNewSprite(self.gameObject, "pinGrub");
        }

        private static void SetNewSprite(GameObject go, string spriteName)
        {
            go.GetComponent<SpriteRenderer>().sprite = SpriteManager.GetSprite(spriteName);

            go.transform.localScale = new Vector2(VanillaMapMod.GS.PinScaleSize, VanillaMapMod.GS.PinScaleSize);
        }

        private static void MoveSprite(GameObject go, Vector3 offset)
        {
            go.transform.localPosition = offset;
        }
    }
}