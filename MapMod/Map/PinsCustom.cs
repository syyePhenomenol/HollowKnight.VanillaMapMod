using System;
using System.Collections.Generic;
using System.Linq;
using MapMod.Data;
using UnityEngine;

namespace MapMod.Map
{
    public class PinsCustom : MonoBehaviour
    {
        private readonly Dictionary<string, GameObject> _Groups = new();

        private readonly List<Pin> _pins = new();

        public void MakePins(GameMap gameMap)
        {
            DestroyPins();

            foreach (PinDef pinData in Data.DataLoader.GetPinArray())
            {
                try
                {
                    MakePin(pinData, gameMap);
                }
                catch (Exception e)
                {
                    MapMod.Instance.LogError(e);
                }
            }
        }

        public void UpdatePins(string mapName)
        {
            foreach (Pin pin in _pins)
            {
                pin.UpdatePin(mapName);
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

        private void MakePin(PinDef pinData, GameMap gameMap)
        {
            if (_pins.Any(pin => pin.PinData.name == pinData.name))
            {
                MapMod.Instance.LogWarn($"Duplicate pin found for group: {pinData.name} - Skipped.");
                return;
            }

            // Create new pin GameObject
            GameObject goPin = new($"pin_rando_{pinData.name}")
            {
                layer = 30
            };

            // Attach pin data to the GameObject
            Pin pin = goPin.AddComponent<Pin>();
            pin.SetPinData(pinData);
            _pins.Add(pin);

            // Attach sprite renderer to the GameObject
            SpriteRenderer sr = goPin.AddComponent<SpriteRenderer>();
            Sprite pinSprite = SpriteManager.GetSpriteFromPool(pinData.pool);
            sr.sprite = pinSprite;
            sr.sortingLayerName = "HUD";
            sr.size = new Vector2(1f, 1f);

            // Set pin transform (by pool)
            AssignGroup(goPin, pinData);

            // Position the pin - if pinScene exists we need to set a custom base offset
            string roomName = pinData.pinScene ?? pinData.sceneName;
            Vector3 vec = GetRoomPos(roomName, gameMap);
            vec.Scale(new Vector3(1.46f, 1.46f, 1));
            vec += new Vector3(pinData.offsetX, pinData.offsetY, pinData.offsetZ);
            goPin.transform.localPosition = new Vector3(vec.x, vec.y, vec.z - 0.01f);

            // Scale the pin
            goPin.transform.localScale = 1.46f * new Vector2(MapMod.GS.PinScaleSize, MapMod.GS.PinScaleSize);
        }

        private void AssignGroup(GameObject newPin, PinDef pinData)
        {
            if (!_Groups.ContainsKey(pinData.pool))
            {
                _Groups[pinData.pool] = new GameObject("PinGroup " + pinData.pool);
                _Groups[pinData.pool].transform.SetParent(transform);

                // Set all true for now
                _Groups[pinData.pool].SetActive(true);
            }

            newPin.transform.SetParent(_Groups[pinData.pool].transform);
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

        protected void Start()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
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