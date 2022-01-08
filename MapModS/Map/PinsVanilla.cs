using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MapModS.Settings;
using MapModS.Data;

namespace MapModS.Map
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

        // Replace all PlayerData boolNames with our own so we can force enable all pins,
        // without changing the existing PlayerData settings
        private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.FsmName == "toll_bench_pin"
                || (self.gameObject.name == "Crossroads_02" && self.FsmName == "Dreamer Pin"))
            {
                self.enabled = false;
            }
            else if (self.FsmName == "Check Grub Map Owned")
            {
                FullMap.ReplaceBool(self, "Check", 1);
            }
            else if (self.gameObject.name == "Pin_Backer Ghost" && self.FsmName == "FSM")
            {
                FullMap.ReplaceBool(self, "Check", 1);
                FullMap.ReplaceBool(self, "Check", 3);
            }
            else if ((self.gameObject.name == "pin_banker" && self.FsmName == "pin_activation")
                || (self.gameObject.name == "pin_charm_slug" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_colosseum" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_dream moth" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_grub_king" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_hunter" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_jiji" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_leg eater" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_mapper" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_nailsmith" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_relic_dealer" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_sly (1)" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_sly" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_spa" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_stag_station (7)" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_stag_station" && self.FsmName == "FSM")
                || (self.gameObject.name == "pin_tram" && self.FsmName == "FSM"))
            {
                FullMap.ReplaceBool(self, "Check", 0);
                FullMap.ReplaceBool(self, "Check", 2);
            }
            else if ((self.gameObject.name == "pin_bench" && self.FsmName == "FSM")
                // For AdditionalMaps
                || (self.gameObject.name == "pin_bench(Clone)(Clone)" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_BlackEgg" && self.FsmName == "FSM"))
            {
                if (self.FsmStates.FirstOrDefault(t => t.Name == "Check") != null)
                {
                    FullMap.ReplaceBool(self, "Check", 0);
                }
            }
            else if ((self.gameObject.name == "Pin_Beast" && self.FsmName == "Display")
                || (self.gameObject.name == "Pin_Teacher" && self.FsmName == "Display")
                || (self.gameObject.name == "Pin_Watcher" && self.FsmName == "Display"))
            {
                FullMap.ReplaceBool(self, "Init", 1);
            }
            else if ((self.gameObject.name == "Pin_Beast" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_Teacher" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_Watcher" && self.FsmName == "FSM"))
            {
                if (self.FsmStates.FirstOrDefault(t => t.Name == "Deactivate") != null)
                {
                    FullMap.ReplaceBool(self, "Check", 0);
                }
            }
        }

        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            // At this point, if AdditionalMaps is installed, the new custom areas have been added

            // Disable map key since the UI is too busy otherwise
            GameObject mapKey = GameObject.Find("Map Key");
            mapKey.transform.parent = null;
            mapKey.SetActive(false);

            // Clear all pin references from previous save load
            foreach (Pool group in _Groups.Keys)
            {
                _Groups[group].Clear();
            }

            SetupPins(go_gameMap);
        }

        private static readonly Dictionary<Pool, List<GameObject>> _Groups = new()
        {
            { Pool.Bench, new() },
            { Pool.Grave, new() },
            { Pool.Grub, new() },
            { Pool.Root, new() },
            { Pool.Spa, new() },
            { Pool.Stag, new() },
            { Pool.Tram, new() },
            { Pool.Vendor, new() },
        };

        // Add pins to their respective groups for easier reference later.
        // This is recursive and is done once per save load
        private static void SetupPins(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                try
                {
                    switch (child.name)
                    {
                        case "pin_bench":
                        // For AdditionalMaps compatibility
                        case "pin_bench(Clone)(Clone)":
                            // Sprite is set persistently in other function
                            _Groups[Pool.Bench].Add(child.gameObject);
                            break;
                        case "Pin_Backer Ghost":
                            SetNewSprite(child.gameObject, "pinBackerGhost");
                            _Groups[Pool.Grave].Add(child.gameObject);
                            break;
                        case "pin_dream_tree":
                            SetNewSprite(child.gameObject, "pinRoot");
                            _Groups[Pool.Root].Add(child.gameObject);

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
                        case "pin_spa":
                            SetNewSprite(child.gameObject, "pinSpa");
                            _Groups[Pool.Spa].Add(child.gameObject);
                            break;
                        case "pin_stag_station":
                        case "pin_stag_station (7)":
                            SetNewSprite(child.gameObject, "pinStag");
                            _Groups[Pool.Stag].Add(child.gameObject);
                            break;
                        case "pin_tram":
                            SetNewSprite(child.gameObject, "pinTramLocation");
                            _Groups[Pool.Tram].Add(child.gameObject);
                            break;
                        case "pin_banker":
                            SetNewSprite(child.gameObject, "pinShopBanker");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_charm_slug":
                            SetNewSprite(child.gameObject, "pinCharmSlug");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_colosseum":
                            SetNewSprite(child.gameObject, "pinColosseum");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_dream moth":
                            SetNewSprite(child.gameObject, "pinEssenceBoss");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_grub_king":
                            SetNewSprite(child.gameObject, "pinGrubKing");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_hunter":
                            SetNewSprite(child.gameObject, "pinShopHunter");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_jiji":
                            SetNewSprite(child.gameObject, "pinShopJiji");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_leg eater":
                            SetNewSprite(child.gameObject, "pinShopLegEater");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_mapper":
                            SetNewSprite(child.gameObject, "pinShopMapper");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_nailsmith":
                            SetNewSprite(child.gameObject, "pinShopNailsmith");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_relic_dealer":
                            SetNewSprite(child.gameObject, "pinShopRelicDealer");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        case "pin_sly":
                            SetNewSprite(child.gameObject, "pinShopSly");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        // This is actually Godtuner
                        case "pin_sly (1)":
                            SetNewSprite(child.gameObject, "pinGodSeeker");
                            _Groups[Pool.Vendor].Add(child.gameObject);
                            break;
                        // These are super buggy in vanilla!
                        case "Map Markers":
                            child.gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    MapModS.Instance.LogError(e);
                }

                SetupPins(child.gameObject);
            }
        }

        // Grub pins are handled separately, since it's convenient to use the existing hook
        private static void On_GrubPin_Start(On.GrubPin.orig_Start orig, GrubPin self)
        {
            orig(self);

            SetNewSprite(self.gameObject, "pinGrub");
            _Groups[Pool.Grub].Add(self.gameObject);
        }

        // Needed to properly disable Grub pins when turned off
        private static void On_GrubPin_OnEnable(On.GrubPin.orig_OnEnable orig, GrubPin self)
        {
            orig(self);

            if (!MapModS.LS.GetOnFromGroup(Pool.Grub))
            {
                self.gameObject.SetActive(false);
            }
        }

        // Recursive function that sets/adjusts pins every time the map is opened
        public static void UpdatePins(GameObject obj)
        {
            if (obj == null)
                return;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;

                if (child.gameObject.name == "pin_bench" || child.gameObject.name == "pin_bench(Clone)(Clone)")
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
                else if (child.gameObject.name == "Pin_Black_Egg")
                {
                    child.gameObject.SetActive(PlayerData.instance.GetBool("VMM_hasPinBlackEgg"));
                }
                // Disable vanilla cocoon pins, make our own
                else if (child.gameObject.name == "pin_blue_health")
                {
                    child.gameObject.SetActive(false);
                }
                // Disable to refresh roots if Reveal Map is turned off
                else if (child.gameObject.name == "pin_dream_tree")
                {
                    child.gameObject.SetActive(false);
                }

                UpdatePins(child.gameObject);
            }
        }

        // Called every time the map is opened
        public static void RefreshGroups()
        {
            foreach (Pool group in _Groups.Keys)
            {
                _Groups[group].RemoveAll(item => item == null);

                if (MapModS.LS.GetHasFromGroup(group) || MapModS.LS.RevealFullMap)
                {
                    if (MapModS.LS.GetOnFromGroup(group))
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

            // Hot fix for Roots showing even when completed
            foreach (GameObject rootPin in _Groups[Pool.Root])
            {
                if (PlayerData.instance.scenesEncounteredDreamPlantC.Contains(rootPin.transform.parent.name))
                {
                    rootPin.SetActive(false);
                }
            }
        }

        public static void ResizePins()
        {
            foreach (Pool group in _Groups.Keys)
            {
                _Groups[group].RemoveAll(item => item == null);

                foreach (GameObject pinObject in _Groups[group])
                {
                    ResizePin(pinObject);
                }
            }
        }

        // Only used for adjusting pin size
        private static void SetNewSprite(GameObject go, string spriteName)
        {
            go.GetComponent<SpriteRenderer>().sprite = SpriteManager.GetSprite(spriteName);

            ResizePin(go);
        }

        public static void ResizePin(GameObject go)
        {
            float scale = MapModS.GS.PinSizeSetting switch
            {
                GlobalSettings.PinSize.small => 0.31f,
                GlobalSettings.PinSize.medium => 0.37f,
                GlobalSettings.PinSize.large => 0.42f,
                _ => throw new NotImplementedException()
            };

            go.transform.localScale = scale * new Vector2(1.0f, 1.0f);
        }

        private static void MoveSprite(GameObject go, Vector3 offset)
        {
            go.transform.localPosition = offset;
        }
    }
}