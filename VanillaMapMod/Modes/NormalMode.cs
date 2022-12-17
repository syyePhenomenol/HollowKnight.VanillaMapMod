using MapChanger;

namespace VanillaMapMod
{
    internal class NormalMode : MapMode
    {
        public override bool InitializeToThis() => true;
        public override float Priority => 10f;
        public override string Mod => VanillaMapMod.MOD;
        public override string ModeName => "Normal";
        public override bool? VanillaPins => VanillaMapMod.LS.VanillaPinsOn ? null : false;
        public override bool ImmediateMapUpdate => true;
    }
}
