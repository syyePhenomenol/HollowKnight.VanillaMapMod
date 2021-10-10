using System;
using System.Collections.Generic;

namespace VanillaMapMod.Settings
{
    [Serializable]
    public class LocalSettings
    {
		public class GroupSettingPair
        {
			public GroupSettingPair()
            {
				Has = false;
				On = true;
            }

			public bool Has;
			public bool On;
        };

        public Dictionary<string, bool> ObtainedItems = new();

		public Dictionary<string, GroupSettingPair> GroupSettings = new()
		{
			{ "Skill", new GroupSettingPair() },
			{ "Charm", new GroupSettingPair() },
			{ "Key", new GroupSettingPair() },
			{ "Mask", new GroupSettingPair() },
			{ "Vessel", new GroupSettingPair() },
			{ "Notch", new GroupSettingPair() },
			{ "Ore", new GroupSettingPair() },
			{ "Egg", new GroupSettingPair() },
			{ "Relic", new GroupSettingPair() },
			{ "EssenceBoss", new GroupSettingPair() },
			{ "Cocoon", new GroupSettingPair() },
			{ "Geo", new GroupSettingPair() },
			{ "Rock", new GroupSettingPair() },
			{ "Totem", new GroupSettingPair() },
			{ "Lore", new GroupSettingPair() }
		};

		public bool GotFullMap = false;

		public void SetFullMap()
        {
            GotFullMap = true;

			foreach(KeyValuePair<string, GroupSettingPair> entry in GroupSettings)
            {
				entry.Value.Has = true;
            }

            //HasSkillPin = true;
            //HasCharmPin = true;
            //HasKeyPin = true;
            //HasMaskPin = true;
            //HasVesselPin = true;
            //HasNotchPin = true;
            //HasOrePin = true;
            //HasEggPin = true;
            //HasRelicPin = true;
            //HasEssenceBossPin = true;
            //HasCocoonPin = true;
            //HasGeoPin = true;
            //HasRockPin = true;
            //HasTotemPin = true;
            //HasLorePin = true;
        }

        

		public bool GetHasFromGroup(string group)
		{
			if (GroupSettings.ContainsKey(group))
            {
				return GroupSettings[group].Has;
            }

			return false;

			//return group switch
			//{
			//	"Skill" => HasSkillPin,
			//	"Charm" => HasCharmPin,
			//	"Key" => HasKeyPin,
			//	"Mask" => HasMaskPin,
			//	"Vessel" => HasVesselPin,
			//	"Notch" => HasNotchPin,
			//	"Ore" => HasOrePin,
			//	"Egg" => HasEggPin,
			//	"Relic" => HasRelicPin,
			//	"EssenceBoss" => HasEssenceBossPin,
			//	"Cocoon" => HasCocoonPin,
			//	"Geo" => HasGeoPin,
			//	"Rock" => HasRockPin,
			//	"Totem" => HasTotemPin,
			//	"Lore" => HasLorePin,
			//	_ => false,
			//};
		}

		//public bool HasSkillPin = false;
		//      public bool HasCharmPin = false;
		//      public bool HasKeyPin = false;
		//      public bool HasMaskPin = false;
		//      public bool HasVesselPin = false;
		//      public bool HasNotchPin = false;
		//      public bool HasOrePin = false;
		//      public bool HasEggPin = false;
		//      public bool HasRelicPin = false;
		//      public bool HasEssenceBossPin = false;
		//      public bool HasCocoonPin = false;
		//      public bool HasGeoPin = false;
		//      public bool HasRockPin = false;
		//      public bool HasTotemPin = false;
		//      public bool HasLorePin = false;

		public void SetHasFromGroup(string group, bool value)
		{
			if (GroupSettings.ContainsKey(group))
			{
				GroupSettings[group].Has = value;
			}
		}

		public bool GetOnFromGroup(string group)
		{
			if (GroupSettings.ContainsKey(group))
			{
				return GroupSettings[group].On;
			}

			return false;

			//return group switch
			//{
			//	"Skill" => SkillOn,
			//	"Charm" => CharmOn,
			//	"Key" => KeyOn,
			//	"Mask" => MaskOn,
			//	"Vessel" => VesselOn,
			//	"Notch" => NotchOn,
			//	"Ore" => OreOn,
			//	"Egg" => EggOn,
			//	"Relic" => RelicOn,
			//	"EssenceBoss" => EssenceBossOn,
			//	"Cocoon" => CocoonOn,
			//	"Geo" => GeoOn,
			//	"Rock" => RockOn,
			//	"Totem" => RockOn,
			//	"Lore" => LoreOn,
			//	_ => false,
			//};
		}

		public void SetOnFromGroup(string group, bool value)
		{
			if (GroupSettings.ContainsKey(group))
			{
				GroupSettings[group].On = value;
			}

			//switch (group)
			//{
			//	case "Skill":
			//		SkillOn = value;
			//		break;

			//	case "Charm":
			//		CharmOn = value;
			//		break;

			//	case "Key":
			//		KeyOn = value;
			//		break;

			//	case "Mask":
			//		MaskOn = value;
			//		break;

			//	case "Vessel":
			//		VesselOn = value;
			//		break;

			//	case "Notch":
			//		NotchOn = value;
			//		break;

			//	case "Ore":
			//		OreOn = value;
			//		break;

			//	case "Egg":
			//		EggOn = value;
			//		break;

			//	case "Relic":
			//		RelicOn = value;
			//		break;

			//	case "EssenceBoss":
			//		EssenceBossOn = value;
			//		break;

			//	case "Cocoon":
			//		CocoonOn = value;
			//		break;

			//	case "Geo":
			//		GeoOn = value;
			//		break;

			//	case "Rock":
			//		RockOn = value;
			//		break;

			//	case "Totem":
			//		TotemOn = value;
			//		break;

			//	case "Lore":
			//		LoreOn = value;
			//		break;
			//}
		}

		//public bool SkillOn = true;
		//public bool CharmOn = true;
		//public bool KeyOn = true;
		//public bool MaskOn = true;
		//public bool VesselOn = true;
		//public bool NotchOn = true;
		//public bool OreOn = true;
		//public bool EggOn = true;
		//public bool RelicOn = true;
		//public bool EssenceBossOn = true;
		//public bool CocoonOn = true;
		//public bool GeoOn = true;
		//public bool RockOn = true;
		//public bool TotemOn = true;
		//public bool LoreOn = true;
	}
}