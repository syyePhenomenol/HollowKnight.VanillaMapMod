using HutongGames.PlayMaker;
using UnityEngine;

namespace VanillaMapMod.Trackers
{
    public class TrackGeoRock : FsmStateAction
    {
        private readonly GameObject _go;
        private readonly GeoRockData _grd;

        public TrackGeoRock(GameObject go)
        {
            _go = go;
            _grd = _go.GetComponent<GeoRock>().geoRockData;
        }

        public override void OnEnter()
        {
            VanillaMapMod.LS.ObtainedItems[_grd.id + _grd.sceneName] = true;

            VanillaMapMod.Instance.LogDebug("Geo Rock broken");
            VanillaMapMod.Instance.LogDebug(" ID: " + _grd.id);
            VanillaMapMod.Instance.LogDebug(" Scene: " + _grd.sceneName);

            Finish();
        }
    }

    public class TrackItem : FsmStateAction
    {
        private readonly string _oName;

        public TrackItem(string oName)
        {
            _oName = oName;
        }

        public override void OnEnter()
        {
            string scene = GameManager.instance.sceneName;

            VanillaMapMod.LS.ObtainedItems[_oName + scene] = true;

            VanillaMapMod.Instance.LogDebug("Item picked up");
            VanillaMapMod.Instance.LogDebug(" Name: " + _oName);
            VanillaMapMod.Instance.LogDebug(" Scene: " + scene);

            Finish();
        }
    }
}