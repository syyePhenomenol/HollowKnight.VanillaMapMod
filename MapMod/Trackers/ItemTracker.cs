using HutongGames.PlayMaker;
using SFCore.Utils;

namespace MapMod.Trackers
{
    public static class ItemTracker
    {
        public static void Hook()
        {
            Modding.ModHooks.AfterSavegameLoadHook += ModHooks_AfterSavegameLoadHook;
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
        }

        private static void ModHooks_AfterSavegameLoadHook(SaveGameData obj)
        {
            UpdatePlayerDataItems();

            // Doesn't seem to be a way to tell apart the duplicate cases
            foreach (GeoRockData grd in obj.sceneData.geoRocks)
            {
                if (grd.hitsLeft == 0)
                {
                    MapMod.LS.ObtainedItems[grd.id + grd.sceneName] = true;
                }
            }

            foreach (PersistentBoolData pbd in obj.sceneData.persistentBoolItems)
            {
                if (pbd.id.Contains("Shiny Item") && pbd.activated)
                {
                    MapMod.LS.ObtainedItems[pbd.id + pbd.sceneName] = true;
                }
            }

            //foreach (PersistentIntData pid in obj.sceneData.persistentIntItems)
            //{
            //             MapMod.Instance.Log("- " + pid.id);
            //             MapMod.Instance.Log("- " + pid.sceneName);
            //             MapMod.Instance.Log("- - " + pid.value);
            //         }

            // TO DO: Mask/Vessel/Essence Boss/Chest
        }

        public static void UpdatePlayerDataItems()
        {
            if (PlayerData.instance.hasDash)
            {
                MapMod.LS.ObtainedItems["Mothwing_Cloak" + "Fungus1_04"] = true;
            }

            if (PlayerData.instance.hasWalljump)
            {
                MapMod.LS.ObtainedItems["Mantis_Claw" + "Fungus2_14"] = true;
            }

            if (PlayerData.instance.hasSuperDash)
            {
                MapMod.LS.ObtainedItems["Crystal_Heart" + "Mines_31"] = true;
            }

            if (PlayerData.instance.hasDoubleJump)
            {
                MapMod.LS.ObtainedItems["Monarch_Wings" + "Abyss_21"] = true;
            }

            if (PlayerData.instance.hasShadowDash)
            {
                MapMod.LS.ObtainedItems["Shade_Cloak" + "Abyss_10"] = true;
            }

            if (PlayerData.instance.hasAcidArmour)
            {
                MapMod.LS.ObtainedItems["Isma's_Tear" + "Waterways_13"] = true;
            }

            if (PlayerData.instance.hasDreamNail)
            {
                MapMod.LS.ObtainedItems["Dream_Nail" + "Dream_Nailcollection"] = true;
            }

            if (PlayerData.instance.fireballLevel != 0)
            {
                MapMod.LS.ObtainedItems["Vengeful_Spirit" + "Crossroads_ShamanTemple"] = true;
            }

            if (PlayerData.instance.fireballLevel == 2)
            {
                MapMod.LS.ObtainedItems["Shade_Soul" + "Ruins1_31b"] = true;
            }

            if (PlayerData.instance.quakeLevel != 0)
            {
                MapMod.LS.ObtainedItems["Desolate_Dive" + "Ruins1_24"] = true;
            }

            if (PlayerData.instance.quakeLevel == 2)
            {
                MapMod.LS.ObtainedItems["Descending_Dark" + "Mines_35"] = true;
            }

            if (PlayerData.instance.screamLevel != 0)
            {
                MapMod.LS.ObtainedItems["Howling_Wraiths" + "Room_Fungus_Shaman"] = true;
            }

            if (PlayerData.instance.screamLevel == 2)
            {
                MapMod.LS.ObtainedItems["Abyss_Shriek" + "Abyss_12"] = true;
            }

            if (PlayerData.instance.hasCyclone)
            {
                MapMod.LS.ObtainedItems["Cyclone_Slash" + "Room_nailmaster"] = true;
            }

            if (PlayerData.instance.hasDashSlash)
            {
                MapMod.LS.ObtainedItems["Dash_Slash" + "Room_nailmaster_02"] = true;
            }

            if (PlayerData.instance.hasUpwardSlash)
            {
                MapMod.LS.ObtainedItems["Great_Slash" + "Room_nailmaster_03"] = true;
            }
        }

        private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            //if (self.gameObject.scene.name == "Crossroads_ShamanTemple")
            //{
            //    MapMod.Instance.Log(self.FsmName);

            //    foreach (FsmState state in self.FsmStates)
            //    {
            //        MapMod.Instance.Log("- " + state.Name);
            //    }
            //}

            string goName = self.gameObject.name;

            // Most items: charms, charm notches, pale ore, rancid eggs, relics
            if (self.FsmName == "Shiny Control")
            {
                FsmUtil.AddAction(self, "Finish", new TrackItem(goName));
            }

            else if (goName.Contains("Shiny Item"))
            {
                MapMod.Instance.Log(self.FsmName);

                foreach (FsmState state in self.FsmStates)
                {
                    MapMod.Instance.Log("- " + state.Name);
                }
            }

            // Mask/Vessel
            else if (goName == "Heart Piece"
                || goName == "Vessel Fragment")
            {
                FsmUtil.AddAction(self, "Get", new TrackItem(goName));
            }

            // Geo Chests
            else if (goName == "Chest")
            {
                FsmUtil.AddAction(self, "Open", new TrackItem(goName));
            }

            else if (goName == "Ghost False Knight NPC"
                || goName == "Ghost Mage Lord NPC"
                || goName == "Ghost Infected Knight NPC")
            {
                MapMod.Instance.Log(goName);
                foreach (FsmState state in self.FsmStates)
                {
                    MapMod.Instance.Log("- " + state.Name);
                }
            }

            else if (goName == "Dung Defender_Sleep"
                || goName == "Dream Enter")
            {
                foreach (FsmState state in self.FsmStates)
                {
                    MapMod.Instance.Log("- " + state.Name);
                }
            }
        }
    }
}