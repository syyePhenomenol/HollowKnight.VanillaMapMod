using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MapModS.Data;

// Code borrowed from homothety: https://github.com/homothetyhk/RandomizerMod/
namespace MapModS.Map
{
    internal static class SpriteManager
    {
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

                MapModS.Instance.Log(altName);
            }
        }
        public static Sprite GetSpriteFromPool(Pool pool)
        {
            string spriteName = pool switch
            {
                Pool.Charm => "pinCharm",
                Pool.Cocoon => "pinCocoon",
                Pool.Egg => "pinEgg",
                Pool.EssenceBoss => "pinEssenceBoss",
                Pool.Geo => "pinGeo",
                Pool.Grub => "pinGrub",
                Pool.Key => "pinKey",
                Pool.Lore => "pinLore",
                Pool.Map => "pinMap",
                Pool.Mask => "pinMask",
                Pool.Notch => "pinNotch",
                Pool.Ore => "pinOre",
                Pool.Relic => "pinRelic",
                Pool.Rock => "pinRock",
                Pool.Root => "pinRoot",
                Pool.Skill => "pinSkill",
                Pool.Stag => "pinStag",
                Pool.Totem => "pinTotem",
                Pool.Vessel => "pinVessel",
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

            MapModS.Instance.LogWarn("Failed to load sprite named '" + name + "'");
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