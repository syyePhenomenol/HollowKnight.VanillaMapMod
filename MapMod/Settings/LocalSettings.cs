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
			// Copied settings from PlayerData
			{ "Bench", new() },
			{ "Vendor", new() },
			{ "Stag", new() },
			{ "Spa", new() },
			{ "Root", new() },
			{ "Grave", new() },
			{ "Tram", new() },
			{ "Grub", new() },

			// Also copied, but using custom pins
			{ "Cocoon", new() },

			// New settings, custom pins
			{ "Skill", new() },
			{ "Charm", new() },
			{ "Key", new() },
			{ "Mask", new() },
			{ "Vessel", new() },
			{ "Notch", new() },
			{ "Ore", new() },
			{ "Egg", new() },
			{ "Relic", new() },
			{ "EssenceBoss", new() },
			{ "Geo", new() },
			{ "Rock", new() },
			{ "Totem", new() },
			{ "Lore", new() },
		};

		public bool GotFullMap = false;

		public void SetFullMap()
		{
			GotFullMap = true;

			foreach (KeyValuePair<string, GroupSettingPair> entry in GroupSettings)
			{
				entry.Value.Has = true;
			}
		}

		public bool IsGroup(string boolName)
		{
			if (GroupSettings.ContainsKey(boolName))
			{
				return true;
			}
			return false;
		}

		public bool GetHasFromGroup(string group)
		{
			if (GroupSettings.ContainsKey(group))
			{
				return GroupSettings[group].Has;
			}

			return false;
		}

		public void SetHasFromGroup(string boolName, bool value)
		{
			if (GroupSettings.ContainsKey(boolName))
			{
				GroupSettings[boolName].Has = value;
			}
			else
			{
				switch (boolName)
				{
					case "hasPinBench":
						GroupSettings["Bench"].Has = value;
						break;
					case "hasPinCocoon":
						GroupSettings["Cocoon"].Has = value;
						break;
					case "hasPinDreamPlant":
						GroupSettings["Root"].Has = value;
						break;
					case "hasPinGhost":
						GroupSettings["Grave"].Has = value;
						break;
					case "hasPinGrub":
						GroupSettings["Grub"].Has = value;
						break;
					case "hasPinShop":
						GroupSettings["Vendor"].Has = value;
						break;
					case "hasPinSpa":
						GroupSettings["Spa"].Has = value;
						break;
					case "hasPinStag":
						GroupSettings["Stag"].Has = value;
						break;
					case "hasPinTram":
						GroupSettings["Tram"].Has = value;
						break;

					default:
						break;
				};
			}
		}

		public bool HasNoGroup()
		{
			foreach (GroupSettingPair settingPair in GroupSettings.Values)
			{
				if (settingPair.Has)
				{
					return false;
				}
			}

			return true;
		}

		public bool GetOnFromGroup(string group)
		{
			if (GroupSettings.ContainsKey(group))
			{
				return GroupSettings[group].On;
			}

			return false;
		}

		public void SetOnFromGroup(string group, bool value)
		{
			if (GroupSettings.ContainsKey(group))
			{
				GroupSettings[group].On = value;
			}
		}

		//public bool AllGroupsOn()
		//{
		//	foreach (GroupSettingPair settingPair in GroupSettings.Values)
		//	{
		//		if (!settingPair.On)
		//		{
		//			return false;
		//		}
		//	}

		//	return true;
		//}

		//public bool AllGroupsOff()
		//{
		//	foreach (GroupSettingPair settingPair in GroupSettings.Values)
		//	{
		//		if (settingPair.On)
		//		{
		//			return false;
		//		}
		//	}

		//	return true;
		//}

		public void ToggleGroups()
		{
			if (AllHasIsOff())
			{
				foreach (GroupSettingPair settingPair in GroupSettings.Values)
				{
					if (settingPair.Has)
					{
						settingPair.On = true;
					}
				}
			}
			else
			{
				foreach (GroupSettingPair settingPair in GroupSettings.Values)
				{
					if (settingPair.Has)
					{
						settingPair.On = false;
					}
				}
			}
		}

		public bool AllHasIsOn()
		{
			foreach (GroupSettingPair settingPair in GroupSettings.Values)
			{
				if (settingPair.Has)
				{
					if (!settingPair.On)
					{
						return false;
					}
				}
			}

			return true;
		}

		public bool AllHasIsOff()
		{
			foreach (GroupSettingPair settingPair in GroupSettings.Values)
			{
				if (settingPair.Has)
				{
					if (settingPair.On)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}