using HutongGames.PlayMaker;
using SFCore.Utils;
using UnityEngine;

namespace MapMod.Trackers
{
    public static class ItemTracker
    {
        public static void Hook()
		{
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
		}

		private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
			orig(self);

            //MapMod.Instance.Log(self.FsmName);

            //foreach (FsmState state in self.FsmStates)
            //{
            //    MapMod.Instance.Log("- " + state.Name);
            //}

            string goName = self.gameObject.name;
            // Most items
            if (self.FsmName == "Shiny Control")
            {
                FsmUtil.AddAction(self, "Finish", new TrackItem(goName));
            }
            // Crystal Dash
            else if (goName == "Super Dash Get")
            {
                FsmUtil.AddAction(self, "Check", new TrackItem(goName));
            }
            // Monarch Wings
            else if (self.FsmName == "DJ Control")
            {
                FsmUtil.AddAction(self, "Get2", new TrackItem(goName));
            }
            // Nail Arts
            else if (goName == "NM Mato NPC"
                || goName == "NM Sheo NPC"
                || goName == "NM Oro NPC")
            {
                FsmUtil.AddAction(self, "Yes", new TrackItem(goName));
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

            else if (goName == "Chest")
            {
                FsmUtil.AddAction(self, "Open", new TrackItem(goName));
            }
            
            else if (goName == "Ghost False Knight NPC"
                || goName == "Ghost Mage Lord NPC"
                || goName == "Ghost Infected Knight NPC")
            {
                //MapMod.Instance.Log(goName);
                //foreach (FsmState state in self.FsmStates)
                //{
                //    MapMod.Instance.Log("- " + state.Name);
                //}
            }

            else if (goName == "Dung Defender_Sleep"
                || goName == "Dream Enter")
            {
                //foreach (FsmState state in self.FsmStates)
                //{
                //    MapMod.Instance.Log("- " + state.Name);
                //}
            }
            else if (goName.Contains("Shop Menu"))
            {
                foreach (GameObject goShopItem in self.gameObject.GetComponent<ShopMenuStock>().stock)
                {
                    ShopItemStats shopItem = goShopItem.GetComponent<ShopItemStats>();

                    MapMod.Instance.Log(shopItem.playerDataBoolName);
                    MapMod.Instance.Log(shopItem.nameConvo);
                    MapMod.Instance.Log(shopItem.descConvo);
                    MapMod.Instance.Log(shopItem.requiredPlayerDataBool);
                    MapMod.Instance.Log(shopItem.removalPlayerDataBool);
                    MapMod.Instance.Log(shopItem.dungDiscount);
                    MapMod.Instance.Log(shopItem.notchCostBool);
                    MapMod.Instance.Log(shopItem.cost);
                    MapMod.Instance.Log(shopItem.priceConvo);
                    MapMod.Instance.Log(shopItem.specialType);
                    MapMod.Instance.Log(shopItem.charmsRequired);
                    MapMod.Instance.Log(shopItem.relic);
                    MapMod.Instance.Log(shopItem.relicNumber);
                    MapMod.Instance.Log(shopItem.relicPDInt);
                }
                //foreach (FsmState state in self.FsmStates)
                //{
                //    MapMod.Instance.Log("- " + state.Name);
                //}
            }
        }
    }
}
