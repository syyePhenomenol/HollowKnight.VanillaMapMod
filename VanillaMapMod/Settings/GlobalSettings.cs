using System;

namespace VanillaMapMod.Settings
{
    public class GlobalSettings
    {
        public PinSize PinSize = PinSize.Medium;
        public PinShape PinShape = PinShape.Circle;
        
        internal void TogglePinSize()
        {
            PinSize = (PinSize)(((int)PinSize + 1) % Enum.GetNames(typeof(PinSize)).Length);
        }

        internal void TogglePinShape()
        {
            PinShape = (PinShape)(((int)PinShape + 1) % Enum.GetNames(typeof(PinShape)).Length);
        }
    }
}