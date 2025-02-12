using HomoLudens.Data;
using HomoLudens.Services.PersistentProgress;
//using HomoLudens.Core.Factory;
using System;
using UnityEngine;
using YG;
using System.Threading.Tasks;
using System.Collections;
//using YG.Insides;

namespace HomoLudens.Services.SaveLoad
{
    // This class save and load the player's progress for Yandex Games
    public class YGSaveLoadService //: ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        //private TaskCompletionSource<bool> _dataLoadedTCS;
        //private readonly IGameFactory _gameFactory;

        public YGSaveLoadService(IPersistentProgressService progressService
                                //, IGameFactory gameFactory
                                )
        {
            _progressService = progressService;
            //_dataLoadedTCS = new TaskCompletionSource<bool>();

#if UNITY_WEBGL
            YG2.onDefaultSaves += OnFirstGameSession;
            YG2.onGetSDKData += OnDataLoaded;
#endif
            //_gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            // update all writers, who want to update the player's progress
//            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
//                progressWriter.UpdateProgress(_progressService.Progress);

#if !UNITY_EDITOR//UNITY_WEBGL
            //YG2.saves += _progressService.Progress;
            YG2.saves.Progress = _progressService.Progress.DeepCopy();
            YG2.SaveProgress();
#elif UNITY_EDITOR
            // save the player's progress
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
            Debug.Log("Прогресс сохранён локально в PlayerPrefs.");
#endif
        }

        public void LoadProgressOrInitNew()
        {
#if !UNITY_EDITOR //UNITY_WEBGL
            if (YG2.isSDKEnabled)
            {
                if (YG2.isFirstGameSession)
                {
                    _progressService.Progress = NewProgress();
                    Debug.Log("Первый запуск - создано новое сохранение!");
                }
                else
                {
                    _progressService.Progress = LoadProgress() ?? throw new Exception("_progressService.Progress is NULL");//NewProgress();
                    Debug.Log("Сохранение загружено из YG2!");
                }
            }
#elif UNITY_EDITOR
            _progressService.Progress = LoadProgress() ?? NewProgress();
            Debug.Log("Локальное сохранение загружено или создано новое.");
#endif
        }

        public IEnumerator LoadProgressOrInitNewCoroutine()
        {
            bool isLoaded = false;

            void OnLoaded() => isLoaded = true;

            // Запускаем загрузку
            //YG2.GetData();

            // Ждём завершения загрузки
            yield return new WaitUntil(() => isLoaded);

            YG2.onGetSDKData -= OnLoaded;
            YG2.onDefaultSaves -= OnLoaded;

            // Обновляем прогресс
            if (YG2.isFirstGameSession)
            {
                _progressService.Progress = NewProgress();
                Debug.Log("Первый запуск - создано новое сохранение!");
            }
            else
            {
                _progressService.Progress = LoadProgress() ?? throw new Exception("_progressService.Progress is NULL");
                Debug.Log("Сохранение загружено из YG2!");
            }
        }

        public PlayerProgress LoadProgress()
        {
#if !UNITY_EDITOR//UNITY_WEBGL
            //return YG2.saves is SavesYG savedProgress ? (PlayerProgress)savedProgress : null;
            return YG2.saves?.Progress;
#elif UNITY_EDITOR
            return PlayerPrefs.GetString(ProgressKey)?
                    .ToDeserialized<PlayerProgress>();
#endif
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress();
            //return new PlayerProgress(initialLevel: MainScene);
        }

#if UNITY_WEBGL
        // Вызывается при старте игры при отсутствии сохранений,
        // то есть когда пользователь первый раз зашёл в игру.
        // Или если выполнить метод сброса сохранений SetDefaultSaves()
        private void OnFirstGameSession()
        {
            //_dataLoadedTCS.TrySetResult(true);
            _progressService.Progress = NewProgress();
        }

        // Событие onGetSDKData срабатывает при загрузке сохранений 
        // и при других обновлениях данных
        // Также, при после вызова метод сброса сохранений SetDefaultSaves()  
        // будет вызов onGetSDKData и мы обновим данные при сбросе сохранений
        private void OnDataLoaded()
        {
            //_dataLoadedTCS.TrySetResult(true);
            _progressService.Progress = LoadProgress();
        }

        public Task LoadProgressOrInitNewAsync()
        {
            throw new NotImplementedException();
        }
#endif

        // TODO: the method is not used
        /*        public async Task InitializeAsync()
                {
                    await _dataLoadedTCS.Task;
                    //LoadProgressOrInitNew();
                }*/
    }
}