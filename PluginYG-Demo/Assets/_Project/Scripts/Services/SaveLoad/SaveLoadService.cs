using HomoLudens.Data;
//using IdleArcade.Core.Factory;
using HomoLudens.Services.PersistentProgress;
using UnityEngine;

namespace HomoLudens.Services.SaveLoad
{
    // This class save and load the player's progress
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
    
        private readonly IPersistentProgressService _progressService;
        //private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService)//, IGameFactory gameFactory)
        {
            _progressService = progressService;
            //_gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            // update all writers, who want to update the player's progress
            //foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            //    progressWriter.UpdateProgress(_progressService.Progress);
            
            // save the player's progress
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                    .ToDeserialized<PlayerProgress>();
        }

        public void LoadProgressOrInitNew()
        {
            throw new System.NotImplementedException();
        }
    }
}