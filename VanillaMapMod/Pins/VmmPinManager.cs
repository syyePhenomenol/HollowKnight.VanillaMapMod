using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.Defs;
using MapChanger.MonoBehaviours;
using UnityEngine;

namespace VanillaMapMod
{
    internal static class VmmPinManager
    {
        private const float OFFSETZ_BASE = -1.4f;
        private const float OFFSETZ_RANGE = 0.4f;

        internal static Dictionary<string, VmmPin> Pins { get; private set; } = new();

        internal static Dictionary<PoolGroup, VmmPinGroup> PinGroups { get; private set; } = new();

        internal static void MakePins(GameObject goMap)
        {
            Pins = new();
            PinGroups = new();

            foreach (PoolGroup poolGroup in Enum.GetValues(typeof(PoolGroup)).Cast<PoolGroup>().Where(poolGroup => poolGroup is not PoolGroup.Other))
            {
                VmmPinGroup pinGroup = Utils.MakeMonoBehaviour<VmmPinGroup>(goMap, poolGroup.FriendlyName());
                pinGroup.Initialize(poolGroup);

                PinGroups[poolGroup] = pinGroup;
            }

            foreach (MapLocationDef mld in Finder.GetAllVanillaLocations().Values)
            {
                VmmPin pin = Utils.MakeMonoBehaviour<VmmPin>(goMap, mld.Name);
                pin.Initialize(mld, PinGroups[SubcategoryFinder.GetLocationPoolGroup(mld.Name)]);

                Pins[mld.Name] = pin;
            }

            // Stagger the Z offset of each pin
            IEnumerable<MapObject> PinsSorted = Pins.Values.OrderBy(mapObj => mapObj.transform.position.x).ThenBy(mapObj => mapObj.transform.position.y);

            for (int i = 0; i < PinsSorted.Count(); i++)
            {
                Transform transform = PinsSorted.ElementAt(i).transform;
                transform.localPosition = new(transform.localPosition.x, transform.localPosition.y, OFFSETZ_BASE + (float)i / Pins.Count() * OFFSETZ_RANGE);
            }
        }

        internal static void UpdatePinSize()
        {
            foreach (VmmPin pin in Pins.Values)
            {
                pin.UpdatePinSize();
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
}
