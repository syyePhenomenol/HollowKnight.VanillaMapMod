using GlobalEnums;

namespace MapModS.Data
{
    public class PinDef
    {
        public string name;

        // The list of objects that when all found, the pin will disappear
        public string[] objectName;

        // The name of the scene in which the objects are
        public string sceneName;

        // The name of the scene the pin belongs to, an override to sceneName in some cases
        public string pinScene;

        // The map area/zone the pin belongs to
        public MapZone mapZone;

        // The pool/group the pin belongs to
        public Pool pool;

        // The local offset of the pin relative to its pinScene/sceneName map object
        public float offsetX;
        public float offsetY;
        public float offsetZ;

        // 0 = not affected by AdditionalMaps, 1 = show without, 2 = show with
        public int additionalMaps;
    }
}