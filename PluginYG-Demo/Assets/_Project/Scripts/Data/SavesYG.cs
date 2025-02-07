using HomoLudens.Data;

namespace YG
{
    public partial class SavesYG
    {
        public int CurrentLevel = 0;
        public int MaxUnlockedLevel = 0;
        public int HighScore = 0;
        public int DeathCount = 0;
        public int Collectables = 0;

        public static SavesYG operator + (SavesYG savesYG, PlayerProgress playerProgress)
        {
            savesYG.CurrentLevel = playerProgress.CurrentLoadedLevel;
            savesYG.MaxUnlockedLevel = playerProgress.MaxUnlockedLevel;
            savesYG.HighScore = playerProgress.HighScore;
            savesYG.DeathCount = playerProgress.DeathCount;
            savesYG.Collectables = playerProgress.Collectables;

            return savesYG;
        }


        /*        public List<ItemData> items = new List<ItemData>();

                public ItemData GetItem(string name)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].name == name)
                            return items[i];
                    }

                    ItemData newItem = new ItemData
                    {
                        name = name,
                        position = Vector3.zero
                    };

                    return newItem;
                }*/
    }
}
