﻿using MapChanger;
using MapChanger.MonoBehaviours;

namespace VanillaMapMod
{
    internal class FullMapMode : MapMode
    {
        public override string Mod => VanillaMapMod.MOD;
        public override string ModeName => "Full Map";
        public override bool ForceHasMap => true;
        public override bool ForceHasQuill => true;
        public override bool? VanillaPins => VanillaMapMod.LS.VanillaPinsOn;
        public override bool ImmediateMapUpdate => true;
        public override bool FullMap => true;
        public override bool? NextAreaNameActiveOverride(NextAreaName nextAreaName) => true;
        public override bool? NextAreaArrowActiveOverride(NextAreaArrow nextAreaArrow) => true;
    }
}
