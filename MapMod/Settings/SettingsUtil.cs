using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapMod.Settings
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

		public static bool GetMapModSettingFromPool(string pool)
		{
			return pool switch
			{
				"Skill" => MapMod.LS.HasSkillPin,
				"Charm" => MapMod.LS.HasCharmPin,
				"Key" => MapMod.LS.HasKeyPin,
				"Mask" => MapMod.LS.HasMaskPin,
				"Vessel" => MapMod.LS.HasVesselPin,
				"Notch" => MapMod.LS.HasNotchPin,
				"Ore" => MapMod.LS.HasOrePin,
				"Egg" => MapMod.LS.HasEggPin,
				"Relic" => MapMod.LS.HasRelicPin,
				"EssenceBoss" => MapMod.LS.HasEssenceBossPin,
				"Cocoon" => MapMod.LS.HasCocoonPin,
				"Geo" => MapMod.LS.HasGeoPin,
				"Rock" => MapMod.LS.HasRockPin,
				"Totem" => MapMod.LS.HasTotemPin,
				"Lore" => MapMod.LS.HasLorePin,
				_ => true,
			};
		}

		public static bool GetMapModSettingFromBoolName(string pool)
		{
			return pool switch
			{
				"HasSkillPin" => MapMod.LS.HasSkillPin,
				"HasCharmPin" => MapMod.LS.HasCharmPin,
				"HasKeyPin" => MapMod.LS.HasKeyPin,
				"HasMaskPin" => MapMod.LS.HasMaskPin,
				"HasVesselPin" => MapMod.LS.HasVesselPin,
				"HasNotchPin" => MapMod.LS.HasNotchPin,
				"HasOrePin" => MapMod.LS.HasOrePin,
				"HasEggPin" => MapMod.LS.HasEggPin,
				"HasRelicPin" => MapMod.LS.HasRelicPin,
				"HasEssenceBossPin" => MapMod.LS.HasEssenceBossPin,
				"HasPinCocoon" => MapMod.LS.HasCocoonPin,
				"HasGeoPin" => MapMod.LS.HasGeoPin,
				"HasRockPin" => MapMod.LS.HasRockPin,
				"HasTotemPin" => MapMod.LS.HasTotemPin,
				"HasLorePin" => MapMod.LS.HasLorePin,
				_ => true,
			};
		}

		public static bool IsMapModSetting(string boolName)
		{
			switch (boolName) {
				case "HasSkillPin":
				case "HasCharmPin":
				case "HasKeyPin":
				case "HasMaskPin":
				case "HasVesselPin":
				case "HasNotchPin":
				case "HasOrePin":
				case "HasEggPin":
				case "HasRelicPin":
				case "HasEssenceBossPin":
				case "HasCocoonPin":
				case "HasGeoPin":
				case "HasRockPin":
				case "HasTotemPin":
				case "HasLorePin":
					return true;
				default:
					return false;
			}
		}

		public static bool SetMapModSetting(string boolName, bool value)
		{
			switch (boolName)
            {
				case "HasSkillPin":
					MapMod.LS.HasSkillPin = value;
					return true;
				case "HasCharmPin":
					MapMod.LS.HasCharmPin = value;
					return true;
				case "HasKeyPin":
					MapMod.LS.HasKeyPin = value;
					return true;
				case "HasMaskPin":
					MapMod.LS.HasMaskPin = value;
					return true;
				case "HasVesselPin":
					MapMod.LS.HasVesselPin = value;
					return true;
				case "HasNotchPin":
					MapMod.LS.HasNotchPin = value;
					return true;
				case "HasOrePin":
					MapMod.LS.HasOrePin = value;
					return true;
				case "HasEggPin":
					MapMod.LS.HasEggPin = value;
					return true;
				case "HasRelicPin":
					MapMod.LS.HasRelicPin = value;
					return true;
				case "HasEssenceBossPin":
					MapMod.LS.HasEssenceBossPin = value;
					return true;
				// Overwrite vanilla behaviour of cocoon pins
				case "hasPinCocoon":
					MapMod.LS.HasCocoonPin = value;
					PlayerData.instance.hasPinCocoon = value;
					return true;
				case "HasGeoPin":
					MapMod.LS.HasGeoPin = value;
					return true;
				case "HasRockPin":
					MapMod.LS.HasRockPin = value;
					return true;
				case "HasTotemPin":
					MapMod.LS.HasTotemPin = value;
					return true;
				case "HasLorePin":
					MapMod.LS.HasLorePin = value;
					return true;
				default:
					return false;
			}
		}
	}
}
