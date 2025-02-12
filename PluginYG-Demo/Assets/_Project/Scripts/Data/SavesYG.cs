using HomoLudens.Data;
using System;
using UnityEngine;

namespace YG
{
    [Serializable]
    public partial class SavesYG
    {
        public int idSave;

        [field: SerializeField] public PlayerProgress Progress = new();

/*        public int CurrentLevel = 0;
        
        // Example: YG2.saves += _progressService.Progress;
        public static SavesYG operator +(SavesYG savesYG, PlayerProgress playerProgress)
        {
            savesYG.CurrentLevel = playerProgress.CurrentLevel;
            return savesYG;
        }

        public static explicit operator SavesYG(PlayerProgress playerProgress)
        {
            if (playerProgress == null)
            {
                throw new ArgumentNullException(nameof(playerProgress), "PlayerProgress object cannot be null.");
            }

            return new SavesYG {
                CurrentLevel = playerProgress.CurrentLevel
            };
        }*/
    }
}

/*        
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
        }*/
