using System;
using Modding;
using VanillaMapMod.Data;
using VanillaMapMod.Map;
using VanillaMapMod.Settings;
using VanillaMapMod.Shop;
using VanillaMapMod.Trackers;

namespace VanillaMapMod
{
    public class VanillaMapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
    {
        public static VanillaMapMod Instance;

        private readonly string _version = "PRERELEASE 0.0.3";

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
                LogError($"Error loading MapData!\n{e}");
                throw;
            }

            // Handles adding Custom Pins to World Map and Show/Hide behaviour when opening the Map
            WorldMap.Hook();

            // Handles Show/Hide behaviour when Quick Map is opened
            QuickMap.Hook();

            // Modifies existing Vanilla Pins to match Custom Pins behaviour
            PinsVanilla.Hook();

            // Updates scenes mapped per scene change
            SceneChanges.Hook();

            // The following updates obtained items based on certain triggers
            GeoRockTracker.Hook();
            ItemTracker.Hook();

            // Adds new Pins to the Shop
            ShopHooks.Hook();
            ShopChanger.Hook();

            Log("Initialization complete.");
        }
    }
}