using UnityEngine.SceneManagement;

namespace VanillaMapMod.Map
{
    public static class SceneChanges
    {
        public static void Hook()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
        }

        // Force map to update every time we enter a new scene
        private static void HandleSceneChanges(Scene from, Scene to)
        {
            if (GameManager.instance.sceneName == to.name)
            {
                if (!PlayerData.instance.scenesVisited.Contains(to.name))
                {
                    PlayerData.instance.scenesVisited.Add(to.name);
                }

                if (PlayerData.instance.hasQuill)
                {
                    foreach (string scene in PlayerData.instance.scenesVisited)
                    {
                        if (!PlayerData.instance.scenesMapped.Contains(scene))
                        {
                            PlayerData.instance.scenesMapped.Add(scene);
                        }
                    }
                }

                //if (!PlayerData.instance.scenesMapped.Contains(to.name) && PlayerData.instance.hasQuill)
                //{
                //    PlayerData.instance.scenesMapped.Add(to.name);
                //}
            }
        }
    }
}