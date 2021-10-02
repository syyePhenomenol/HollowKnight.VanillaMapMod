using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using SFCore.Generics;

namespace MapMod
{
	public static class SceneChange
	{
		public static void Hook()
		{
			UnityEngine.SceneManagement.SceneManager.activeSceneChanged += HandleSceneChanges;
		}

		private static void HandleSceneChanges(Scene from, Scene to)
		{
			if (to.name == "Quit_To_Menu")
			{
				
			}
		}
	}
}
