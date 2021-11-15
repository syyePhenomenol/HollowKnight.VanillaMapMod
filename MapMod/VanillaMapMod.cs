using System;
using Modding;
using VanillaMapMod.Data;
using VanillaMapMod.Map;
using VanillaMapMod.Settings;
using VanillaMapMod.Shop;
using VanillaMapMod.Trackers;
using VanillaMapMod.UI;

namespace VanillaMapMod
{
    public class VanillaMapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
    {
        public static VanillaMapMod Instance;

        private readonly string _version = "1.0.1";

        public override string GetVersion() => _version;

        public override int LoadPriority() => 10;

        public static LocalSettings LS { get; set; } = new LocalSettings();

        public void OnLoadLocal(LocalSettings s) => LS = s;

        public LocalSettings OnSaveLocal() => LS;

        public static GlobalSettings GS { get; set; } = new GlobalSettings();

        public void OnLoadGlobal(GlobalSettings s) => GS = s;

        public GlobalSettings OnSaveGlobal() => GS;

        public override void Initialize()
        {
            Log("Initializing...");

            Instance = this;

            try
            {
                SpriteManager.LoadEmbeddedPngs("VanillaMapMod.Resources.Pins");
            }
            catch (Exception e)
            {
                LogError($"Error loading sprites!\n{e}");
                throw;
            }

            try
            {
                DataLoader.Load();
            }
            catch (Exception e)
            {
                LogError($"Error loading data!\n{e}");
                throw;
            }

            // Track when items are picked up/Geo Rocks are broken
            ItemTracker.Hook();
            GeoRockTracker.Hook();

            // Add new Pins to the Shop
            ShopHooks.Hook();
            ShopChanger.Hook();

            // Modify overall Map behaviour
            WorldMap.Hook();
            QuickMap.Hook();
            PinsVanilla.Hook();
            SceneChanges.Hook();

            // Add a Pause Menu GUI
            GUI.Hook();

            Log("Initialization complete.");
        }
    }
}