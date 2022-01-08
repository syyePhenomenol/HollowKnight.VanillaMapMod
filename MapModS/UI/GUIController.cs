using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MapModS.UI
{
    // All the following was modified from the GUI implementation of BenchwarpMod by homothetyhk
    public class GUIController : MonoBehaviour
    {
        public Dictionary<string, Texture2D> Images = new();

        private static GUIController _instance;

        private GameObject _pauseCanvas;

        public static GUIController Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<GUIController>();

                if (_instance != null) return _instance;

                MapModS.Instance.LogWarn("Couldn't find GUIController");

                GameObject GUIObj = new();
                _instance = GUIObj.AddComponent<GUIController>();
                DontDestroyOnLoad(GUIObj);

                return _instance;
            }
        }

        public Font TrajanBold { get; private set; }

        public Font TrajanNormal { get; private set; }

        private Font Arial { get; set; }

        public static void Setup()
        {
            GameObject GUIObj = new("MapModS GUI");
            _instance = GUIObj.AddComponent<GUIController>();
            DontDestroyOnLoad(GUIObj);
        }

        public static void Unload()
        {
            if (_instance != null)
            {
                Destroy(_instance._pauseCanvas);
                Destroy(_instance.gameObject);
            }
        }

        public void BuildMenus()
        {
            LoadResources();

            _pauseCanvas = new GameObject();
            _pauseCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler pauseScaler = _pauseCanvas.AddComponent<CanvasScaler>();
            pauseScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            pauseScaler.referenceResolution = new Vector2(1920f, 1080f);
            _pauseCanvas.AddComponent<GraphicRaycaster>();

            PauseMenu.BuildMenu(_pauseCanvas);

            DontDestroyOnLoad(_pauseCanvas);
        }

        public void Update()
        {
            try
            {
                PauseMenu.Update();
            }
            catch (Exception e)
            {
                MapModS.Instance.LogError(e);
            }
        }

        private void LoadResources()
        {
            TrajanBold = Modding.CanvasUtil.TrajanBold;
            TrajanNormal = Modding.CanvasUtil.TrajanNormal;

            try
            {
                Arial = Font.CreateDynamicFontFromOSFont
                (
                    Font.GetOSInstalledFontNames().First(x => x.ToLower().Contains("arial")),
                    13
                );
            }
            catch
            {
                MapModS.Instance.LogWarn("Unable to find Arial! Using Perpetua.");
                Arial = Modding.CanvasUtil.GetFont("Perpetua");
            }

            if (TrajanBold == null || TrajanNormal == null || Arial == null)
            {
                MapModS.Instance.LogError("Could not find game fonts");
            }

            Assembly asm = Assembly.GetExecutingAssembly();

            foreach (string res in asm.GetManifestResourceNames())
            {
                if (!res.StartsWith("MapModS.Resources.GUI.")) continue;

                try
                {
                    using Stream imageStream = asm.GetManifestResourceStream(res);
                    byte[] buffer = new byte[imageStream.Length];
                    imageStream.Read(buffer, 0, buffer.Length);

                    Texture2D tex = new(1, 1);
                    tex.LoadImage(buffer.ToArray());

                    string[] split = res.Split('.');
                    string internalName = split[split.Length - 2];

                    Images.Add(internalName, tex);
                }
                catch (Exception e)
                {
                    MapModS.Instance.LogError("Failed to load image: " + res + "\n" + e);
                }
            }
        }
    }
}