using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MapMod.MapData;

namespace MapMod
{
    public class PinGroup : MonoBehaviour
    {
        public Dictionary<string, GameObject> GroupDictionary = new();

        private readonly List<Pin> _pins = new();

        public bool Hidden { get; private set; } = false;

        public void MakePins(GameMap gameMap)
        {
            DestroyPins();

            foreach (PinDef pinData in Data.GetPinArray())
            {
                try
                {
                    AddPinToRoom(pinData, gameMap);
                }
                catch (Exception e)
                {
                    MapMod.Instance.LogError(e);
                }
            }
        }

        public void DestroyPins()
        {
            foreach (Pin pin in _pins)
            {
                Destroy(pin.gameObject);
            }

            _pins.Clear();
        }

        public void AddPinToRoom(PinDef pinData, GameMap gameMap)
        {
            if (_pins.Any(pin => pin.PinData.name == pinData.name))
            {
                MapMod.Instance.LogWarn($"Duplicate pin found for group: {pinData.name} - Skipped.");
                return;
            }

            // If pinScene exists we need to set a custom base offset
            string roomName = pinData.pinScene ?? pinData.sceneName;

            Sprite pinSprite = SpriteManager.GetSpriteFromPool(pinData.pool);

            GameObject pinObject = new($"pin_rando_{pinData.name}")
            {
                layer = 30
            };

            //pinObject.transform.localScale *= 1.2f;

            pinObject.transform.localScale *= MapMod.GS.PinScaleSize;

            SpriteRenderer sr = pinObject.AddComponent<SpriteRenderer>();
            sr.sprite = pinSprite;
            sr.sortingLayerName = "HUD";
            sr.size = new Vector2(1f, 1f);

            Vector3 vec = GetRoomPos(roomName, gameMap);

            vec.Scale(new Vector3(1.46f, 1.46f, 1));
            vec += new Vector3(pinData.offsetX, pinData.offsetY, pinData.offsetZ);

            pinObject.transform.localPosition = new Vector3(vec.x, vec.y, vec.z - 1f);

            Pin pinComponent = pinObject.AddComponent<Pin>();

            pinComponent.SetPinData(pinData);

            _pins.Add(pinComponent);

            AssignPinGroup(pinObject, pinData);
        }

        private Vector3 GetRoomPos(string roomName, GameMap gameMap)
        {
            foreach (Transform areaObj in gameMap.transform)
            {
                foreach (Transform roomObj in areaObj.transform)
                {
                    if (roomObj.gameObject.name == roomName)
                    {
                        Vector3 roomVec = roomObj.transform.localPosition;
                        roomVec.Scale(areaObj.transform.localScale);
                        return areaObj.transform.localPosition + roomVec;
                    }
                }
            }

            MapMod.Instance.LogWarn($"{roomName} is not a valid room name!");
            return new Vector3(0, 0, 0);
        }

        // Set each Pin to the correct Parent Group
        private void AssignPinGroup(GameObject newPin, PinDef pinData)
        {
            if (!GroupDictionary.ContainsKey(pinData.pool))
            {
                GroupDictionary[pinData.pool] = new GameObject("PinGroup " + pinData.pool);
                GroupDictionary[pinData.pool].transform.SetParent(transform);

                // Set all true for now
                GroupDictionary[pinData.pool].SetActive(true);
            }

            newPin.transform.SetParent(GroupDictionary[pinData.pool].transform);
        }

        public void UpdatePins(string mapName)
        {
            foreach (Pin pin in _pins)
            {
                pin.UpdatePin(mapName);
            }
        }

        protected void Start()
        {
            Hide();
        }

        public void Show(bool force = false)
        {
            if (force) Hidden = false;

            if (!Hidden)
            {
                gameObject.SetActive(true);
            }
        }

        public void Hide(bool force = false)
        {
            if (force) Hidden = true;
            gameObject.SetActive(false);
        }



        //public void RefreshPins(GameMap gameMap)
        //{
        //    foreach (KeyValuePair<string, PinDef> entry in ResourceLoader.PinDataDictionary)
        //    {
        //        if (GameObject.Find($"pin_rando_{entry.Key}"))
        //        {
        //            Pin pin = GameObject.Find($"pin_rando_{entry.Key}").GetComponent<Pin>();
        //            try
        //            {
        //                string roomName = pin.PinData.PinScene ?? pin.PinData.SceneName;
        //                Vector3 vec = _GetRoomPos(roomName, gameMap);
        //                vec.Scale(new Vector3(1.46f, 1.46f, 1));
        //                vec += entry.Value.Offset;

        //                pin.transform.localPosition = new Vector3(vec.x, vec.y, vec.z - 5f);
        //            }
        //            catch (Exception e)
        //            {
        //                MapModS.Instance.LogError($"Error: RefeshPins {e}");
        //            }
        //        }
        //    }
        //}
    }
}