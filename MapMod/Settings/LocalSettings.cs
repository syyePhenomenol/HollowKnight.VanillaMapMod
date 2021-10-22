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

			public bool Has; // The corresponding pin has been bought
			public bool On; // The corresponding pin will be shown on the map (if Has)
		};

		public Dictionary<string, bool> ObtainedItems = new();

		public Dictionary<string, GroupSettingPair> GroupSettings = new()
		{
			// Copied settings from PlayerData
			{ "Bench", new() },
			{ "Grave", new() },
			{ "Grub", new() },
			{ "Root", new() },
			{ "Spa", new() },
			{ "Stag", new() },
			{ "Tram", new() },
			{ "Vendor", new() },

			// Also copied, but using custom pins
			{ "Cocoon", new() },

			// New settings, custom pins
			{ "Charm", new() },
			{ "Egg", new() },
			{ "EssenceBoss", new() },
			{ "Geo", new() },
			{ "Key", new() },
			{ "Lore", new() },
			{ "Mask", new() },
			{ "Notch", new() },
			{ "Ore", new() },
			{ "Relic", new() },
			{ "Rock", new() },
			{ "Skill", new() },
			{ "Totem", new() },
			{ "Vessel", new() },
		};

		public bool RevealFullMap = false;

        public void ToggleFullMap()
        {
			RevealFullMap = !RevealFullMap;

			// Force all pins to show again
			foreach (KeyValuePair<string, GroupSettingPair> entry in GroupSettings)
            {
                entry.Value.On = true;
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

		public void SetHasFromGroup(string group, bool value)
		{
			if (GroupSettings.ContainsKey(group))
			{
				GroupSettings[group].Has = value;
			}
			else
			{
				// Set based on ORIGINAL PlayerData settings
				switch (group)
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
			if (RevealFullMap)
            {
				return false;
            }

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

		public void ToggleGroups()
		{
			if (AllHasIsOff())
			{
				foreach (GroupSettingPair settingPair in GroupSettings.Values)
				{
					if (settingPair.Has || RevealFullMap)
					{
						settingPair.On = true;
					}
				}
			}
			else
			{
				foreach (GroupSettingPair settingPair in GroupSettings.Values)
				{
					if (settingPair.Has || RevealFullMap)
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
				if (settingPair.Has || RevealFullMap)
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
				if (settingPair.Has || RevealFullMap)
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