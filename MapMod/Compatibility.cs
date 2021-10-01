using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMod
{
    public class Compatibility
    {
        public static Type AMapsType;

        public static bool CheckAMapsInstalled()
        {
            AMapsType = Type.GetType("AdditionalMaps.AdditionalMaps, AdditionalMaps");

            if (AMapsType != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
