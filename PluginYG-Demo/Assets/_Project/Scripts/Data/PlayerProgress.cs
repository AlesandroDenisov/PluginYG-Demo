using System;
using UnityEngine;

//using UnityEngine;
using YG;

namespace HomoLudens.Data
{
    [Serializable]
    public class PlayerProgress
    {
        //public WorldData WorldData;
        //public ResourceData ResourceData;

       public int CurrentLevel { get; private set; }

        public PlayerProgress()
        {
            CurrentLevel = 0;
        }

        public PlayerProgress(int level)
        {
            CurrentLevel = level;
        }

        public void SetCurrentLevel(int level) => CurrentLevel = level;

        public static explicit operator PlayerProgress(SavesYG saves)
        {
            if (saves == null)
            {
                throw new ArgumentNullException(nameof(saves), "SavesYG object cannot be null.");
            }

            return new PlayerProgress{
                CurrentLevel = saves.CurrentLevel
            };
        }

    /* 
            // Player state
            public int CurrentLoadedLevel;
            public int MaxUnlockedLevel = 0;
            public int HighScore = 0;

            public int DeathCount = 0;
            public int Collectables = 0;

            public bool IsDemo = false;

            public PlayerStats PlayerStats;

            //public GameObject[] m_Diamonds;

            // Music state
            public bool m_IsIngameMusicPlaying;
            public float MusicVolume;
            public float EffectsVolume;

            public PlayerProgress()
            {
                //WorldData = new WorldData(initialLevel);
                //ResourceData = new ResourceData();

                CurrentLoadedLevel = 0;
                MaxUnlockedLevel = 0;
                HighScore = 0;
                DeathCount = 0;
                Collectables = 0;

                PlayerStats = new PlayerStats();
            }

            public static explicit operator PlayerProgress(SavesYG savesYG) =>
                new PlayerProgress()
                {
                    CurrentLevel = savesYG.CurrentLevel,
                    MaxUnlockedLevel = savesYG.MaxUnlockedLevel,
                    HighScore = savesYG.HighScore,
                    DeathCount = savesYG.DeathCount,
                    Collectables = savesYG.Collectables
                };

            public void SetCurrentLevel(int level) => CurrentLevel = level;
    */
}
}