using MagicUI.Elements;
using MapChanger.UI;

namespace VanillaMapMod
{
    internal class PoolsPanelButton : MainButton
    {
        internal static PoolsPanelButton Instance { get; private set; }

        public PoolsPanelButton() : base("Pools Panel Button", "VanillaMapMod", 1, 2)
        {

        }

        protected override void OnClick()
        {
            PoolsPanel.Instance.Toggle();
        }

        public override void Update()
        {
            base.Update();

            Button.Content = "Customize\nPins";
        }
    }
}
