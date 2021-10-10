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
	}
}