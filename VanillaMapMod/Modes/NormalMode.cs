using MapChanger;

namespace VanillaMapMod;

internal class NormalMode : MapMode
{
    public override bool InitializeToThis()
    {
        return true;
    }

    public override float Priority => 10f;
    public override string Mod => nameof(VanillaMapMod);
    public override string ModeName => "Normal";
    public override bool? VanillaPins => VanillaMapMod.LS.VanillaPinsOn ? null : false;
    public override bool ImmediateMapUpdate => VanillaMapMod.GS.FastMapUpdate;
}
