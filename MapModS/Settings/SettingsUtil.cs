using GlobalEnums;
using MapModS.Data;

namespace MapModS.Settings
{
    public static class SettingsUtil
    {
        public static bool GetVMMMapSetting(MapZone mapZone)
        {
            return mapZone switch
            {
                MapZone.ABYSS => PlayerData.instance.GetBool("VMM_mapAbyss"),
                MapZone.CITY => PlayerData.instance.GetBool("VMM_mapCity"),
                MapZone.CLIFFS => PlayerData.instance.GetBool("VMM_mapCliffs"),
                MapZone.CROSSROADS => PlayerData.instance.GetBool("VMM_mapCrossroads"),
                MapZone.MINES => PlayerData.instance.GetBool("VMM_mapMines"),
                MapZone.DEEPNEST => PlayerData.instance.GetBool("VMM_mapDeepnest"),
                MapZone.TOWN => PlayerData.instance.GetBool("VMM_mapDirtmouth"),
                MapZone.FOG_CANYON => PlayerData.instance.GetBool("VMM_mapFogCanyon"),
                MapZone.WASTES => PlayerData.instance.GetBool("VMM_mapFungalWastes"),
                MapZone.GREEN_PATH => PlayerData.instance.GetBool("VMM_mapGreenpath"),
                MapZone.OUTSKIRTS => PlayerData.instance.GetBool("VMM_mapOutskirts"),
                MapZone.ROYAL_GARDENS => PlayerData.instance.GetBool("VMM_mapRoyalGardens"),
                MapZone.RESTING_GROUNDS => PlayerData.instance.GetBool("VMM_mapRestingGrounds"),
                MapZone.WATERWAYS => PlayerData.instance.GetBool("VMM_mapWaterways"),
                MapZone.WHITE_PALACE => PlayerData.instance.GetBool("VMM_AdditionalMapsGotWpMap"),
                MapZone.GODS_GLORY => PlayerData.instance.GetBool("VMM_AdditionalMapsGotGhMap"),
                _ => false,
            };
        }

        public static void SyncPlayerDataSettings()
        {
            // The Has settings should be equivalent to the ORIGINAL PlayerData settings
            MapModS.LS.GroupSettings[Pool.Bench].Has = PlayerData.instance.GetBool("hasPinBench");
            MapModS.LS.GroupSettings[Pool.Cocoon].Has = PlayerData.instance.GetBool("hasPinCocoon");
            MapModS.LS.GroupSettings[Pool.Grave].Has = PlayerData.instance.GetBool("hasPinGhost");
            MapModS.LS.GroupSettings[Pool.Grub].Has = PlayerData.instance.GetBool("hasPinGrub");
            MapModS.LS.GroupSettings[Pool.Root].Has = PlayerData.instance.GetBool("hasPinDreamPlant");
            MapModS.LS.GroupSettings[Pool.Spa].Has = PlayerData.instance.GetBool("hasPinSpa");
            MapModS.LS.GroupSettings[Pool.Stag].Has = PlayerData.instance.GetBool("hasPinStag");
            MapModS.LS.GroupSettings[Pool.Tram].Has = PlayerData.instance.GetBool("hasPinTram");
            MapModS.LS.GroupSettings[Pool.Vendor].Has = PlayerData.instance.GetBool("hasPinShop");
        }

        public static bool IsFSMMapState(string name)
        {
            return name switch
            {
                "Abyss"
                or "Ancient Basin"
                or "City"
                or "Cliffs"
                or "Crossroads"
                or "Deepnest"
                or "Fog Canyon"
                or "Fungal Wastes"
                or "Fungus"
                or "Greenpath"
                or "Hive"
                or "Mines"
                or "Outskirts"
                or "Resting Grounds"
                or "Royal Gardens"
                or "Waterways"
                or "WHITE_PALACE"
                or "GODS_GLORY" => true,
                _ => false,
            };
            ;
        }
    }
}
