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
            _progressService = progressService;
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
                //_progressService.Progress = (PlayerProgress)YG2.saves;
                _progressService.Progress = YG2.saves.Progress.DeepCopy();
                onLoaded?.Invoke();
                Debug.Log("[SaveLoadService.WaitingLoadProgress()] Данные успешно загружены!");
                Debug.Log($"[SaveLoadService.WaitingLoadProgress()] YG2.saves.Progress = {YG2.saves.Progress.ToJson()}");
                Debug.Log($"[SaveLoadService.WaitingLoadProgress()] _progressService.Progress = {_progressService.Progress.ToJson()}");

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
            _progressService.Progress = progress;
            //YG2.saves = (SavesYG)progress;
            YG2.saves.Progress = progress.DeepCopy();
        }

        public void LoadProgress(Action onLoaded = null)
        {
            if (_isDataLoaded) return;

            YGInsides.LoadProgress();

            Debug.Log(YG2.saves.Progress.ToJson());

            _isDataLoaded = true;
            //onLoaded?.Invoke();
        }

        public void SaveProgress()
        {
            //YG2.saves += progress;
            //YG2.saves += _progressService.Progress;
            YG2.saves.Progress = _progressService.Progress.DeepCopy();

            Debug.Log($"[SaveLoadService.SaveProgress()] YG2.saves.Progress = {YG2.saves.Progress.ToJson()}");
            Debug.Log($"[SaveLoadService.SaveProgress()] _progressService.Progress = {_progressService.Progress.ToJson()}");

            YG2.SaveProgress();
            Debug.Log($"[SaveProgress] Progress saved!");

            // TODO: добавить сохранение в PlayerPrefs
        }

        public void ResetProgress()
        {
            YG2.SetDefaultSaves();
            //progress = (PlayerProgress)YG2.saves;
            //_progressService.Progress = (PlayerProgress)YG2.saves;
            _progressService.Progress = YG2.saves.Progress.DeepCopy();

            YG2.SaveProgress();
            Debug.Log("[ResetProgress] Progress was reset!");
        }
    }
}
