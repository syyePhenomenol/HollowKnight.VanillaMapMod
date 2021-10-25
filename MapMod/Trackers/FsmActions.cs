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

            VanillaMapMod.Instance.Log("Geo Rock broken");
            VanillaMapMod.Instance.Log(" ID: " + _grd.id);
            VanillaMapMod.Instance.Log(" Scene: " + _grd.sceneName);

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

            VanillaMapMod.Instance.Log("Item picked up");
            VanillaMapMod.Instance.Log(" Name: " + _oName);
            VanillaMapMod.Instance.Log(" Scene: " + scene);

            Finish();
        }
    }
}