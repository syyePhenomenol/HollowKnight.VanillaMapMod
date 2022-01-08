using UnityEngine;
using UnityEngine.UI;

namespace MapModS.CanvasUtil
{
    // Code borrowed from homothety
    public class CanvasImage
    {
        public bool Active;
        private readonly GameObject _imageObj;
        private Rect _sub;
        private Vector2 _sz;

        public CanvasImage(GameObject parent, Texture2D tex, Vector2 pos, Vector2 size, Rect subSprite)
        {
            if (size.x == 0 || size.y == 0)
            {
                size = new Vector2(subSprite.width, subSprite.height);
            }

            _sz = size;
            _sub = subSprite;

            _imageObj = new GameObject();
            _imageObj.AddComponent<CanvasRenderer>();
            RectTransform imageTransform = _imageObj.AddComponent<RectTransform>();
            imageTransform.sizeDelta = new Vector2(subSprite.width, subSprite.height);
            _imageObj.AddComponent<Image>().sprite = Sprite.Create(tex, new Rect(subSprite.x, tex.height - subSprite.height, subSprite.width, subSprite.height), Vector2.zero);

            CanvasGroup group = _imageObj.AddComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            _imageObj.transform.SetParent(parent.transform, false);

            Vector2 position = new((pos.x + (size.x / subSprite.width * subSprite.width / 2f)) / 1920f, (1080f - (pos.y + size.y / subSprite.height * subSprite.height / 2f)) / 1080f);
            imageTransform.anchorMin = position;
            imageTransform.anchorMax = position;
            imageTransform.SetScaleX(size.x / subSprite.width);
            imageTransform.SetScaleY(size.y / subSprite.height);

            GameObject.DontDestroyOnLoad(_imageObj);

            Active = true;
        }

        public void Destroy()
        {
            GameObject.Destroy(_imageObj); ;
        }

        public void SetActive(bool b)
        {
            if (_imageObj != null)
            {
                _imageObj.SetActive(b);
                Active = b;
            }
        }

        public void SetHeight(float height)
        {
            if (_imageObj != null)
            {
                _sz = new Vector2(_sz.x, height);
                _imageObj.GetComponent<RectTransform>().SetScaleY(height / _imageObj.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            if (_imageObj != null)
            {
                Vector2 position = new((pos.x + (_sz.x / _sub.width * _sub.width / 2f)) / 1920f, (1080f - (pos.y + ((_sz.y / _sub.height) * _sub.height) / 2f)) / 1080f);
                _imageObj.GetComponent<RectTransform>().anchorMin = position;
                _imageObj.GetComponent<RectTransform>().anchorMax = position;
            }
        }

        public void SetRenderIndex(int idx)
        {
            _imageObj.transform.SetSiblingIndex(idx);
        }

        public void SetWidth(float width)
        {
            if (_imageObj != null)
            {
                _sz = new Vector2(width, _sz.y);
                _imageObj.GetComponent<RectTransform>().SetScaleX(width / _imageObj.GetComponent<RectTransform>().sizeDelta.x);
            }
        }

        public void UpdateImage(Texture2D tex, Rect subSection)
        {
            if (_imageObj != null)
            {
                _imageObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(subSection.x, tex.height - subSection.height, subSection.width, subSection.height), Vector2.zero);
            }
        }
    }
}