using System;
using MapChanger;
using MapChanger.UI;
using Modding;
using UnityEngine;
using VanillaMapMod.Settings;

namespace VanillaMapMod;

public sealed class VanillaMapMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
{
    private static readonly string[] _dependencies = ["MapChangerMod", "CMICore"];

    private static readonly MapMode[] _modes = [new NormalMode(), new FullMapMode()];

    private static readonly Title _title = new VmmTitle();

    private static readonly MainButton[] _mainButtons =
    [
        new ModEnabledButton(),
        new ModeButton(),
        new PinSizeButton(),
        new PinShapeButton(),
        new ModPinsButton(),
        new VanillaPinsButton(),
        new PoolsPanelButton(),
    ];

    private static readonly ExtraButtonPanel[] _extraButtonPanels = [new PoolsPanel()];

    public VanillaMapMod()
    {
        Instance = this;
    }

    internal static VanillaMapMod Instance { get; private set; }

    internal static LocalSettings LS { get; private set; } = new();

    internal static GlobalSettings GS { get; private set; } = new();

    public override string GetVersion()
    {
        return "2.1.2";
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
        MapChanger.Settings.AddModes(_modes);
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

            PauseMenu.Add(_title);

            foreach (var button in _mainButtons)
            {
                PauseMenu.Add(button);
            }

            foreach (var ebp in _extraButtonPanels)
            {
                PauseMenu.Add(ebp);
            }
        }
        catch (Exception e)
        {
            Instance.LogError(e);
        }
    }
}
