using System;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MapChanger.UI;

namespace VanillaMapMod
{
    internal class PoolsPanel : ExtraButtonPanel
    {
        internal static PoolsPanel Instance { get; private set; }

        public PoolsPanel() : base("Pools Panel", "VanillaMapMod", 295f, 10)
        {
            Instance = this;
        }

        protected override void MakeButtons()
        {
            foreach (PoolGroup poolGroup in Enum.GetValues(typeof(PoolGroup)).Cast<PoolGroup>())
            {
                if (poolGroup is PoolGroup.Other) continue;

                PoolButton poolButton = new(poolGroup);
                poolButton.Make();
                ExtraButtonsGrid.Children.Add(poolButton.Button);
                ExtraButtons.Add(poolButton);
            }
        }
    }
}
