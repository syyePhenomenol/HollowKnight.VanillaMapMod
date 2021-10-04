using System;
using HutongGames.PlayMaker;
using SFCore.Utils;

namespace MapMod.Trackers
{
    public static class GeoRockTracker
    {
		public static void Hook()
		{
			On.GeoRock.OnEnable += GeoRock_OnEnable;
		}

		private static void GeoRock_OnEnable(On.GeoRock.orig_OnEnable orig, GeoRock self)
        {
			orig(self);

            PlayMakerFSM geoRockFSM = self.gameObject.LocateMyFSM("Geo Rock");

            //foreach (FsmState state in geoRockFSM.FsmStates)
            //{
            //    MapMod.Instance.Log(state.Name);
            //}

			FsmUtil.AddAction(geoRockFSM, "Destroy", new TrackGeoRock(self.gameObject.name));
            FsmUtil.AddAction(geoRockFSM, "Broken", new TrackGeoRock(self.gameObject.name));
        }

        private class TrackGeoRock : FsmStateAction
        {
            private readonly string _oName;
            public TrackGeoRock(string oName)
            {
                _oName = oName;
            }
            public override void OnEnter()
            {
                string scene = GameManager.instance.sceneName;
                if (scene == "Crossroads_ShamanTemple" && _oName == "Geo Rock 2")
                {
                    // Check if the hero is closer to lower rock or higher rock
                    // Mound Dupe is Geo Rock 1 (1), Mound Tree is Geo Rock 2
                    if (Math.Abs(HeroController.instance.transform.position.y - 46.5f)
                        < Math.Abs(HeroController.instance.transform.position.y - 60.8f))
                    {
                        MapMod.LS.ObtainedItems["Geo Rock 1 (1)" + scene] = true;
                    }
                    else
                    {
                        MapMod.LS.ObtainedItems["Geo Rock 2" + scene] = true;
                    }
                }
                else
                {
                    MapMod.LS.ObtainedItems[_oName + scene] = true;
                }
                
                Finish();
            }
        }
    }
}
