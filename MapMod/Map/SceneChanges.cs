using UnityEngine.SceneManagement;

namespace MapMod.Map
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
                PlayerData.instance.scenesVisited.Add(to.name);
                PlayerData.instance.scenesMapped.Add(to.name);
            }
		}
	}
}
