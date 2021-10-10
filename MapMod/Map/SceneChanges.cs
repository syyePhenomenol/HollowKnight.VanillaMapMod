using UnityEngine.SceneManagement;

namespace VanillaMapMod.Map
{
    public static class SceneChanges
    {
        public static void Hook()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
        }

        private static void HandleSceneChanges(Scene from, Scene to)
        {
            if (GameManager.instance.sceneName == to.name && PlayerData.instance.hasQuill)
            {
                if (!PlayerData.instance.scenesVisited.Contains(to.name))
                {
                    PlayerData.instance.scenesVisited.Add(to.name);
                }

                if (!PlayerData.instance.scenesMapped.Contains(to.name))
                {
                    PlayerData.instance.scenesMapped.Add(to.name);
                }
            }
        }
    }
}