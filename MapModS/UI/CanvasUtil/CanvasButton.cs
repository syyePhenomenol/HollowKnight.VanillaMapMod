using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MapModS.CanvasUtil
{
    // Code borrowed from homothety
    public class CanvasButton
    {
        private readonly string _buttonName;
        private readonly GameObject _buttonObj;
        private readonly GameObject _textObj;
        private UnityAction<string> _clicked;

        public CanvasButton(GameObject parent, string name, Texture2D tex, Vector2 pos, Vector2 size, Rect bgSubSection, Font font = null, string text = null, int fontSize = 13)
        {
            if (Mathf.Abs(size.x) < Mathf.Epsilon || Mathf.Abs(size.y) < Mathf.Epsilon)
            {
                size = new Vector2(bgSubSection.width, bgSubSection.height);
            }

            _buttonName = name;

            _buttonObj = new GameObject();
            _buttonObj.AddComponent<CanvasRenderer>();

            RectTransform buttonTransform = _buttonObj.AddComponent<RectTransform>();

            buttonTransform.sizeDelta = new Vector2(bgSubSection.width, bgSubSection.height);

            _buttonObj.AddComponent<Image>().sprite = Sprite.Create
            (
                tex,
                new Rect
                (
                    bgSubSection.x,
                    tex.height - bgSubSection.height,
                    bgSubSection.width,
                    bgSubSection.height
                ),
                Vector2.zero
            );

            _buttonObj.AddComponent<Button>();

            _buttonObj.transform.SetParent(parent.transform, false);

            buttonTransform.SetScaleX(size.x / bgSubSection.width);
            buttonTransform.SetScaleY(size.y / bgSubSection.height);

            Vector2 position = new
            (
                (pos.x + size.x / bgSubSection.width * bgSubSection.width / 2f) / 1920f,
                (1080f - (pos.y + size.y / bgSubSection.height * bgSubSection.height / 2f)) / 1080f
            );

            buttonTransform.anchorMin = buttonTransform.anchorMax = position;

            Object.DontDestroyOnLoad(_buttonObj);

            if (font != null && text != null)
            {
                _textObj = new GameObject();
                _textObj.AddComponent<RectTransform>().sizeDelta = new Vector2(bgSubSection.width * 2, bgSubSection.height * 2);
                Text t = _textObj.AddComponent<Text>();
                t.text = text;
                t.font = font;
                t.fontSize = fontSize * 2;
                t.alignment = TextAnchor.MiddleCenter;
                _textObj.transform.SetParent(_buttonObj.transform, false);

                _textObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                Object.DontDestroyOnLoad(_textObj);
            }
        }

        public void AddClickEvent(UnityAction<string> action)
        {
            if (_buttonObj != null)
            {
                _clicked = action;
                _buttonObj.GetComponent<Button>().onClick.AddListener(ButtonClicked);
            }
        }

        public void ButtonClicked()
        {
            if (_clicked != null && _buttonName != null) _clicked(_buttonName);
        }

        public void Destroy()
        {
            Object.Destroy(_buttonObj);
            Object.Destroy(_textObj);
        }

        public Vector2 GetPosition()
        {
            if (_buttonObj != null)
            {
                Vector2 anchor = _buttonObj.GetComponent<RectTransform>().anchorMin;
                Vector2 sz = _buttonObj.GetComponent<RectTransform>().sizeDelta;

                return new Vector2(anchor.x * 1920f - sz.x / 2f, 1080f - anchor.y * 1080f - sz.y / 2f);
            }

            return Vector2.zero;
        }

        public string GetText()
        {
            if (_textObj != null)
            {
                return _textObj.GetComponent<Text>().text;
            }

            return null;
        }

        public void MoveToTop()
        {
            if (_buttonObj != null)
            {
                _buttonObj.transform.SetAsLastSibling();
            }
        }

        public void SetActive(bool b)
        {
            if (_buttonObj != null)
            {
                _buttonObj.SetActive(b);
            }
        }

        public void SetHeight(float height)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<RectTransform>().SetScaleY(height / _buttonObj.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            if (_buttonObj != null)
            {
                Vector2 sz = _buttonObj.GetComponent<RectTransform>().sizeDelta;
                Vector2 position = new((pos.x + (sz.x / 2f)) / 1920f, (1080f - (pos.y + (sz.y / 2f))) / 1080f);
                _buttonObj.GetComponent<RectTransform>().anchorMin = position;
                _buttonObj.GetComponent<RectTransform>().anchorMax = position;
            }
        }

        public void SetRenderIndex(int idx)
        {
            _buttonObj.transform.SetSiblingIndex(idx);
        }

        public void SetTextColor(Color color)
        {
            if (_textObj != null)
            {
                Text t = _textObj.GetComponent<Text>();
                t.color = color;
            }
        }

        public void SetWidth(float width)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<RectTransform>().SetScaleX(width / _buttonObj.GetComponent<RectTransform>().sizeDelta.x);
            }
        }

        public void UpdateSprite(Texture2D tex, Rect bgSubSection)
        {
            if (_buttonObj != null)
            {
                _buttonObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(bgSubSection.x, tex.height - bgSubSection.height, bgSubSection.width, bgSubSection.height), Vector2.zero);
            }
        }

        public void UpdateText(string text)
        {
            if (_textObj != null)
            {
                _textObj.GetComponent<Text>().text = text;
            }
        }
    }
}