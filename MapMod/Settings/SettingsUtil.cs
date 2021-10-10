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

		public static bool IsMapModSetting(string boolName)
		{
            return boolName switch
            {
                "HasSkillPin"
				or "HasCharmPin"
				or "HasKeyPin"
				or "HasMaskPin"
				or "HasVesselPin"
				or "HasNotchPin"
				or "HasOrePin"
				or "HasEggPin"
				or "HasRelicPin"
				or "HasEssenceBossPin"
				or "HasCocoonPin"
				or "HasGeoPin"
				or "HasRockPin"
				or "HasTotemPin"
				or "HasLorePin" => true,
                _ => false,
            };
        }

	}
}
