using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VanillaMapMod.Data;

namespace VanillaMapMod.Map
{
    public class PinsCustom : MonoBehaviour
    {
        private readonly Dictionary<string, GameObject> _Groups = new();

        private readonly List<Pin> _pins = new();

        public void MakePins(GameMap gameMap)
        {
            DestroyPins();

            foreach (PinDef pinData in DataLoader.GetPinArray())
            {
                try
                {
                    MakePin(pinData, gameMap);
                }
                catch (Exception e)
                {
                    VanillaMapMod.Instance.LogError(e);
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

        public void RefreshGroups()
        {
            foreach (string group in _Groups.Keys)
            {
                RefreshGroup(group);
            }
        }

        public void RefreshGroup(string group)
        {
            _Groups[group].SetActive(VanillaMapMod.LS.GetOnFromGroup(group));
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
                VanillaMapMod.Instance.LogWarn($"Duplicate pin found for group: {pinData.name} - Skipped.");
                return;
            }

            // Create new pin GameObject
            GameObject goPin = new($"pin_mapmod_{pinData.name}")
            {
                layer = 30
            };

            // Attach pin data to the GameObject
            Pin pin = goPin.AddComponent<Pin>();
            pin.SetPinData(pinData);
            _pins.Add(pin);

            // Attach sprite renderer to the GameObject
            SpriteRenderer sr = goPin.AddComponent<SpriteRenderer>();
            Sprite pinSprite;

            // Give the Grimm Troupe Lantern its unique sprite
            if (pinData.name == "Grimm Troupe Lantern")
            {
                pinSprite = SpriteManager.GetSpriteFromPool("Flame");
            }
            else
            {
                pinSprite = SpriteManager.GetSpriteFromPool(pinData.pool);
            }
            
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
            goPin.transform.localScale = 1.46f * new Vector2(VanillaMapMod.GS.PinScaleSize, VanillaMapMod.GS.PinScaleSize);
        }

        private void AssignGroup(GameObject newPin, PinDef pinData)
        {
            if (!_Groups.ContainsKey(pinData.pool))
            {
                _Groups[pinData.pool] = new GameObject("PinGroup " + pinData.pool);
                _Groups[pinData.pool].transform.SetParent(transform);

                // Set all true for now (doesn't really affect any behaviour)
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

            VanillaMapMod.Instance.LogWarn($"{roomName} is not a valid room name!");
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
    }
}