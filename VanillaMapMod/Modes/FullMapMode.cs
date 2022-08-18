using MapChanger;

namespace VanillaMapMod
{
    internal class FullMapMode : MapMode
    {
        public override string Mod => VanillaMapMod.MOD;
        public override string ModeName => "Full Map";
        public override bool ForceHasMap => true;
        public override bool ForceHasQuill => true;
        public override OverrideType VanillaPins
        {
            get
            {
                if (VanillaMapMod.LS.VanillaPinsOn)
                {
                    return OverrideType.ForceOn;
                }
                else
                {
                    return OverrideType.ForceOff;
                }
            }
        }
        public override OverrideType MapMarkers => OverrideType.ForceOff;
        public override bool ImmediateMapUpdate => true;
        public override bool FullMap => true;
    }
}
