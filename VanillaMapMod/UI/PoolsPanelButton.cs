using MagicUI.Elements;
using MapChanger.UI;

namespace VanillaMapMod
{
    internal class PoolsPanelButton : MainButton
    {
        public PoolsPanelButton() : base("Pools Panel Button", "VanillaMapMod", 0, 3)
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
