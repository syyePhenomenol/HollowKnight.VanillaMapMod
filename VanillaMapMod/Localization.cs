using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MapChanger;

namespace VanillaMapMod;

internal static class Localization
{
    private static Dictionary<string, string> _localization;

    internal static void Load()
    {
        try
        {
            _localization = JsonUtil.DeserializeFromExternalFile<Dictionary<string, string>>(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "language.json")
            );
            MapChanger.Localization.AddLocalizer(Localize);
        }
        catch (Exception)
        {
            VanillaMapMod.Instance.LogDebug($"No valid \"language.json\" file found");
        }
    }

    internal static string Localize(string t)
    {
        if (_localization is not null && _localization.TryGetValue(t, out var result))
        {
            return result;
        }

        return null;
    }
}
