using GlobalEnums;
using VanillaMapMod.Data;

namespace VanillaMapMod.Settings
{
    public static class SettingsUtil
    {
		//public static bool GetPlayerDataMapSetting(string mapZone)
		//{
		//	return mapZone switch
		//	{
		//		"Ancient_Basin" => PlayerData.instance.mapAbyss,
		//		"City_of_Tears" => PlayerData.instance.mapCity,
		//		"Howling_Cliffs" => PlayerData.instance.mapCliffs,
		//		"Forgotten_Crossroads" => PlayerData.instance.mapCrossroads,
		//		"Crystal_Peak" => PlayerData.instance.mapMines,
		//		"Deepnest" => PlayerData.instance.mapDeepnest,
		//		"Dirtmouth" => PlayerData.instance.mapDirtmouth,
		//		"Fog_Canyon" => PlayerData.instance.mapFogCanyon,
		//		"Fungal_Wastes" => PlayerData.instance.mapFungalWastes,
		//		"Greenpath" => PlayerData.instance.mapGreenpath,
		//		"Kingdoms_Edge" => PlayerData.instance.mapOutskirts,
		//		"Queens_Gardens" => PlayerData.instance.mapRoyalGardens,
		//		"Resting_Grounds" => PlayerData.instance.mapRestingGrounds,
		//		"Royal_Waterways" => PlayerData.instance.mapWaterways,
		//		_ => false,
		//	};
  //      }

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
                _ => false,
            };
        }

        public static void SyncPlayerDataSettings()
        {
            // The Has settings should be equivalent to the ORIGINAL PlayerData settings
            VanillaMapMod.LS.GroupSettings[Pool.Bench].Has = PlayerData.instance.GetBool("hasPinBench");
            VanillaMapMod.LS.GroupSettings[Pool.Cocoon].Has = PlayerData.instance.GetBool("hasPinCocoon");
            VanillaMapMod.LS.GroupSettings[Pool.Grave].Has = PlayerData.instance.GetBool("hasPinGhost");
            VanillaMapMod.LS.GroupSettings[Pool.Grub].Has = PlayerData.instance.GetBool("hasPinGrub");
            VanillaMapMod.LS.GroupSettings[Pool.Root].Has = PlayerData.instance.GetBool("hasPinDreamPlant");
            VanillaMapMod.LS.GroupSettings[Pool.Spa].Has = PlayerData.instance.GetBool("hasPinSpa");
            VanillaMapMod.LS.GroupSettings[Pool.Stag].Has = PlayerData.instance.GetBool("hasPinStag");
            VanillaMapMod.LS.GroupSettings[Pool.Tram].Has = PlayerData.instance.GetBool("hasPinTram");
            VanillaMapMod.LS.GroupSettings[Pool.Vendor].Has = PlayerData.instance.GetBool("hasPinShop");
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
                or "Waterways" => true,
                _ => false,
            };
            ;
        }
    }
}
