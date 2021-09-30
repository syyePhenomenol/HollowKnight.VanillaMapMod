using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Modding;
using MapMod.Settings;


// Code heavily borrowed from homothetyhk: https://github.com/homothetyhk/RandomizerMod/
namespace MapMod
{
    public class MapMod : Mod, IGlobalSettings<GlobalSettings>
    {
        private readonly string _version = $"PRERELEASE: {GetSHA1()}";
        public override string GetVersion()
        {
            return _version;
        }

        public MapMod() : base("Vanilla Map Mod") { }

        public override void Initialize()
        {
            base.Initialize();
            
            Log("Initialization complete.");
        }

        public static string GetSHA1()
        {
            using System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            using FileStream sr = File.OpenRead(Location);
            return Convert.ToBase64String(sha1.ComputeHash(sr));
        }

        public static GlobalSettings GS { get; private set; } = new();

        public void OnLoadGlobal(GlobalSettings s)
        {
            GS = s;
        }

        public GlobalSettings OnSaveGlobal()
        {
            return GS;
        }

        public static string Folder { get; }
        public static string Location { get; }
        public static Assembly Assembly { get; }

        static MapMod()
        {
            Assembly = typeof(MapMod).Assembly;
            Location = Assembly.Location;
            Folder = Path.GetDirectoryName(Location);
        }
    }

    
}
