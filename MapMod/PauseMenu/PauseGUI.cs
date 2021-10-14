using VanillaMapMod.CanvasUtil;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using VanillaMapMod.Map;
using VanillaMapMod.Settings;

namespace VanillaMapMod.PauseMenu
{
	// All the following was modified from the GUI implementation of BenchwarpMod by homothetyhk
	internal class PauseGUI
	{
		public static GameObject Canvas;

		private static readonly Dictionary<string, (string, Vector2)> _groupButtons = new()
		{
            ["Bench"] = ("Benches", new Vector2(0f, 0f)),
            ["Vendor"] = ("Vendors", new Vector2(100f, 0f)),
            ["Stag"] = ("Stag\nStations", new Vector2(200f, 0f)),
            ["Spa"] = ("Hot\nSprings", new Vector2(300f, 0f)),
            ["Root"] = ("Whispering\nRoots", new Vector2(400f, 0f)),
            ["Grave"] = ("Warrior's\nGraves", new Vector2(500f, 0f)),
            ["Tram"] = ("Trams", new Vector2(600f, 0f)),
            ["Grub"] = ("Grubs", new Vector2(700f, 0f)),

            ["Cocoon"] = ("Lifeblood\nCocoons", new Vector2(0f, 30f)),

            ["Skill"] = ("Skills", new Vector2(100f, 30f)),
			["Charm"] = ("Charms", new Vector2(200f, 30f)),
			["Key"] = ("Keys", new Vector2(300f, 30f)),
			["Mask"] = ("Mask\nShards", new Vector2(400f, 30f)),
			["Vessel"] = ("Vessel\nFragments", new Vector2(500f, 30f)),
			["Notch"] = ("Charm\nNotches", new Vector2(600f, 30f)),
			["Ore"] = ("Pale Ore", new Vector2(700f, 30f)),
			["Egg"] = ("Rancid\nEggs", new Vector2(0f, 60f)),
			["Relic"] = ("Relics", new Vector2(100f, 60f)),
			["EssenceBoss"] = ("Hidden\nBosses", new Vector2(200f, 60f)),
            ["Rock"] = ("Geo Rocks", new Vector2(300f, 60f)),
			["Geo"] = ("Geo Chests", new Vector2(400f, 60f)),
			["Totem"] = ("Soul\nTotems", new Vector2(500f, 60f)),
			["Lore"] = ("Lore\nTablets", new Vector2(600f, 60f)),
		};

		private static CanvasPanel _mapControlPanel;
        private static int _mapRevealCounter = 0;

        public static void BuildMenu(GameObject _canvas)
		{
			Canvas = _canvas;

			_mapControlPanel = new CanvasPanel
				(_canvas, GUIController.Instance.Images["ButtonsMenuBG"], new Vector2(10f, 870f), new Vector2(1346f, 0f), new Rect(0f, 0f, 0f, 0f));
			_mapControlPanel.AddText("MapModLabel", "Vanilla Map Mod", new Vector2(0f, -25f), Vector2.zero, GUIController.Instance.TrajanNormal, 18);

			Rect buttonRect = new(0, 0, GUIController.Instance.Images["ButtonRect"].width, GUIController.Instance.Images["ButtonRect"].height);

            _mapControlPanel.AddButton
                (
                    "AllPins",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(200f, -30f),
                    Vector2.zero,
                    AllPinsClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "All Pins\nOn",
                    fontSize: 10
                );
            SetAllPinsButton();

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
                s => PoolsClicked(),
                buttonRect,
                GUIController.Instance.TrajanBold,
                "Customize\nPins",
                fontSize: 10
            );
            pools.SetActive(false, true);

            //Pool buttons
            foreach (KeyValuePair<string, (string, Vector2)> pair in _groupButtons)
            {
                pools.AddButton
                (
                    pair.Key,
                    GUIController.Instance.Images["ButtonRectEmpty"],
                    pair.Value.Item2,
                    Vector2.zero,
                    PoolButtonClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    pair.Value.Item1,
                    fontSize: 10
                );
            }

            // Only appears if the full map hasn't been revealed yet
            if (!VanillaMapMod.LS.GotFullMap)
            {
                _mapControlPanel.AddButton
                (
                    "Reveal\nFull Map",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(400, -30f),
                    Vector2.zero,
                    RevealFullMapClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "Reveal\nFull Map",
                    fontSize: 10
                );
                SetRevealFullMap();
            }

            SetGUI();

			_mapControlPanel.SetActive(false, true); // collapse all subpanels
			if (GameManager.instance.IsGamePaused())
			{
				_mapControlPanel.SetActive(true, false);
			}
		}

		public static void RebuildMenu()
		{
			_mapControlPanel.Destroy();
			BuildMenu(Canvas);
		}

		public static void Update()
		{
			if (_mapControlPanel == null || GameManager.instance == null)
			{
				return;
			}

			if (HeroController.instance == null || !GameManager.instance.IsGameplayScene() || !GameManager.instance.IsGamePaused())
			{
				if (_mapControlPanel.Active) _mapControlPanel.SetActive(false, true);
				_mapRevealCounter = 0;
				return;
			}
			else
			{
				if (!_mapControlPanel.Active)
				{
					RebuildMenu();
				}
			}
		}

		public static void SetGUI()
		{
            foreach (string group in _groupButtons.Keys)
            {
                SetPoolButton(group);
            }

            SetAllPinsButton();
        }

        private static void AllPinsClicked(string buttonName)
        {
            VanillaMapMod.LS.ToggleGroups();
            SetGUI();
        }

        private static void SetAllPinsButton()
        {
            if (!VanillaMapMod.LS.HasNoGroup())
            {
                if (VanillaMapMod.LS.AllHasIsOn())
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.green);
                    _mapControlPanel.GetButton("AllPins").UpdateText("All Pins:\non");
                }
                else if (VanillaMapMod.LS.AllHasIsOff())
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.white);
                    _mapControlPanel.GetButton("AllPins").UpdateText("All Pins:\noff");
                }
                else
                {
                    _mapControlPanel.GetButton("AllPins").SetTextColor(Color.yellow);
                    _mapControlPanel.GetButton("AllPins").UpdateText("All Pins:\ncustom");
                }
            }
            else
            {
                _mapControlPanel.GetButton("AllPins").SetTextColor(Color.red);
                _mapControlPanel.GetButton("AllPins").UpdateText("No Pins\n Unlocked");
            }
        }

        private static void PoolsClicked()
        {
            _mapControlPanel.TogglePanel("PoolsPanel");
        }

        private static void PoolButtonClicked(string buttonName)
        {
            if (VanillaMapMod.LS.GetHasFromGroup(buttonName))
            {
                VanillaMapMod.LS.SetOnFromGroup(buttonName, !VanillaMapMod.LS.GetOnFromGroup(buttonName));
                SetGUI();

                // Toggling not needed here if we don't control while the map is open

                //if (SettingsUtil.IsCustomPinGroup(buttonName))
                //{
                //    WorldMap.CustomPins.SetGroup(buttonName);
                //}
            }
        }

        private static void SetPoolButton(string buttonName)
        {
            if (!VanillaMapMod.LS.GetHasFromGroup(buttonName))
            {
                _mapControlPanel.GetPanel("PoolsPanel").GetButton(buttonName).SetTextColor(Color.red);
                return;
            }

            bool setting = VanillaMapMod.LS.GetOnFromGroup(buttonName);

            _mapControlPanel.GetPanel("PoolsPanel").GetButton(buttonName).SetTextColor
                (
                    setting ? Color.green : Color.white
                );
        }

        private static void RevealFullMapClicked(string buttonName)
        {
            _mapRevealCounter++;

            if (_mapRevealCounter > 1)
            {
                WorldMap.GiveFullMap();
                _mapControlPanel.GetButton("Reveal\nFull Map").SetActive(false);
            }

            SetRevealFullMap();
        }

        private static void SetRevealFullMap()
        {
            if (_mapRevealCounter == 1)
            {
                _mapControlPanel.GetButton("Reveal\nFull Map").SetTextColor(Color.yellow);
                _mapControlPanel.GetButton("Reveal\nFull Map").UpdateText("Are you sure?");
            }
            else
            {
                _mapControlPanel.GetButton("Reveal\nFull Map").SetTextColor(Color.white);
            }
        }
    }
}