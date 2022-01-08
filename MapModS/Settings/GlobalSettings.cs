namespace MapModS.Settings
{
    public class GlobalSettings
    {
        public enum PinSize
        {
            small,
            medium,
            large
        }

        //public float PinScaleSize = 0.36f;

        public PinSize PinSizeSetting = PinSize.medium;

        public void TogglePinSize()
        {
            switch (PinSizeSetting)
            {
                case PinSize.small:
                case PinSize.medium:
                    PinSizeSetting += 1;
                    break;
                default:
                    PinSizeSetting = PinSize.small;
                    break;
            }
        }
    }
}