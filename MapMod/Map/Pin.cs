using System;
using UnityEngine;
using MapMod.Data;
using MapMod.Settings;

namespace MapMod.Map {
    internal class Pin : MonoBehaviour
    {
        //private Vector3 _origScale;
        public PinDef PinData { get; private set; } = null;
        //private SpriteRenderer SR => gameObject.GetComponent<SpriteRenderer>();

        public void SetPinData(PinDef pd)
        {
            PinData = pd;
        }

        //public void SetPinSprite()
        //{
        //    SR.sprite = GetSprite(PinData.pool);
        //}

        public void UpdatePin(string mapAreaName)
        {
            try
            {
                if (PinData == null)
                {
                    throw new Exception("Cannot enable pin with null pindata. Ensure game object is disabled before adding as component, then call SetPinData(<pd>) before enabling.");
                }

                ShowIfCorrectMap(mapAreaName);
                HideIfNotBought();
                HideIfFound();

            }
            catch (Exception e)
            {
                MapMod.Instance.LogError(message: $"Failed to update pin! ID: {PinData.name}\n{e}");
            }
        }

        // This method hides or shows the pin depending on which map was opened
        private void ShowIfCorrectMap(string mapAreaName)
        {
            if (PlayerData.instance.hasQuill)
            {
                if (mapAreaName == PinData.mapArea || mapAreaName == "WorldMap")
                {
                    // Have these pins always on
                    if (PinData.pool == "Skill")
                    {
                        gameObject.SetActive(true);
                        return;
                    }

                    if (PinData.pool == "Charm"
                        || PinData.pool == "Key"
                        || PinData.pool == "Notch"
                        || PinData.pool == "Mask"
                        || PinData.pool == "Vessel"
                        || PinData.pool == "Ore"
                        || PinData.pool == "EssenceBoss")
                    {
                        //Show these pins if the corresponding map item has been picked up
                        if (SettingsUtil.GetPlayerDataMapSetting(PinData.mapArea))
                        {
                            gameObject.SetActive(true);
                            return;
                        }
                    }

                    // For the rest, show pin if the corresponding area has been mapped
                    if (PinData.pinScene != null)
                    {
                        if (PlayerData.instance.scenesMapped.Contains(PinData.pinScene))
                        {
                            gameObject.SetActive(true);
                            return;
                        }
                    }
                    else
                    {
                        if (PlayerData.instance.scenesMapped.Contains(PinData.sceneName))
                        {
                            gameObject.SetActive(true);
                            return;
                        }
                    }
                }
            }

            gameObject.SetActive(false);
        }

        private void HideIfNotBought()
        {
            if (!SettingsUtil.GetMapModSettingFromPool(PinData.pool))
            {
                gameObject.SetActive(false);
            }
        }

        private void HideIfFound()
        {
            if (PinData.objectName == null)
            {
                return;
            }

            foreach (string oName in PinData.objectName)
            {
                if (!MapMod.LS.ObtainedItems.ContainsKey(oName + PinData.sceneName))
                {
                    return;
                }
            }

            gameObject.SetActive(false);
        }
    }
}