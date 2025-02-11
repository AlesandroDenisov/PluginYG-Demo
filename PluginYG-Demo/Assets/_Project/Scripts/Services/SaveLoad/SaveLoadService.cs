//using IdleArcade.Core.Factory;
using HomoLudens.Core;
using HomoLudens.Data;
using HomoLudens.Services.PersistentProgress;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using YG;
using YG.Insides;

namespace HomoLudens.Services.SaveLoad
{
    // This class save and load the player's progress
    public class SaveLoadService : ISaveLoadService
    {
        private bool _isDataLoaded;

        private IPersistentProgressService _progressService;
        private ICoroutineRunner _coroutineRunner;

        public SaveLoadService( IPersistentProgressService progressService
                              , ICoroutineRunner coroutineRunner
                             //, IGameFactory gameFactory
                             )
        {
            //_progressService = progressService;
            _coroutineRunner = coroutineRunner;
        }

        public void LoadProgressOrInitNew(uint waitingTimeSeconds, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(WaitingLoadProgress(waitingTimeSeconds, onLoaded));
        }

        public IEnumerator WaitingLoadProgress(uint waitingTimeSeconds, Action onLoaded = null)
        {
            if (YG2.isSDKEnabled)
            {
                onLoaded?.Invoke();
                yield break;
            }

            float elapsedTime = 0f;
            while (!_isDataLoaded && elapsedTime < waitingTimeSeconds)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                LoadProgress(onLoaded);
            }

            if (_isDataLoaded)
            {
                onLoaded?.Invoke();
                Debug.Log("[SaveLoadService] Данные успешно загружены!");
            }
            else
            {
                NewProgress();
                onLoaded?.Invoke();
                Debug.Log("[SaveLoadService] Истекло время ожидания. Данные не были загружены. Создан новый прогресс!");
            }
        }

        private void NewProgress()
        {
            PlayerProgress progress = new PlayerProgress();
            YG2.saves = (SavesYG)progress;
        }

        public void LoadProgress(Action onLoaded = null)
        {
            if (_isDataLoaded) return;

            YGInsides.LoadProgress();

            Debug.Log(YG2.saves.ToJson());

            _isDataLoaded = true;
            //onLoaded?.Invoke();
        }

        // TODO: move the progress to the separate service
        public void SaveProgress(PlayerProgress progress)
        {
            //YG2.saves += _progressService.Progress;
            YG2.saves += progress;

            YG2.SaveProgress();
            Debug.Log($"[SaveProgress] Progress saved!");

            // TODO: добавить сохранение в PlayerPrefs
        }

        public void ResetProgress(PlayerProgress progress)
        {
            YG2.SetDefaultSaves();
            progress = (PlayerProgress)YG2.saves;
            YG2.SaveProgress();
            Debug.Log("[ResetProgress] Progress was reset!");
        }
    }
}
