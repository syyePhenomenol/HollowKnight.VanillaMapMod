namespace MapModS.Data
{
    public class ShopDef
    {
        // The PlayerData setting that is read to determine if the item is in the shop,
        // and also to write to when the item is bought
        public string playerDataBoolName;

        // The displayed name of the item (as a Language reference)
        public string nameConvo;

        // The displayed description of the item (as a Language reference)
        public string descConvo;

        // Geo cost of the item
        public int cost;

        // Name of the sprite to display in the Menu
        public string spriteName;
    }
}