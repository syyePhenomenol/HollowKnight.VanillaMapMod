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
		// TODO: Clean up this mess
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

		//public static bool GetMapModSettingFromPool(string pool)
		//{
		//	return pool switch
		//	{
		//		"Skill" => VanillaMapMod.LS.HasSkillPin,
		//		"Charm" => VanillaMapMod.LS.HasCharmPin,
		//		"Key" => VanillaMapMod.LS.HasKeyPin,
		//		"Mask" => VanillaMapMod.LS.HasMaskPin,
		//		"Vessel" => VanillaMapMod.LS.HasVesselPin,
		//		"Notch" => VanillaMapMod.LS.HasNotchPin,
		//		"Ore" => VanillaMapMod.LS.HasOrePin,
		//		"Egg" => VanillaMapMod.LS.HasEggPin,
		//		"Relic" => VanillaMapMod.LS.HasRelicPin,
		//		"EssenceBoss" => VanillaMapMod.LS.HasEssenceBossPin,
		//		"Cocoon" => VanillaMapMod.LS.HasCocoonPin,
		//		"Geo" => VanillaMapMod.LS.HasGeoPin,
		//		"Rock" => VanillaMapMod.LS.HasRockPin,
		//		"Totem" => VanillaMapMod.LS.HasTotemPin,
		//		"Lore" => VanillaMapMod.LS.HasLorePin,
		//		_ => true,
		//	};
		//}

		//public static bool GetMapModSettingFromBoolName(string pool)
		//{
		//	return pool switch
		//	{
		//		"HasSkillPin" => VanillaMapMod.LS.HasSkillPin,
		//		"HasCharmPin" => VanillaMapMod.LS.HasCharmPin,
		//		"HasKeyPin" => VanillaMapMod.LS.HasKeyPin,
		//		"HasMaskPin" => VanillaMapMod.LS.HasMaskPin,
		//		"HasVesselPin" => VanillaMapMod.LS.HasVesselPin,
		//		"HasNotchPin" => VanillaMapMod.LS.HasNotchPin,
		//		"HasOrePin" => VanillaMapMod.LS.HasOrePin,
		//		"HasEggPin" => VanillaMapMod.LS.HasEggPin,
		//		"HasRelicPin" => VanillaMapMod.LS.HasRelicPin,
		//		"HasEssenceBossPin" => VanillaMapMod.LS.HasEssenceBossPin,
		//		"HasPinCocoon" => VanillaMapMod.LS.HasCocoonPin,
		//		"HasGeoPin" => VanillaMapMod.LS.HasGeoPin,
		//		"HasRockPin" => VanillaMapMod.LS.HasRockPin,
		//		"HasTotemPin" => VanillaMapMod.LS.HasTotemPin,
		//		"HasLorePin" => VanillaMapMod.LS.HasLorePin,
		//		_ => true,
		//	};
		//}

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

		//public static void SetMapModSetting(string boolName, bool value)
		//{
		//	switch (boolName)
  //          {
		//		case "HasSkillPin":
		//			VanillaMapMod.LS.HasSkillPin = value;
		//			break;
		//		case "HasCharmPin":
		//			VanillaMapMod.LS.HasCharmPin = value;
		//			break;
		//		case "HasKeyPin":
		//			VanillaMapMod.LS.HasKeyPin = value;
		//			break;
		//		case "HasMaskPin":
		//			VanillaMapMod.LS.HasMaskPin = value;
		//			break;
		//		case "HasVesselPin":
		//			VanillaMapMod.LS.HasVesselPin = value;
		//			break;
		//		case "HasNotchPin":
		//			VanillaMapMod.LS.HasNotchPin = value;
		//			break;
		//		case "HasOrePin":
		//			VanillaMapMod.LS.HasOrePin = value;
		//			break;
		//		case "HasEggPin":
		//			VanillaMapMod.LS.HasEggPin = value;
		//			break;
		//		case "HasRelicPin":
		//			VanillaMapMod.LS.HasRelicPin = value;
		//			break;
		//		case "HasEssenceBossPin":
		//			VanillaMapMod.LS.HasEssenceBossPin = value;
		//			break;
		//		// Overwrite vanilla behaviour of cocoon pins
		//		case "hasPinCocoon":
		//			VanillaMapMod.LS.HasCocoonPin = value;
		//			PlayerData.instance.hasPinCocoon = value;
		//			break;
		//		case "HasGeoPin":
		//			VanillaMapMod.LS.HasGeoPin = value;
		//			break;
		//		case "HasRockPin":
		//			VanillaMapMod.LS.HasRockPin = value;
		//			break;
		//		case "HasTotemPin":
		//			VanillaMapMod.LS.HasTotemPin = value;
		//			break;
		//		case "HasLorePin":
		//			VanillaMapMod.LS.HasLorePin = value;
		//			break;
		//		default:
		//			break;
		//	}
		//}
	}
}
