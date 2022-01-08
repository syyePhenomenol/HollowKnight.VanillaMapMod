using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MapModS.CanvasUtil
{
    // Code borrowed from homothety
    public class CanvasText
    {
        private readonly Vector2 _size;
        private readonly GameObject _textObj;
        private bool _active;

        public CanvasText(GameObject parent, Vector2 pos, Vector2 sz, Font font, string text, int fontSize = 13, FontStyle style = FontStyle.Normal, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            if (Mathf.Abs(sz.x) < Mathf.Epsilon || Mathf.Abs(sz.y) < Mathf.Epsilon)
            {
                _size = new Vector2(1920f, 1080f);
            }
            else
            {
                _size = sz;
            }

            _textObj = new GameObject();
            _textObj.AddComponent<CanvasRenderer>();
            RectTransform textTransform = _textObj.AddComponent<RectTransform>();
            textTransform.sizeDelta = _size;

            CanvasGroup group = _textObj.AddComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            Text t = _textObj.AddComponent<Text>();
            t.text = text;
            t.font = font;
            t.fontSize = fontSize;
            t.fontStyle = style;
            t.alignment = alignment;

            _textObj.transform.SetParent(parent.transform, false);

            Vector2 position = new((pos.x + _size.x / 2f) / 1920f, (1080f - (pos.y + _size.y / 2f)) / 1080f);
            textTransform.anchorMin = position;
            textTransform.anchorMax = position;

            Object.DontDestroyOnLoad(_textObj);

            _active = true;
        }

        public void Destroy()
        {
            Object.Destroy(_textObj);
        }

        public Vector2 GetPosition()
        {
            if (_textObj != null)
            {
                Vector2 anchor = _textObj.GetComponent<RectTransform>().anchorMin;

                return new Vector2(anchor.x * 1920f - _size.x / 2f, 1080f - anchor.y * 1080f - _size.y / 2f);
            }

            return Vector2.zero;
        }

        public void MoveToTop()
        {
            if (_textObj != null)
            {
                _textObj.transform.SetAsLastSibling();
            }
        }

        public void SetActive(bool a)
        {
            _active = a;

            if (_textObj != null)
            {
                _textObj.SetActive(_active);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            if (_textObj != null)
            {
                RectTransform textTransform = _textObj.GetComponent<RectTransform>();

                Vector2 position = new((pos.x + _size.x / 2f) / 1920f, (1080f - (pos.y + _size.y / 2f)) / 1080f);
                textTransform.anchorMin = position;
                textTransform.anchorMax = position;
            }
        }

        public void SetTextColor(Color color)
        {
            if (_textObj != null)
            {
                Text t = _textObj.GetComponent<Text>();
                t.color = color;
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