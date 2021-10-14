using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        //public static bool IsVanillaPinGroup(string group)
        //      {
        //	return group switch
        //	{
        //		"Bench"
        //		or "Vendor"
        //		or "Stag"
        //		or "Spa"
        //		or "Root"
        //		or "Grave"
        //		or "Tram"
        //		or "Grub" => true,
        //		_ => false,
        //	};
        //      }

        //public static bool IsCustomPinGroup(string group)
        //{
        //	return group switch
        //	{
        //		"Cocoon"
        //		or "Skill"
        //		or "Charm"
        //		or "Key"
        //		or "Mask"
        //		or "Vessel"
        //		or "Notch"
        //		or "Ore"
        //		or "Egg"
        //		or "Relic"
        //		or "EssenceBoss"
        //		or "Geo"
        //		or "Rock"
        //		or "Totem"
        //		or "Lore" => true,
        //		_ => false,
        //	};
        //}
    }
}
