using Modding;
using UnityEngine.SceneManagement;

namespace MapModS.Map
{
    public static class Quill
    {
        public static void Hook()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
            ModHooks.SetPlayerBoolHook += BoolSetOverride;
        }

        // Force map to update every time we enter a new scene
        private static void HandleSceneChanges(Scene from, Scene to)
        {
            if (GameManager.instance.sceneName != to.name) return;

            if (!PlayerData.instance.scenesVisited.Contains(to.name))
            {
                PlayerData.instance.scenesVisited.Add(to.name);
            }

            if (!PlayerData.instance.hasQuill) return;

            foreach (string scene in PlayerData.instance.scenesVisited)
            {
                if (!PlayerData.instance.scenesMapped.Contains(scene))
                {
                    PlayerData.instance.scenesMapped.Add(scene);
                }
            }
        }

        private static bool BoolSetOverride(string boolName, bool orig)
        {
            if (!(boolName == "hasQuill" && orig)) return orig;

            // Immediately update map with visited areas when quill is picked up, to avoid wasting time at bench
            foreach (string scene in PlayerData.instance.scenesVisited)
            {
                if (!PlayerData.instance.scenesMapped.Contains(scene))
                {
                    PlayerData.instance.scenesMapped.Add(scene);
                }
            }

            return orig;
        }
    }
}