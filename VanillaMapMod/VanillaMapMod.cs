using System;
using MapChanger;
using Modding;
using UnityEngine;
using VanillaMapMod.Settings;
using VanillaMapMod.UI;

namespace VanillaMapMod;

public sealed class VanillaMapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
{
    private static readonly string[] _dependencies = ["MapChangerMod", "CMICore"];

    private static readonly MapMode[] _modes = [new NormalMode(), new FullMapMode()];

    public VanillaMapMod()
    {
        Instance = this;
    }

    internal static VanillaMapMod Instance { get; private set; }
    internal static LocalSettings LS { get; private set; } = new();
    internal static GlobalSettings GS { get; private set; } = new();

    internal static VmmPauseMenu PauseMenu { get; private set; }

    public override string GetVersion()
    {
        return "2.1.3";
    }

    public override int LoadPriority()
    {
        return 10;
    }

    public void OnLoadLocal(LocalSettings ls)
    {
        LS = ls;
    }

    public LocalSettings OnSaveLocal()
    {
        return LS;
    }

    public void OnLoadGlobal(GlobalSettings gs)
    {
        GS = gs;
    }

    public GlobalSettings OnSaveGlobal()
    {
        return GS;
    }

    public override void Initialize()
    {
        LogDebug($"Initializing");

        foreach (var dependency in _dependencies)
        {
            if (ModHooks.GetMod(dependency) is not Mod)
            {
                MapChangerMod.Instance.LogWarn($"Dependency not found for {GetType().Name}: {dependency}");
                return;
            }
        }

        Events.OnEnterGame += OnEnterGame;
        Events.OnQuitToMenu += OnQuitToMenu;

        LogDebug($"Initialization complete.");
    }

    private static void OnEnterGame()
    {
        ModeManager.AddModes(_modes);

        Events.OnSetGameMap += OnSetGameMap;
    }

    private static void OnQuitToMenu()
    {
        Events.OnSetGameMap -= OnSetGameMap;

        PauseMenu?.Destroy();
        PauseMenu = null;
    }

    private static void OnSetGameMap(GameObject goMap)
    {
        try
        {
            VmmPinManager.MakePins(goMap);
            PauseMenu = new();
        }
        catch (Exception e)
        {
            Instance.LogError(e);
        }
    }
}
