using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


// Currently AdditionalMaps has a bug when reopening a loaded game, so I disabled compatibility for now
namespace MapMod
{
    public static class Compatibility
    {
        public static bool AdditionalMapsInstalled()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == "AdditionalMaps")

                        foreach (FieldInfo field in type.GetFields())
                        {
                            MapMod.Instance.Log(field.Name);
                        }

                    return true;
                }
            }

            return false;
        }

        public static bool HasWhitePalaceMap()
        {
            GameObject go_gameMap = GameObject.Find("Game_Map(Clone)");

            if (go_gameMap != null)
            {
                MapMod.Instance.Log("gameMap object found");

                Transform whitePalace = go_gameMap.transform.Find("WHITE_PALACE");

                if (whitePalace != null)
                {
                    MapMod.Instance.Log("White Palace exists");

                    if (whitePalace.gameObject.activeSelf)
                    {
                        MapMod.Instance.Log("White Palace map is unlocked");

                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasGodhomeMap()
        {
            return false;
        }
    }
}
