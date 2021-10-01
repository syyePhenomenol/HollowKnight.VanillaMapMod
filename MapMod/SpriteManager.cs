using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Modding.Logger;

// Code borrowed from homothety: https://github.com/homothetyhk/RandomizerMod/
namespace MapMod
{
    internal static class SpriteManager
    {
        public enum PinStyles
        {
            Normal,
            Q_Marks_1,
            Q_Marks_2,
            Q_Marks_3,
        }

        private static Dictionary<string, Sprite> _sprites;

        public static void LoadEmbeddedPngs(string prefix)
        {
            Assembly a = typeof(SpriteManager).Assembly;
            _sprites = new Dictionary<string, Sprite>();

            foreach (string name in a.GetManifestResourceNames().Where(name => name.Substring(name.Length - 3).ToLower() == "png"))
            {
                string altName = prefix != null ? name.Substring(prefix.Length) : name;
                altName = altName.Remove(altName.Length - 4);
                altName = altName.Replace(".", "");
                Sprite sprite = FromStream(a.GetManifestResourceStream(name));
                _sprites[altName] = sprite;

                Logger.Log(altName);
            }
        }

        public static Sprite GetSpriteFromPool(string pool)
        {
            string spriteName = pool switch
            {
                "Skill" => "pinSkill",
                "Charm" => "pinCharm",
                "Key" => "pinKey",
                "Mask" => "pinMask",
                "Vessel" => "pinVessel",
                "Notch" => "pinNotch",
                "Ore" => "pinOre",
                "Geo" => "pinGeo",
                "Relic" => "pinRelic",
                "EssenceBoss" => "pinEssenceBoss",
                "Map" => "pinMap",
                "Rock" => "pinRock",
                "Soul" => "pinTotem",
                "Lore" => "pinLore",
                "Shop" => "pinShop",
                _ => "pinUnknown",
            };

            return GetSprite(spriteName);
        }

        public static Sprite GetSprite(string name)
        {
            if (_sprites.TryGetValue(name, out Sprite sprite))
            {
                return sprite;
            }

            Logger.Log("Failed to load sprite named '" + name + "'");
            return null;
            //return _sprites != null && _sprites.TryGetValue(name, out Sprite sprite)
            //    ? sprite
            //    : FromStream(typeof(SpriteManager).Assembly.GetManifestResourceStream(name));
        }

        private static Sprite FromStream(Stream s)
        {
            Texture2D tex = new(1, 1, TextureFormat.RGBA32, true);
            byte[] buffer = ToArray(s);
            _ = tex.LoadImage(buffer, markNonReadable: true);
            tex.filterMode = FilterMode.Bilinear;
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }

        private static byte[] ToArray(Stream s)
        {
            using MemoryStream ms = new();
            s.CopyTo(ms);
            return ms.ToArray();
        }
    }
}