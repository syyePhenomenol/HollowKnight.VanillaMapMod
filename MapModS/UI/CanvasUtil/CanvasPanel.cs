using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MapModS.CanvasUtil
{
    // Code borrowed from homothety
    public class CanvasPanel
    {
        public bool Active;
        private readonly Dictionary<string, CanvasButton> _buttons = new();
        private readonly Dictionary<string, CanvasImage> _images = new();
        private readonly Dictionary<string, CanvasPanel> _panels = new();
        private readonly Vector2 _size;
        private readonly Dictionary<string, CanvasText> _texts = new();
        private readonly CanvasImage _background;
        private readonly GameObject _canvas;

        private Vector2 _position;

        public CanvasPanel(GameObject parent, Texture2D tex, Vector2 pos, Vector2 sz, Rect bgSubSection)
        {
            if (parent == null) return;

            _position = pos;
            _size = sz;
            _canvas = parent;
            _background = new CanvasImage(parent, tex, pos, sz, bgSubSection);

            Active = true;
        }

        public void AddButton(string name, Texture2D tex, Vector2 pos, Vector2 sz, UnityAction<string> func, Rect bgSubSection, Font font = null, string text = null, int fontSize = 13)
        {
            CanvasButton button = new(_canvas, name, tex, _position + pos, _size + sz, bgSubSection, font, text, fontSize);
            button.AddClickEvent(func);

            _buttons.Add(name, button);
        }

        public void AddImage(string name, Texture2D tex, Vector2 pos, Vector2 size, Rect subSprite)
        {
            CanvasImage image = new(_canvas, tex, _position + pos, size, subSprite);

            _images.Add(name, image);
        }

        public CanvasPanel AddPanel(string name, Texture2D tex, Vector2 pos, Vector2 sz, Rect bgSubSection)
        {
            CanvasPanel panel = new(_canvas, tex, _position + pos, sz, bgSubSection);

            _panels.Add(name, panel);

            return panel;
        }

        public void AddText(string name, string text, Vector2 pos, Vector2 sz, Font font, int fontSize = 13, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            CanvasText t = new(_canvas, _position + pos, sz, font, text, fontSize, style, alignment);

            _texts.Add(name, t);
        }

        public void ClearButtons()
        {
            foreach (CanvasButton button in _buttons.Values)
            {
                button.Destroy();
            }
            _buttons.Clear();
        }

        public void Destroy()
        {
            _background.Destroy();

            foreach (CanvasButton button in _buttons.Values)
            {
                button.Destroy();
            }

            foreach (CanvasImage image in _images.Values)
            {
                image.Destroy();
            }

            foreach (CanvasText t in _texts.Values)
            {
                t.Destroy();
            }

            foreach (CanvasPanel p in _panels.Values)
            {
                p.Destroy();
            }
        }

        public void FixRenderOrder()
        {
            foreach (CanvasText t in _texts.Values)
            {
                t.MoveToTop();
            }

            foreach (CanvasButton button in _buttons.Values)
            {
                button.MoveToTop();
            }

            foreach (CanvasImage image in _images.Values)
            {
                image.SetRenderIndex(0);
            }

            foreach (CanvasPanel panel in _panels.Values)
            {
                panel.FixRenderOrder();
            }

            _background.SetRenderIndex(0);
        }

        public CanvasButton GetButton(string buttonName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetButton(buttonName);
            }

            if (_buttons.ContainsKey(buttonName))
            {
                return _buttons[buttonName];
            }

            return null;
        }

        public CanvasImage GetImage(string imageName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetImage(imageName);
            }

            if (_images.ContainsKey(imageName))
            {
                return _images[imageName];
            }

            return null;
        }

        public CanvasPanel GetPanel(string panelName)
        {
            if (_panels.ContainsKey(panelName))
            {
                return _panels[panelName];
            }

            return null;
        }

        public CanvasText GetText(string textName, string panelName = null)
        {
            if (panelName != null && _panels.ContainsKey(panelName))
            {
                return _panels[panelName].GetText(textName);
            }

            if (_texts.ContainsKey(textName))
            {
                return _texts[textName];
            }

            return null;
        }

        public void ResizeBG(Vector2 sz)
        {
            _background.SetWidth(sz.x);
            _background.SetHeight(sz.y);
            _background.SetPosition(_position);
        }

        public void SetActive(bool b, bool panel)
        {
            _background.SetActive(b);

            foreach (CanvasButton button in _buttons.Values)
            {
                button.SetActive(b);
            }

            foreach (CanvasImage image in _images.Values)
            {
                image.SetActive(b);
            }

            foreach (CanvasText t in _texts.Values)
            {
                t.SetActive(b);
            }

            if (panel)
            {
                foreach (CanvasPanel p in _panels.Values)
                {
                    p.SetActive(b, false);
                }
            }

            Active = b;
        }

        public void SetPosition(Vector2 pos)
        {
            _background.SetPosition(pos);

            Vector2 deltaPos = _position - pos;
            _position = pos;

            foreach (CanvasButton button in _buttons.Values)
            {
                button.SetPosition(button.GetPosition() - deltaPos);
            }

            foreach (CanvasText text in _texts.Values)
            {
                text.SetPosition(text.GetPosition() - deltaPos);
            }

            foreach (CanvasPanel panel in _panels.Values)
            {
                panel.SetPosition(panel._position - deltaPos);
            }
        }

        public void ToggleActive()
        {
            Active = !Active;
            SetActive(Active, false);
        }

        public void TogglePanel(string name)
        {
            if (Active && _panels.ContainsKey(name))
            {
                _panels[name].ToggleActive();
            }
        }

        public void UpdateBackground(Texture2D tex, Rect subSection)
        {
            _background.UpdateImage(tex, subSection);
        }

        private Vector2 GetPosition()
        {
            return _position;
        }
    }
}