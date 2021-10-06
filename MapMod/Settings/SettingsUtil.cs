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
				"Kingdom's_Edge" => PlayerData.instance.mapOutskirts,
				"Queens_Gardens" => PlayerData.instance.mapRoyalGardens,
				"Resting_Grounds" => PlayerData.instance.mapRestingGrounds,
				"Royal_Waterways" => PlayerData.instance.mapWaterways,
				_ => false,
			};
		}

		public static bool GetMapModSetting(string pool)
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
				"Rock" => MapMod.LS.HasRockPin,
				"Totem" => MapMod.LS.HasTotemPin,
				"Lore" => MapMod.LS.HasLorePin,
				_ => true,
			};
		}

		public static void SetMapModSetting(string boolName, bool value)
		{
			switch (boolName)
            {
				case "HasSkillPin":
					MapMod.LS.HasSkillPin = value;
					break;
				case "HasCharmPin":
					MapMod.LS.HasCharmPin = value;
					break;
				case "HasKeyPin":
					MapMod.LS.HasKeyPin = value;
					break;
				case "HasMaskPin":
					MapMod.LS.HasMaskPin = value;
					break;
				case "HasVesselPin":
					MapMod.LS.HasVesselPin = value;
					break;
				case "HasNotchPin":
					MapMod.LS.HasNotchPin = value;
					break;
				case "HasOrePin":
					MapMod.LS.HasOrePin = value;
					break;
				case "HasEggPin":
					MapMod.LS.HasEggPin = value;
					break;
				case "HasRelicPin":
					MapMod.LS.HasRelicPin = value;
					break;
				case "HasEssenceBossPin":
					MapMod.LS.HasEssenceBossPin = value;
					break;
				case "HasCocoonPin":
					MapMod.LS.HasCocoonPin = value;
					break;
				case "HasRockPin":
					MapMod.LS.HasRockPin = value;
					break;
				case "HasTotemPin":
					MapMod.LS.HasTotemPin = value;
					break;
				case "HasLorePin":
					MapMod.LS.HasLorePin = value;
					break;
				default:
					break;
			}
		}
	}
}
