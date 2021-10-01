using System;
using UnityEngine;
using MapMod.MapData;
using static MapMod.SpriteManager;
using Logger = Modding.Logger;

internal class Pin : MonoBehaviour
{
    private Vector3 _origScale;
    public PinDef PinData { get; private set; } = null;
    private SpriteRenderer SR => gameObject.GetComponent<SpriteRenderer>();

    public void SetPinData(PinDef pd)
    {
        PinData = pd;

        _origScale = transform.localScale;
        transform.localScale = _origScale * 0.7f;

        //SetPinSprite();
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
            HideIfFound();

        }
        catch (Exception e)
        {
            Logger.LogError(message: $"Failed to update pin! ID: {PinData.name}\n{e}");
        }
    }

    // This method hides or shows the pin depending on which map was opened
    private void ShowIfCorrectMap(string mapAreaName)
    {
        if (mapAreaName == PinData.mapArea || mapAreaName == "WorldMap")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void HideIfFound()
    {
        //if (MapMod.MapMod.

        //{
        //    gameObject.SetActive(false);
        //}
    }

}