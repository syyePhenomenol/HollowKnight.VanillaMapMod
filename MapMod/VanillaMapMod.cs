using System;
using System.Reflection;
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

        private readonly string _version = "1.0.2";

        public override string GetVersion() => _version;

        public override int LoadPriority() => 10;

        public static LocalSettings LS { get; set; } = new LocalSettings();

        public void OnLoadLocal(LocalSettings s) => LS = s;

        public LocalSettings OnSaveLocal() => LS;

        public static GlobalSettings GS { get; set; } = new GlobalSettings();

        public void OnLoadGlobal(GlobalSettings s) => GS = s;

        public GlobalSettings OnSaveGlobal() => GS;

        public static bool AdditionalMapsInstalled = false;

        public override void Initialize()
        {
            Log("Initializing...");

            Instance = this;

            AdditionalMapsInstalled = HasAdditionalMaps();

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
            FullMap.Hook();
            PinsVanilla.Hook();
            Quill.Hook();

            // Add a Pause Menu GUI
            GUI.Hook();

            Log("Initialization complete.");
        }

        public static bool HasAdditionalMaps()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == "AdditionalMaps")
                    {
                        Instance.Log("Additional Maps detected");
                        return true;
                    }

                        //foreach (FieldInfo field in type.GetFields())
                        //{
                        //    VanillaMapMod.Instance.Log(field.Name);
                        //}
                }
            }

            return false;
        }
    }
}