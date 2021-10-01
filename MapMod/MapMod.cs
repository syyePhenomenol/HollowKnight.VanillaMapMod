using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Modding;
using SFCore.Generics;
using MapMod.Settings;

namespace MapMod
{
    public class MapMod : FullSettingsMod<SaveSettings, GlobalSettings>
    {
        private readonly string _version = "PRERELEASE";
        public override string GetVersion()
        {
            return _version;
        }

        public MapMod() : base("VanillaMapMod") { }

        public static SaveSettings SS { get; private set; } = new();
        public static GlobalSettings GS { get; private set; } = new();

        public override void Initialize()
        {
            base.Initialize();

            try
            {
                SpriteManager.LoadEmbeddedPngs("MapMod.Resources.Pins");
            }
            catch (Exception e)
            {
                LogError($"Error loading sprites!\n{e}");
                throw;
            }

            if (Compatibility.CheckAMapsInstalled())
            {
                Log("Additional Maps is installed.");
            }
            else
            {
                Log("Additional Maps is not installed.");
            }

            try
            {
                MapData.Data.Load();
            }
            catch (Exception e)
            {
                LogError($"Error loading MapData!\n{e}");
                throw;
            }

            WorldMap.Hook();

            Log("Initialization complete.");
        }
    }
}
