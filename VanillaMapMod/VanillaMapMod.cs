using System;
using MapChanger;
using MapChanger.UI;
using Modding;
using UnityEngine;
using VanillaMapMod.Settings;

namespace VanillaMapMod
{
    public sealed class VanillaMapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
    {
        internal const string MOD = "VanillaMapMod";

        private static readonly string[] dependencies = new string[] 
        { 
            "MapChangerMod", 
            "CMICore"
        };

        private static readonly MapMode[] modes = new MapMode[]
        {
            new NormalMode(),
            new FullMapMode()
        };

        private static readonly Title title = new VmmTitle();

        private static readonly MainButton[] mainButtons = new MainButton[] 
        { 
            new ModEnabledButton(),
            new ModeButton(),
            new PinSizeButton(),
            new ModPinsButton(),
            new VanillaPinsButton(),
            new PoolsPanelButton(),
        };

        private static readonly ExtraButtonPanel[] extraButtonPanels = new ExtraButtonPanel[]
        {
            new PoolsPanel() 
        };

        internal static VanillaMapMod Instance { get; private set; }

        public VanillaMapMod()
        {
            Instance = this;
        }

        public override string GetVersion() => "2.0.2";
        public override int LoadPriority() => 10;

        internal static LocalSettings LS = new();

        public void OnLoadLocal(LocalSettings ls) => LS = ls;

        public LocalSettings OnSaveLocal() => LS;

        internal static GlobalSettings GS = new();
        public void OnLoadGlobal(GlobalSettings gs) => GS = gs;
        public GlobalSettings OnSaveGlobal() => GS;

        public override void Initialize()
        {
            LogDebug($"Initializing");

            foreach (string dependency in dependencies)
            {
                if (ModHooks.GetMod(dependency) is not Mod)
                {
                    MapChangerMod.Instance.LogWarn($"Dependency not found for {GetType().Name}: {dependency}");
                    return;
                }
            }

            try
            {
                Events.OnEnterGame += OnEnterGame;
                Events.OnQuitToMenu += OnQuitToMenu;
            }
            catch (Exception e)
            {
                LogError(e);
            }

            LogDebug($"Initialization complete.");
        }

        private static void OnEnterGame()
        {
            MapChanger.Settings.AddModes(modes);
            Events.OnSetGameMap += OnSetGameMap;
        }

        private static void OnQuitToMenu()
        {
            Events.OnSetGameMap -= OnSetGameMap;
        }

        private static void OnSetGameMap(GameObject goMap)
        {
            try
            {
                VmmPinManager.MakePins(goMap);

                title.Make();

                foreach (MainButton button in mainButtons)
                {
                    button.Make();
                }

                foreach (ExtraButtonPanel ebp in extraButtonPanels)
                {
                    ebp.Make();
                }
            }
            catch (Exception e)
            {
                Instance.LogError(e);
            }
        }
    }
}
