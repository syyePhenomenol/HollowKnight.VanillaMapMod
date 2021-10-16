using Vasi;
using HutongGames.PlayMaker;
using Modding;

namespace VanillaMapMod.Trackers
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

            // Rename duplicate GameObjects (GeoRockData gets created later and its id is also the GameObject name)
            if (self.gameObject.scene.name == "Crossroads_ShamanTemple" && self.gameObject.name == "Geo Rock 2")
            {
                if (self.transform.parent != null)
                {
                    self.gameObject.name = "_Items/Geo Rock 2";
                }
            }

            if (self.gameObject.scene.name == "Abyss_06_Core" && self.gameObject.name == "Geo Rock Abyss")
            {
                if (self.transform.parent != null)
                {
                    self.gameObject.name = "_Props/Geo Rock Abyss";
                }
            }

            FsmUtil.AddAction(FsmUtil.GetState(geoRockFSM, "Destroy"), new TrackGeoRock(self.gameObject));
        }
    }
}