using System;
using UnityEngine;
using HutongGames.PlayMaker;
using SFCore.Utils;

namespace MapMod.Trackers
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
            if (_grd.sceneName == "Crossroads_ShamanTemple" && _grd.id == "Geo Rock 2")
            {
                // Resolve duplicate ids by position
                if (_go.transform.localPosition.y > 50.0f)
                {
                    MapMod.LS.ObtainedItems[_grd.id + _grd.sceneName] = true;
                }
                else
                {
                    MapMod.LS.ObtainedItems["Geo Rock 1 (1)" + _grd.sceneName] = true;
                }
            }
            else if (_grd.sceneName == "Abyss_06_Core" && _grd.id == "Geo Rock Abyss")
            {
                // Resolve duplicate ids by position
                if (_go.transform.localPosition.y > 180.0f)
                {
                    MapMod.LS.ObtainedItems[_grd.id + _grd.sceneName] = true;
                }
                else
                {
                    MapMod.LS.ObtainedItems["Geo Rock Abyss (2)" + _grd.sceneName] = true;
                }
            }
            else
            {
                MapMod.LS.ObtainedItems[_grd.id + _grd.sceneName] = true;
            }

            //MapMod.Instance.Log(_go.transform.localPosition);
            MapMod.Instance.Log(_grd.id);
            MapMod.Instance.Log(_grd.sceneName);

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

            MapMod.LS.ObtainedItems[_oName + scene] = true;

            MapMod.Instance.Log(_oName);
            MapMod.Instance.Log(scene);

            Finish();
        }
    }
}
