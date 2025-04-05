using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.MonoBehaviours;
using UnityEngine;
using VanillaMapMod.Pins;

namespace VanillaMapMod;

internal static class VmmPinManager
{
    private const float OFFSETZ_BASE = -1.4f;
    private const float OFFSETZ_RANGE = 0.4f;

    internal static Dictionary<string, VmmPin> Pins { get; private set; } = [];

    internal static Dictionary<PoolGroup, VmmPinGroup> PinGroups { get; private set; } = [];

    internal static void MakePins(GameObject goMap)
    {
        VanillaMapMod.Instance.LogDebug("Make Pins");

        Pins = [];
        PinGroups = [];

        foreach (
            var poolGroup in Enum.GetValues(typeof(PoolGroup))
                .Cast<PoolGroup>()
                .Where(poolGroup => poolGroup is not PoolGroup.Other)
        )
        {
            var pinGroup = Utils.MakeMonoBehaviour<VmmPinGroup>(goMap, poolGroup.FriendlyName());
            pinGroup.Initialize(poolGroup);

            PinGroups[poolGroup] = pinGroup;
        }

        foreach (var mld in Finder.GetAllVanillaLocations().Values)
        {
            var pin = Utils.MakeMonoBehaviour<VmmPin>(goMap, mld.Name);
            pin.Initialize(mld, PinGroups[SubcategoryFinder.GetLocationPoolGroup(mld.Name)]);

            Pins[mld.Name] = pin;
        }

        // Stagger the Z offset of each pin
        IEnumerable<MapObject> pinsSorted = Pins
            .Values.OrderBy(mapObj => mapObj.transform.position.x)
            .ThenBy(mapObj => mapObj.transform.position.y);

        for (var i = 0; i < pinsSorted.Count(); i++)
        {
            var transform = pinsSorted.ElementAt(i).transform;
            transform.localPosition = new(
                transform.localPosition.x,
                transform.localPosition.y,
                OFFSETZ_BASE + ((float)i / Pins.Count() * OFFSETZ_RANGE)
            );
        }
    }

    internal static string GetPoolGroupCounter(PoolGroup poolGroup)
    {
        string text;

        IReadOnlyCollection<MapObject> pins = PinGroups[poolGroup].Children;

        if (IsPersistent(poolGroup))
        {
            text = "";
        }
        else
        {
            text = pins.Where(pin => Tracker.HasClearedLocation(pin.name)).Count().ToString() + " / ";
        }

        return text + pins.Count().ToString();
    }

    private static bool IsPersistent(PoolGroup poolGroup)
    {
        return poolGroup is PoolGroup.LifebloodCocoons or PoolGroup.SoulTotems or PoolGroup.LoreTablets;
    }
}
