using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Code borrowed from homothety: https://github.com/homothetyhk/RandomizerMod/
namespace VanillaMapMod.Map
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

                VanillaMapMod.Instance.Log(altName);
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
                "Egg" => "pinEgg",
                "Relic" => "pinRelic",
                "Root" => "pinRoot",
                "EssenceBoss" => "pinEssenceBoss",
                "Grub" => "pinGrub",
                "Map" => "pinMap",
                "Stag" => "pinStag",
                "Cocoon" => "pinCocoon",
                "Rock" => "pinRock",
                "Totem" => "pinTotem",
                "Lore" => "pinLore",
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

            VanillaMapMod.Instance.Log("Failed to load sprite named '" + name + "'");
            return null;
        }

        private static Sprite FromStream(Stream s)
        {
            Texture2D tex = new(1, 1);
            byte[] buffer = ToArray(s);
            _ = tex.LoadImage(buffer, markNonReadable: true);
            tex.filterMode = FilterMode.Bilinear;
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 55);
        }

        private static byte[] ToArray(Stream s)
        {
            using MemoryStream ms = new();
            s.CopyTo(ms);
            return ms.ToArray();
        }
    }
}