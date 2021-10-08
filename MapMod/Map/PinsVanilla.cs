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

            SetVanillaSprites(go_gameMap);
        }

        private static void SetVanillaSprites(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                if (child.name.Contains("pin") && !child.name.Contains("rando"))
                {
                    VanillaMapMod.Instance.Log(child.name);

                    try
                    {
                        switch (child.name)
                        {
                            case "pin_tram":
                                SetNewSprite(child.gameObject, "pinTramLocation");
                                break;
                            case "pin_spa":
                                SetNewSprite(child.gameObject, "pinSpa");
                                break;
                            case "pin_stag_station":
                            case "pin_stag_station (7)":
                                SetNewSprite(child.gameObject, "pinStag");
                                break;
                            case "pin_colosseum":
                                SetNewSprite(child.gameObject, "pinColosseum");
                                break;
                            case "pin_charm_slug":
                                SetNewSprite(child.gameObject, "pinCharmSlug");
                                break;
                            case "pin_grub_king":
                                SetNewSprite(child.gameObject, "pinGrubKing");
                                break;
                            case "pin_sly":
                            case "pin_sly (1)":
                                SetNewSprite(child.gameObject, "pinShopSly");
                                break;
                            case "pin_hunter":
                                SetNewSprite(child.gameObject, "pinShopBanker");
                                break;
                            case "pin_banker":
                                SetNewSprite(child.gameObject, "pinShopBanker");
                                break;
                            case "pin_leg eater":
                                SetNewSprite(child.gameObject, "pinShopLegEater");
                                break;
                            case "pin_jiji":
                                SetNewSprite(child.gameObject, "pinShopJiji");
                                break;
                            case "pin_mapper":
                                SetNewSprite(child.gameObject, "pinShopMapper");
                                break;
                            case "pin_dream moth":
                                SetNewSprite(child.gameObject, "pinEssenceBoss");
                                break;
                            case "pin_nailsmith":
                                SetNewSprite(child.gameObject, "pinShopNailsmith");
                                break;
                            case "pin_relic_dealer":
                                SetNewSprite(child.gameObject, "pinShopRelicDealer");
                                break;
                            case "pin_dream_tree":
                                SetNewSprite(child.gameObject, "pinRoot");

                                // Move Ancestral Mound root pin
                                if (child.transform.parent.name == "Crossroads_ShamanTemple")
                                {
                                    MoveSprite(child.gameObject, new Vector3(0.15f, -0.3f));
                                }
                                break;
                            case "Map Markers":
                                // Disable vanilla lifeblood pins so we can use our own
                            case "pin_blue_health":
                                child.transform.parent = null;
                                child.gameObject.SetActive(false);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        VanillaMapMod.Instance.LogError(e);
                    }
                }

                SetVanillaSprites(child.gameObject);
            }
        }

        public static void SetVanillaSpritesPersistent(GameObject obj)
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

                        // Move Ancestral Mound bench pin
                        if (child.transform.parent.name == "Crossroads_06")
                        {
                            MoveSprite(child.gameObject, new Vector3(-0.15f, -0.3f));
                        }
                    }
                }

                SetVanillaSpritesPersistent(child.gameObject);
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