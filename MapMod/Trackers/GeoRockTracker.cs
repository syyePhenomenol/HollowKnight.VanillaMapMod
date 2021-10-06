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

            FsmUtil.AddAction(geoRockFSM, "Destroy", new TrackGeoRock(self.gameObject));
        }
    }
}
