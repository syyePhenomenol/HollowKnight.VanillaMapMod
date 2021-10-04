using System;
using HutongGames.PlayMaker;
using MapMod.Resources;
using SFCore.Utils;
using UnityEngine;

namespace MapMod.Map
{
    public static class PinsVanilla
    {
        public static void Hook()
        {
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;

            On.GrubPin.OnEnable += On_GrubPin_OnEnable;

            On.GameManager.SetGameMap += GameManager_SetGameMap;
        }

        private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            try
            {
                if (self.gameObject.name.Contains("pin_") || self.gameObject.name.Contains("Grub"))
                {
                    if (self.FsmName == "toll_bench_pin")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinBench"));
                        FsmUtil.AddAction(self, "Inactive", new ReplaceSprite(self, "pinBenchGrey"));
                    }
                    else if (self.gameObject.name.Contains("pin_bench"))
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinBench"));
                    }
                    //else if (self.FsmName == "Check Grub Map Owned")
                    //{
                    //    FsmUtil.AddAction(self, "Active", new ReplaceSprite(self, "pinGrub"));
                    //}
                    else if (self.gameObject.name == "pin_charm_slug")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinCharmSlug"));
                    }
                    else if (self.gameObject.name.Contains("pin_stag_station"))
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinStag"));
                    }
                    else if (self.gameObject.name == "pin_grub_king")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinGrubKing"));
                    }
                    else if (self.gameObject.name == "pin_tram")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinTramLocation"));
                    }
                    else if (self.gameObject.name.Contains("pin_sly"))
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinSly"));
                    }
                    else if (self.gameObject.name == "pin_colosseum")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinColosseum"));
                    }
                    else if (self.gameObject.name == "pin_hunter")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopHunter"));
                    }
                    else if (self.gameObject.name == "pin_banker")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopBanker"));
                    }
                    else if (self.gameObject.name == "pin_leg_eater")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinLegEater"));
                    }
                    else if (self.gameObject.name == "pin_spa")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinSpa"));
                    }
                    else if (self.gameObject.name == "pin_jiji")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopJiji"));
                    }
                    else if (self.gameObject.name == "pin_shop_mapper")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopMapper"));
                    }
                    else if (self.gameObject.name == "pin_dream moth")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinRoot"));
                    }
                    else if (self.gameObject.name == "pin_relic_dealer")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopRelicDealer"));
                    }
                    else if (self.gameObject.name == "pin_nailsmith")
                    {
                        FsmUtil.AddAction(self, "Activate", new ReplaceSprite(self, "pinShopNailsmith"));
                    }
                }
            }
            catch (Exception e)
            {
                MapMod.Instance.LogError(e);
            }

        }

        private class ReplaceSprite : FsmStateAction
        {
            private readonly GameObject _pin;
            private readonly string _spriteName;

            public ReplaceSprite(PlayMakerFSM FSM, string spriteName)
            {
                _pin = FSM.gameObject;
                _spriteName = spriteName;
            }

            public override void OnEnter()
            {
                // Overwrite existing sprite with one with bilinear filtering (looks better when scaled)
                _pin.GetComponent<SpriteRenderer>().sprite = SpriteManager.GetSprite(_spriteName);

                _pin.transform.localScale = new Vector2(MapMod.GS.PinScaleSize, MapMod.GS.PinScaleSize);

                Finish();
            }
        }

        private static void GameManager_SetGameMap(On.GameManager.orig_SetGameMap orig, GameManager self, GameObject go_gameMap)
        {
            orig(self, go_gameMap);

            GameMap gameMap = go_gameMap.GetComponent<GameMap>();

            GameObject lifebloodPin = gameMap.transform.Find("Deepnest")?.Find("Deepnest_26")?.Find("pin_blue_health")?.gameObject;
            if (lifebloodPin != null)
            {
                lifebloodPin.transform.parent = null;
                lifebloodPin.SetActive(false);
            }

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.name == "Map Markers"
                    || go.name == "pin_blue_health")
                {
                    go.transform.parent = null;
                    go.gameObject.SetActive(false);
                }
            }
        }

        private static void On_GrubPin_OnEnable(On.GrubPin.orig_OnEnable orig, GrubPin self)
        {
            orig(self);

            self.GetComponent<SpriteRenderer>().sprite = SpriteManager.GetSprite("pinGrub");
            self.transform.localScale = new Vector2(MapMod.GS.PinScaleSize, MapMod.GS.PinScaleSize);
        }
    }
}
