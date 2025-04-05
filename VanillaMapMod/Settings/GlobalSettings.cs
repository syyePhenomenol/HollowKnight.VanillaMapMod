using System;
using Newtonsoft.Json;

namespace VanillaMapMod.Settings;

public class GlobalSettings
{
    [JsonProperty]
    public PinSize PinSize { get; private set; } = PinSize.Medium;

    [JsonProperty]
    public PinShape PinShape { get; private set; } = PinShape.Circle;

    internal void TogglePinSize()
    {
        PinSize = (PinSize)(((int)PinSize + 1) % Enum.GetNames(typeof(PinSize)).Length);
    }

    internal void TogglePinShape()
    {
        PinShape = (PinShape)(((int)PinShape + 1) % Enum.GetNames(typeof(PinShape)).Length);
    }
}
