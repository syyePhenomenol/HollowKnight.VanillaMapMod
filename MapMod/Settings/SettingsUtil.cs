namespace VanillaMapMod.Settings
{
    public static class SettingsUtil
    {
		public static bool GetPlayerDataMapSetting(string mapArea)
		{
			return mapArea switch
			{
				"Ancient_Basin" => PlayerData.instance.mapAbyss,
				"City_of_Tears" => PlayerData.instance.mapCity,
				"Howling_Cliffs" => PlayerData.instance.mapCliffs,
				"Forgotten_Crossroads" => PlayerData.instance.mapCrossroads,
				"Crystal_Peak" => PlayerData.instance.mapMines,
				"Deepnest" => PlayerData.instance.mapDeepnest,
				"Dirtmouth" => PlayerData.instance.mapDirtmouth,
				"Fog_Canyon" => PlayerData.instance.mapFogCanyon,
				"Fungal_Wastes" => PlayerData.instance.mapFungalWastes,
				"Greenpath" => PlayerData.instance.mapGreenpath,
				"Kingdoms_Edge" => PlayerData.instance.mapOutskirts,
				"Queens_Gardens" => PlayerData.instance.mapRoyalGardens,
				"Resting_Grounds" => PlayerData.instance.mapRestingGrounds,
				"Royal_Waterways" => PlayerData.instance.mapWaterways,
				_ => false,
			};
        }

        public static bool GetVMMMapSetting(string mapArea)
        {
            return mapArea switch
            {
                "Ancient_Basin" => PlayerData.instance.GetBool("VMM_mapAbyss"),
                "City_of_Tears" => PlayerData.instance.GetBool("VMM_mapCity"),
                "Howling_Cliffs" => PlayerData.instance.GetBool("VMM_mapCliffs"),
                "Forgotten_Crossroads" => PlayerData.instance.GetBool("VMM_mapCrossroads"),
                "Crystal_Peak" => PlayerData.instance.GetBool("VMM_mapMines"),
                "Deepnest" => PlayerData.instance.GetBool("VMM_mapDeepnest"),
                "Dirtmouth" => PlayerData.instance.GetBool("VMM_mapDirtmouth"),
                "Fog_Canyon" => PlayerData.instance.GetBool("VMM_mapFogCanyon"),
                "Fungal_Wastes" => PlayerData.instance.GetBool("VMM_mapFungalWastes"),
                "Greenpath" => PlayerData.instance.GetBool("VMM_mapGreenpath"),
                "Kingdoms_Edge" => PlayerData.instance.GetBool("VMM_mapOutskirts"),
                "Queens_Gardens" => PlayerData.instance.GetBool("VMM_mapRoyalGardens"),
                "Resting_Grounds" => PlayerData.instance.GetBool("VMM_mapRestingGrounds"),
                "Royal_Waterways" => PlayerData.instance.GetBool("VMM_mapWaterways"),
                _ => false,
            };
        }

        public static bool IsFSMMapState(string name)
        {
            switch (name)
            {
                case "Abyss":
                case "Ancient Basin":
                case "City":
                case "Cliffs":
                case "Crossroads":
                case "Deepnest":
                case "Fog Canyon":
                case "Fungal Wastes":
                case "Fungus":
                case "Greenpath":
                case "Hive":
                case "Mines":
                case "Outskirts":
                case "Resting Grounds":
                case "Royal Gardens":
                case "Waterways":
                    return true;
                default:
                    return false;
            };
        }
    }
}
