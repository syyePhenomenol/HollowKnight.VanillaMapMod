using System;
using System.Collections.Generic;
using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.Defs;
using MapChanger.MonoBehaviours;
using UnityEngine;
using VanillaMapMod.Settings;

namespace VanillaMapMod
{
    internal class VmmPin : BorderedBackgroundPin
    {
        private const float TINY_SCALE = 0.47f;
        private const float SMALL_SCALE = 0.56f;
        private const float MEDIUM_SCALE = 0.67f;
        private const float LARGE_SCALE = 0.8f;
        private const float HUGE_SCALE = 0.96f;
        private protected const float NO_BORDER_MULTIPLIER = 1.3f;

        private static readonly Dictionary<PinSize, float> pinSizes = new()
        {
            { PinSize.Tiny, TINY_SCALE },
            { PinSize.Small, SMALL_SCALE },
            { PinSize.Medium, MEDIUM_SCALE },
            { PinSize.Large, LARGE_SCALE },
            { PinSize.Huge, HUGE_SCALE }
        };

        internal MapRoomPosition Mlp { get; private set; }

        internal void Initialize(MapLocationDef mld, VmmPinGroup parent)
        {
            base.Initialize();

            ActiveModifiers.AddRange
            (
                new Func<bool>[]
                {
                    CorrectMapOpen,
                    ActiveByCurrentMode,
                    LocationNotCleared
                }
            );

            parent.AddChild(this);
            Mlp = new MapRoomPosition(mld.MapLocations);
            MapPosition = Mlp;
            Sprite = GetSpriteFromPoolGroup(parent.PoolGroup);
        }

        public override void OnMainUpdate(bool active)
        {
            if (!active) return;

            UpdatePinSize();
            UpdatePinShape();
        }

        internal void UpdatePinSize()
        {
            Size = pinSizes[VanillaMapMod.GS.PinSize];

            if (VanillaMapMod.GS.PinShape is PinShape.NoBorder)
            {
                Size *= NO_BORDER_MULTIPLIER;
            }
        }

        internal void UpdatePinShape()
        {
            if (VanillaMapMod.GS.PinShape is PinShape.NoBorder)
            {
                BackgroundSprite = null;
                BorderSprite = null;
                return;
            }

            BackgroundSprite = SpriteManager.Instance.GetSprite($"Pins.Background{VanillaMapMod.GS.PinShape}");
            BorderSprite = SpriteManager.Instance.GetSprite($"Pins.Border{VanillaMapMod.GS.PinShape}");
        }

        private bool CorrectMapOpen()
        {
            return States.WorldMapOpen || (States.QuickMapOpen && States.CurrentMapZone == Mlp.MapZone);
        }

        private bool ActiveByCurrentMode()
        {
            return (MapChanger.Settings.CurrentMode() is NormalMode && Utils.HasMapSetting(Mlp.MapZone))
                    || MapChanger.Settings.CurrentMode() is FullMapMode;
        }

        private bool LocationNotCleared()
        {
            return !Tracker.HasClearedLocation(name);
        }

        private static Sprite GetSpriteFromPoolGroup(PoolGroup poolGroup)
        {
            string spriteName = poolGroup switch
            {
                PoolGroup.Dreamers => "Dreamer",
                PoolGroup.Skills => "Skill",
                PoolGroup.Charms => "Charm",
                PoolGroup.Keys => "Key",
                PoolGroup.MaskShards => "Mask",
                PoolGroup.VesselFragments => "Vessel",
                PoolGroup.CharmNotches => "Notch",
                PoolGroup.PaleOre => "Ore",
                PoolGroup.GeoChests => "Geo",
                PoolGroup.RancidEggs => "Egg",
                PoolGroup.Relics => "Relic",
                PoolGroup.WhisperingRoots => "Root",
                PoolGroup.BossEssence => "EssenceBoss",
                PoolGroup.Grubs => "Grub",
                PoolGroup.Mimics => "Grub",
                PoolGroup.Maps => "Map",
                PoolGroup.Stags => "Stag",
                PoolGroup.LifebloodCocoons => "Cocoon",
                PoolGroup.GrimmkinFlames => "Flame",
                PoolGroup.JournalEntries => "Journal",
                PoolGroup.GeoRocks => "Rock",
                PoolGroup.BossGeo => "Geo",
                PoolGroup.SoulTotems => "Totem",
                PoolGroup.LoreTablets => "Lore",
                PoolGroup.Shops => "Shop",
                _ => "Unknown",
            };

            return SpriteManager.Instance.GetSprite($"Pins.{spriteName}");
        }
    }
}
