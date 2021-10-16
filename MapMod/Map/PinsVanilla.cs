using System;
using System.Collections.Generic;
using UnityEngine;
using VanillaMapMod.PauseMenu;

namespace VanillaMapMod.Map
{
    public static class PinsVanilla
    {
        public static void Hook()
        {
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
            On.GameManager.SetGameMap += GameManager_SetGameMap;
            On.GrubPin.Start += On_GrubPin_Start;
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

            GameObject mapKey = GameObject.Find("Map Key");
            mapKey.transform.parent = null;
            mapKey.SetActive(false);

            // Clear all pin references
            foreach (string group in _Groups.Keys)
            {
                _Groups[group].Clear();
            }

            SetupPins(go_gameMap);
        }

        private static readonly Dictionary<string, List<GameObject>> _Groups = new()
        {
            { "Bench", new() },
            { "Vendor", new() },
            { "Stag", new() },
            { "Spa", new() },
            { "Root", new() },
            { "Grave", new() },
            { "Tram", new() },
            { "Grub", new() },
        };

        private static void SetupPins(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                //VanillaMapMod.Instance.Log(child.name);

                try
                {
                    switch (child.name)
                    {
                        case "pin_bench":
                            // Sprite is set persistently in other function
                            _Groups["Bench"].Add(child.gameObject);
                            break;
                        case "pin_tram":
                            SetNewSprite(child.gameObject, "pinTramLocation");
                            _Groups["Tram"].Add(child.gameObject);
                            break;
                        case "pin_spa":
                            SetNewSprite(child.gameObject, "pinSpa");
                            _Groups["Spa"].Add(child.gameObject);
                            break;
                        case "pin_stag_station":
                        case "pin_stag_station (7)":
                            SetNewSprite(child.gameObject, "pinStag");
                            _Groups["Stag"].Add(child.gameObject);
                            break;
                        case "pin_colosseum":
                            SetNewSprite(child.gameObject, "pinColosseum");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_charm_slug":
                            SetNewSprite(child.gameObject, "pinCharmSlug");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_grub_king":
                            SetNewSprite(child.gameObject, "pinGrubKing");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_sly":
                            SetNewSprite(child.gameObject, "pinShopSly");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_sly (1)":
                            SetNewSprite(child.gameObject, "pinGodSeeker");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_hunter":
                            SetNewSprite(child.gameObject, "pinShopHunter");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_banker":
                            SetNewSprite(child.gameObject, "pinShopBanker");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_leg eater":
                            SetNewSprite(child.gameObject, "pinShopLegEater");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_jiji":
                            SetNewSprite(child.gameObject, "pinShopJiji");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_mapper":
                            SetNewSprite(child.gameObject, "pinShopMapper");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_dream moth":
                            SetNewSprite(child.gameObject, "pinEssenceBoss");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_nailsmith":
                            SetNewSprite(child.gameObject, "pinShopNailsmith");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_relic_dealer":
                            SetNewSprite(child.gameObject, "pinShopRelicDealer");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_dream_tree":
                            SetNewSprite(child.gameObject, "pinRoot");
                            _Groups["Root"].Add(child.gameObject);

                            // Move Ancestral Mound root pin
                            if (child.transform.parent.name == "Crossroads_ShamanTemple")
                            {
                                MoveSprite(child.gameObject, new Vector3(0.15f, -0.3f));
                            }

                            // Move Hive root pin (vanilla bug)
                            if (child.transform.parent.name == "Hive_02")
                            {
                                MoveSprite(child.gameObject, new Vector3(0.4f, -0.32f));
                            }

                            break;
                        case "Pin_Backer Ghost":
                            SetNewSprite(child.gameObject, "pinBackerGhost");
                            _Groups["Grave"].Add(child.gameObject);
                            break;
                        case "Map Markers":
                            child.gameObject.SetActive(false);
                            break;
                        case "Map Key":
                            child.transform.parent = null;
                            child.gameObject.SetActive(false);
                            break;
                            //    // Delete vanilla cocoon pins, make our own
                            //case "pin_blue_health":
                            //    child.gameObject.SetActive(false);
                            //    break;
                    }
                }
                catch (Exception e)
                {
                    VanillaMapMod.Instance.LogError(e);
                }

                SetupPins(child.gameObject);
            }
        }

        private static void On_GrubPin_Start(On.GrubPin.orig_Start orig, GrubPin self)
        {
            orig(self);

            SetNewSprite(self.gameObject, "pinGrub");
            _Groups["Grub"].Add(self.gameObject);
        }

        private static void On_GrubPin_OnEnable(On.GrubPin.orig_OnEnable orig, GrubPin self)
        {
            orig(self);

            //SetNewSprite(self.gameObject, "pinGrub");
            //_Groups["Grub"].Add(self.gameObject);

            if (!VanillaMapMod.LS.GetOnFromGroup("Grub"))
            {
                self.gameObject.SetActive(false);
            }
        }

        public static void SetPinsPersistent(GameObject obj)
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
                // Disable vanilla cocoon pins, make our own
                else if (child.gameObject.name == "pin_blue_health")
                {
                    child.gameObject.SetActive(false);
                }

                SetPinsPersistent(child.gameObject);
            }
        }

        public static void RefreshGroups()
        {
            foreach (string group in _Groups.Keys)
            {
                if (VanillaMapMod.LS.GetHasFromGroup(group))
                {
                    if (VanillaMapMod.LS.GetOnFromGroup(group))
                    {
                        foreach (GameObject pinObject in _Groups[group])
                        {
                            pinObject.SetActive(true);
                        }
                    }
                    else
                    {
                        foreach (GameObject pinObject in _Groups[group])
                        {
                            pinObject.SetActive(false);
                        }
                    }
                }
            }
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