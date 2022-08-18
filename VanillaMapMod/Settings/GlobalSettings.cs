using System;

namespace VanillaMapMod.Settings
{
    public class GlobalSettings
    {
        public PinSize PinSize = PinSize.Medium;
        internal void TogglePinSize()
        {
            PinSize = (PinSize)(((int)PinSize + 1) % Enum.GetNames(typeof(PinSize)).Length);
            VmmPinManager.UpdatePinSize();
        }
    }
}