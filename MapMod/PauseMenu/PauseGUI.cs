using VanillaMapMod.CanvasUtil;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using VanillaMapMod.Map;

namespace VanillaMapMod.PauseMenu
{
	// All the following was modified from the GUI implementation of BenchwarpMod by homothetyhk
	internal class PauseGUI
	{
		public static GameObject Canvas;

		//private static readonly Dictionary<string, (UnityAction<string>, Vector2)> _buttons = new Dictionary<string, (UnityAction<string>, Vector2)>
		//{
		//	//["Spoilers"] = (_SpoilersClicked, new Vector2(0f, 0f)),
		//	//["Style"] = (_StyleClicked, new Vector2(100f, 0f)),
		//	//["Randomized"] = (_RandomizedClicked, new Vector2(200f, 0f)),
		//	//["Others"] = (_OthersClicked, new Vector2(300f, 0f)),
		//};

		private static readonly Dictionary<string, (string, Vector2)> _groupButtons = new()
		{
			["Skill"] = ("Skills", new Vector2(0f, 30f)),
			["Charm"] = ("Charms", new Vector2(100f, 30f)),
			["Key"] = ("Keys", new Vector2(200f, 30f)),
			["Mask"] = ("Mask\nShards", new Vector2(300f, 30f)),
			["Vessel"] = ("Vessel\nFragments", new Vector2(400f, 30f)),
			["Notch"] = ("Charm\nNotches", new Vector2(500f, 30f)),
			["Ore"] = ("Pale Ore", new Vector2(600f, 30f)),
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

            //// Main settings
            //foreach (KeyValuePair<string, (UnityAction<string>, Vector2)> pair in _buttons)
            //{
            //	_mapControlPanel.AddButton
            //	(
            //		pair.Key,
            //		GUIController.Instance.Images["ButtonRect"],
            //		pair.Value.Item2,
            //		Vector2.zero,
            //		pair.Value.Item1,
            //		buttonRect,
            //		GUIController.Instance.TrajanBold,
            //		pair.Key,
            //		fontSize: 10
            //	);
            //}

            // These buttons only appear if the full map hasn't been revealed yet
            if (!VanillaMapMod.LS.GotFullMap)
            {
                _mapControlPanel.AddButton
                (
                    "Reveal\nFull Map",
                    GUIController.Instance.Images["ButtonRect"],
                    new Vector2(100, 0f),
                    Vector2.zero,
                    RevealFullMapClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    "Reveal\nFull Map",
                    fontSize: 10
                );
                SetRevealFullMap();
            }

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
                new Vector2(0f, 0f),
                Vector2.zero,
                s => PoolsClicked(),
                buttonRect,
                GUIController.Instance.TrajanBold,
                "Custom Pins",
                fontSize: 10
            );
            pools.SetActive(false, true);

            //Pool buttons
            foreach (KeyValuePair<string, (string, Vector2)> pair in _groupButtons)
            {
                pools.AddButton
                (
                    pair.Key,
                    GUIController.Instance.Images["ButtonRect"],
                    pair.Value.Item2,
                    Vector2.zero,
                    PoolButtonClicked,
                    buttonRect,
                    GUIController.Instance.TrajanBold,
                    pair.Value.Item1,
                    fontSize: 10
                );
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

        // This one is independent of the ShowButtons function as the button may or may not exist
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

        //private static void _ShowPinsClicked(string buttonName)
        //{
        //	VanillaMapMod.Instance.Settings.ShowAllPins = !VanillaMapMod.Instance.Settings.ShowAllPins;
        //	_SetShowPins();
        //}

        //// This one is independent of the ShowButtons function as the button may or may not exist
        //private static void _SetShowPins()
        //{
        //	if (VanillaMapMod.Instance.Settings.ShowAllPins)
        //	{
        //		_mapControlPanel.GetButton("Show Pins").UpdateText("Show Pins:\neverywhere");
        //	}
        //	else
        //	{
        //		_mapControlPanel.GetButton("Show Pins").UpdateText("Show Pins:\nover map");
        //	}
        //}

        

        private static void PoolButtonClicked(string buttonName)
        {
            if (VanillaMapMod.LS.GetHasFromGroup(buttonName))
            {
                WorldMap.CustomPins.ToggleGroup(buttonName);
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

        private static void PoolsClicked()
        {
            _mapControlPanel.TogglePanel("PoolsPanel");
            //_mapControlPanel.GetButton("PoolsToggle").UpdateText
            //    (
            //        _mapControlPanel.GetPanel("PoolsPanel").Active ? "Hide\nCustom Pins" : "Show\nCustom Pins"
            //    );
        }

    }
}