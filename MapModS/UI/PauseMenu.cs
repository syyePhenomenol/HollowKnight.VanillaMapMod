using System.Collections.Generic;
using UnityEngine;
using MapModS.CanvasUtil;
using MapModS.Data;
using MapModS.Map;
using MapModS.Trackers;

namespace MapModS.UI
{
    // All the following was modified from the GUI implementation of BenchwarpMod by homothetyhk
    internal class PauseMenu
    {
        public static GameObject Canvas;

        private static readonly Dictionary<Pool, (string, Vector2)> _groupButtons = new()
        {
            [Pool.Bench] = ("Benches", new Vector2(0f, 0f)),
            [Pool.Vendor] = ("Vendors", new Vector2(100f, 0f)),
            [Pool.Stag] = ("Stag\nStations", new Vector2(200f, 0f)),
            [Pool.Spa] = ("Hot\nSprings", new Vector2(300f, 0f)),
            [Pool.Root] = ("Whispering\nRoots", new Vector2(400f, 0f)),
            [Pool.Grave] = ("Warrior's\nGraves", new Vector2(500f, 0f)),
            [Pool.Tram] = ("Trams", new Vector2(600f, 0f)),
            [Pool.Grub] = ("Grubs", new Vector2(700f, 0f)),

            [Pool.Cocoon] = ("Lifeblood\nCocoons", new Vector2(0f, 30f)),

            [Pool.Skill] = ("Skills", new Vector2(100f, 30f)),
            [Pool.Charm] = ("Charms", new Vector2(200f, 30f)),
            [Pool.Map] = ("Maps", new Vector2(300f, 30f)),
            [Pool.Key] = ("Keys", new Vector2(400f, 30f)),
            [Pool.Mask] = ("Mask\nShards", new Vector2(500f, 30f)),
            [Pool.Vessel] = ("Vessel\nFragments", new Vector2(600f, 30f)),
            [Pool.Notch] = ("Charm\nNotches", new Vector2(700f, 30f)),
            [Pool.Ore] = ("Pale Ore", new Vector2(0f, 60f)),
            [Pool.Egg] = ("Rancid\nEggs", new Vector2(100f, 60f)),
            [Pool.Relic] = ("Relics", new Vector2(200f, 60f)),
            [Pool.EssenceBoss] = ("Hidden\nBosses", new Vector2(300f, 60f)),
            [Pool.Geo] = ("Geo Chests", new Vector2(400f, 60f)),
            [Pool.Rock] = ("Geo Rocks", new Vector2(500f, 60f)),
            [Pool.Totem] = ("Soul\nTotems", new Vector2(600f, 60f)),
            [Pool.Lore] = ("Lore\nTablets", new Vector2(700f, 60f)),
        };

        private static CanvasPanel _mapControlPanel;

        public static void BuildMenu(GameObject _canvas)
        {
            Canvas = _canvas;

            _mapControlPanel = new CanvasPanel
                (_canvas, GUIController.Instance.Images["ButtonsMenuBG"], new Vector2(10f, 870f), new Vector2(1346f, 0f), new Rect(0f, 0f, 0f, 0f));
            _mapControlPanel.AddText("MapModLabel", "Vanilla Map Mod", new Vector2(0f, -25f), Vector2.zero, GUIController.Instance.TrajanNormal, 18);

            Rect buttonRect = new(0, 0, GUIController.Instance.Images["ButtonRect"].width, GUIController.Instance.Images["ButtonRect"].height);

            // Toggle pool buttons panel on and off
            _mapControlPanel.AddButton
                (
                    "AllPins",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(200f, -30f),
                    Vector2.zero,
                    ShowPinsClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "Show Pins:\nall",
                    fontSize: 10
                );

            // New panel for pool buttons
            CanvasPanel pools = _mapControlPanel.AddPanel
            (
                "PoolsPanel",
                GUIController.Instance.Images["ButtonRectEmpty"],
                new Vector2(0f, 0f),
                Vector2.zero,
                new Rect(0f, 0f, GUIController.Instance.Images["DropdownBG"].width, 270f)
            );
            _mapControlPanel.AddButton
            (
                "PoolsToggle",
                GUIController.Instance.Images["ButtonRect"],
                new Vector2(300f, -30f),
                Vector2.zero,
                s => PoolsPanelClicked(),
                buttonRect,
                GUIController.Instance.TrajanBold,
                "Customize\nPins",
                fontSize: 10
            );

            // Collapse panel
            pools.SetActive(false, true);

            // Pool buttons
            foreach (KeyValuePair<Pool, (string, Vector2)> pair in _groupButtons)
            {
                pools.AddButton
                (
                    pair.Key.ToString(),
                    GUIController.Instance.Images["ButtonRectEmpty"],
                    pair.Value.Item2,
                    Vector2.zero,
                    PoolClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    pair.Value.Item1,
                    fontSize: 10
                );
            }

            // Adjust pin size
            _mapControlPanel.AddButton
                (
                    "Pin Size",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(400, -30f),
                    Vector2.zero,
                    PinSizeClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "Pin Size\nsmall",
                    fontSize: 10
                );

            // Toggle full map on and off
            _mapControlPanel.AddButton
                (
                    "Reveal\nFull Map",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(500, -30f),
                    Vector2.zero,
                    RevealFullMapClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "Reveal\nFull Map",
                    fontSize: 10
                );

            UpdateGUI();

            _mapControlPanel.SetActive(false, true); // collapse all subpanels

            if (GameManager.instance.IsGamePaused())
            {
                _mapControlPanel.SetActive(true, false);
            }
        }

        // Called every frame
        public static void Update()
        {
            if (_mapControlPanel == null || GameManager.instance == null)
            {
                return;
            }

            if (HeroController.instance == null || !GameManager.instance.IsGameplayScene() || !GameManager.instance.IsGamePaused())
            {
                // Any time we aren't at the Pause Menu / don't want to show the UI otherwise
                if (_mapControlPanel.Active) _mapControlPanel.SetActive(false, true);
                return;
            }

            // On the frame that we enter the Pause Menu
            if (!_mapControlPanel.Active)
            {
                _mapControlPanel.Destroy();
                BuildMenu(Canvas);
            }
        }

        // Update all the buttons (text, color)
        public static void UpdateGUI()
        {
            foreach (Pool group in _groupButtons.Keys)
            {
                UpdatePool(group);
            }

            UpdateShowPins();
            UpdateRevealFullMap();
            UpdatePinSize();
        }

        private static void ShowPinsClicked(string buttonName)
        {
            MapModS.LS.ToggleGroups();
            UpdateGUI();
        }

        private static void UpdateShowPins()
        {
            if (!MapModS.LS.HasNoGroup())
            {
                if (MapModS.LS.AllHasIsOn())
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.green);
                    _mapControlPanel.GetButton("AllPins").UpdateText("Show Pins:\nall");
                }
                else if (MapModS.LS.AllHasIsOff())
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.white);
                    _mapControlPanel.GetButton("AllPins").UpdateText("Show Pins:\nnone");
                }
                else
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.yellow);
                    _mapControlPanel.GetButton("AllPins").UpdateText("Show Pins:\ncustom");
                }
            }
            else
            {
                _mapControlPanel.GetButton("AllPins").SetTextColor(Color.red);
                _mapControlPanel.GetButton("AllPins").UpdateText("No Pins\n Unlocked");
            }
        }

        private static void PoolsPanelClicked()
        {
            _mapControlPanel.TogglePanel("PoolsPanel");
        }

        private static void PoolClicked(string buttonName)
        {
            if (!MapModS.LS.GetHasFromGroup(buttonName) && !MapModS.LS.RevealFullMap) return;

            MapModS.LS.SetOnFromGroup(buttonName, !MapModS.LS.GetOnFromGroup(buttonName));
            UpdateGUI();
        }

        private static void UpdatePool(Pool pool)
        {
            if (!MapModS.LS.GetHasFromGroup(pool) && !MapModS.LS.RevealFullMap)
            {
                _mapControlPanel.GetPanel("PoolsPanel").GetButton(pool.ToString()).SetTextColor(Color.red);
                return;
            }

            if (pool == Pool.Rock)
            {
                _mapControlPanel.GetPanel("PoolsPanel").GetButton(pool.ToString()).UpdateText
                    (
                        _groupButtons[pool].Item1 + "\n"
                        + MapModS.LS.GeoRockCounter + " / " + "207"
                    );
            }

            bool setting = MapModS.LS.GetOnFromGroup(pool);

            _mapControlPanel.GetPanel("PoolsPanel").GetButton(pool.ToString()).SetTextColor
                (
                    setting ? Color.green : Color.white
                );
        }

        private static void RevealFullMapClicked(string buttonName)
        {
            MapModS.LS.ToggleFullMap();

            if (!MapModS.LS.RevealFullMap)
            {
                FullMap.PurgeMap();
            }

            UpdateGUI();
        }

        private static void UpdateRevealFullMap()
        {
            switch (MapModS.LS.RevealFullMap)
            {
                case true:
                    _mapControlPanel.GetButton("Reveal\nFull Map").SetTextColor(Color.green);
                    break;
                case false:
                    _mapControlPanel.GetButton("Reveal\nFull Map").SetTextColor(Color.white);
                    break;
            }
        }

        private static void PinSizeClicked(string buttonName)
        {
            MapModS.GS.TogglePinSize();

            UpdateGUI();
        }

        private static void UpdatePinSize()
        {
            switch (MapModS.GS.PinSizeSetting)
            {
                case Settings.GlobalSettings.PinSize.small:
                    _mapControlPanel.GetButton("Pin Size").UpdateText("Pin Size\nsmall");
                    break;
                case Settings.GlobalSettings.PinSize.medium:
                    _mapControlPanel.GetButton("Pin Size").UpdateText("Pin Size\nmedium");
                    break;
                case Settings.GlobalSettings.PinSize.large:
                    _mapControlPanel.GetButton("Pin Size").UpdateText("Pin Size\nlarge");
                    break;
            }
        }
    }
}