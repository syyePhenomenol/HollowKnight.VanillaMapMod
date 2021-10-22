using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                WorldMap.ReplaceBool(self, "Check", 1);
            }
            else if (self.gameObject.name == "Pin_Backer Ghost" && self.FsmName == "FSM")
            {
                WorldMap.ReplaceBool(self, "Check", 1);
                WorldMap.ReplaceBool(self, "Check", 3);
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
                WorldMap.ReplaceBool(self, "Check", 0);
                WorldMap.ReplaceBool(self, "Check", 2);
            }
            else if ((self.gameObject.name == "pin_bench" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_BlackEgg" && self.FsmName == "FSM"))
            {
                if (self.FsmStates.FirstOrDefault(t => t.Name == "Check") != null)
                {
                    WorldMap.ReplaceBool(self, "Check", 0);
                }
            }
            else if ((self.gameObject.name == "Pin_Beast" && self.FsmName == "Display")
                || (self.gameObject.name == "Pin_Teacher" && self.FsmName == "Display")
                || (self.gameObject.name == "Pin_Watcher" && self.FsmName == "Display"))
            {
                WorldMap.ReplaceBool(self, "Init", 1);
            }
            else if ((self.gameObject.name == "Pin_Beast" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_Teacher" && self.FsmName == "FSM")
                || (self.gameObject.name == "Pin_Watcher" && self.FsmName == "FSM"))
            {
                if (self.FsmStates.FirstOrDefault(t => t.Name == "Deactivate") != null)
                {
                    WorldMap.ReplaceBool(self, "Check", 0);
                }
            }
        }

        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            // Disable map key since the UI is too busy otherwise
            GameObject mapKey = GameObject.Find("Map Key");
            mapKey.transform.parent = null;
            mapKey.SetActive(false);

            // Clear all pin references from previous save load
            foreach (string group in _Groups.Keys)
            {
                _Groups[group].Clear();
            }

            SetupPins(go_gameMap);
        }

        private static readonly Dictionary<string, List<GameObject>> _Groups = new()
        {
            { "Bench", new() },
            { "Grave", new() },
            { "Grub", new() },
            { "Root", new() },
            { "Spa", new() },
            { "Stag", new() },
            { "Tram", new() },
            { "Vendor", new() },
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
                            // Sprite is set persistently in other function
                            _Groups["Bench"].Add(child.gameObject);
                            break;
                        case "Pin_Backer Ghost":
                            SetNewSprite(child.gameObject, "pinBackerGhost");
                            _Groups["Grave"].Add(child.gameObject);
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
                        case "pin_spa":
                            SetNewSprite(child.gameObject, "pinSpa");
                            _Groups["Spa"].Add(child.gameObject);
                            break;
                        case "pin_stag_station":
                        case "pin_stag_station (7)":
                            SetNewSprite(child.gameObject, "pinStag");
                            _Groups["Stag"].Add(child.gameObject);
                            break;
                        case "pin_tram":
                            SetNewSprite(child.gameObject, "pinTramLocation");
                            _Groups["Tram"].Add(child.gameObject);
                            break;
                        case "pin_banker":
                            SetNewSprite(child.gameObject, "pinShopBanker");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_charm_slug":
                            SetNewSprite(child.gameObject, "pinCharmSlug");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_colosseum":
                            SetNewSprite(child.gameObject, "pinColosseum");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_dream moth":
                            SetNewSprite(child.gameObject, "pinEssenceBoss");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_grub_king":
                            SetNewSprite(child.gameObject, "pinGrubKing");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_hunter":
                            SetNewSprite(child.gameObject, "pinShopHunter");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_jiji":
                            SetNewSprite(child.gameObject, "pinShopJiji");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_leg eater":
                            SetNewSprite(child.gameObject, "pinShopLegEater");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        case "pin_mapper":
                            SetNewSprite(child.gameObject, "pinShopMapper");
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
                        case "pin_sly":
                            SetNewSprite(child.gameObject, "pinShopSly");
                            _Groups["Vendor"].Add(child.gameObject);
                            break;
                        // This is actually Godtuner
                        case "pin_sly (1)":
                            SetNewSprite(child.gameObject, "pinGodSeeker");
                            _Groups["Vendor"].Add(child.gameObject);
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
                    VanillaMapMod.Instance.LogError(e);
                }

                SetupPins(child.gameObject);
            }
        }

        // Grub pins are handled separately, since it's convenient to use the existing hook
        private static void On_GrubPin_Start(On.GrubPin.orig_Start orig, GrubPin self)
        {
            orig(self);

            SetNewSprite(self.gameObject, "pinGrub");
            _Groups["Grub"].Add(self.gameObject);
        }

        // Needed to properly disable Grub pins when turned off
        private static void On_GrubPin_OnEnable(On.GrubPin.orig_OnEnable orig, GrubPin self)
        {
            orig(self);

            if (!VanillaMapMod.LS.GetOnFromGroup("Grub"))
            {
                self.gameObject.SetActive(false);
            }
        }

        // Recursive function that sets/adjusts pins every time the map is opened
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

                SetPinsPersistent(child.gameObject);
            }
        }

        public static void RefreshGroups()
        {
            foreach (string group in _Groups.Keys)
            {
                if (VanillaMapMod.LS.GetHasFromGroup(group) || VanillaMapMod.LS.RevealFullMap)
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

        // Only used for adjusting pin size
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