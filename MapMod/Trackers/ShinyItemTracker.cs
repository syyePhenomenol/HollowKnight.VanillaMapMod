using HutongGames.PlayMaker;
using SFCore.Utils;

namespace MapMod.Trackers
{
    public static class ShinyItemTracker
    {
		public static void Hook()
		{
            On.PlayMakerFSM.OnEnable += PlayMakerFSM_OnEnable;
		}

		private static void PlayMakerFSM_OnEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
			orig(self);

            if (self.FsmName == "Shiny Control")
            {
                foreach (FsmState state in self.FsmStates)
                {
                    MapMod.Instance.Log(state.Name);
                }
            }

            FsmUtil.AddAction(self, "Finish", new TrackShinyItem(self.gameObject.name));
            //         FsmUtil.AddAction(self, "Broken", new TrackShinyItem(self.gameObject.name));
        }

        private class TrackShinyItem : FsmStateAction
        {
            private readonly string _oName;
            public TrackShinyItem(string oName)
            {
                _oName = oName;
            }
            public override void OnEnter()
            {
                string scene = GameManager.instance.sceneName;
                
                    MapMod.LS.ObtainedItems[_oName + scene] = true;
                
                Finish();
            }
        }
    }
}
