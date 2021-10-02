using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapMod.Settings;
using Modding;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore.Generics;
using SFCore.Utils;
using UnityEngine.SceneManagement;
using Logger = Modding.Logger;
using UObject = UnityEngine.Object;


namespace MapMod
{
    public class MapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
    {
        public static MapMod Instance;

        private readonly string _version = "Vanilla PRERELEASE";
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
                SpriteManager.LoadEmbeddedPngs("MapMod.Resources.Pins");
            }
            catch (Exception e)
            {
                LogError($"Error loading sprites!\n{e}");
                throw;
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
            QuickMap.Hook();

            Log("Initialization complete.");
        }
    }
}
