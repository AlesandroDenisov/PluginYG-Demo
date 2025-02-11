//using HomoLudens.Core.Factory;
using HomoLudens.Core;
using HomoLudens.Data;
using HomoLudens.Services.PersistentProgress;
using HomoLudens.Services.SaveLoad;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using YG;

public class YGSaveLoadAsyncService //: ISaveLoadAsyncService
{
    private const string ProgressKey = "Progress";
    private readonly IPersistentProgressService _progressService;
    private readonly ICoroutineRunner _coroutineRunner;
    //private readonly IGameFactory _gameFactory;

    //private TaskCompletionSource<bool> _dataLoadedTcs;
    //private TaskCompletionSource<bool> _firstSessionTcs;

    public YGSaveLoadAsyncService( IPersistentProgressService progressService
                                 , ICoroutineRunner coroutineRunner
                                 //, IGameFactory gameFactory
                                 )
    {
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
        //_gameFactory = gameFactory;

        //YG2.onDefaultSaves += OnFirstGameSession;
        //YG2.onGetSDKData += OnDataLoaded;
    }

    public void SaveProgress()
    {
        //foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        //    progressWriter.UpdateProgress(_progressService.Progress);

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        Debug.Log("Progress saved to PlayerPrefs");

        YG2.saves += _progressService.Progress;
        YG2.SaveProgress();
        Debug.Log("Progress saved to Yandex Storage");
    }

    public IEnumerator SaveProgressCoroutine()
    {
        yield return new WaitUntil(() => YG2.isSDKEnabled);

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        Debug.Log("Progress saved to PlayerPrefs");

        YG2.saves += _progressService.Progress;
        YG2.SaveProgress();
        Debug.Log("Progress saved to Yandex Storage");
    }

    public void LoadProgressOrInitNewAsync()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Для WebGL используем корутины c методами PluginYG2
            _coroutineRunner.StartCoroutine(LoadProgressCoroutine());
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            // Для Windows Unity Editor тоже используем корутины c методами PluginYG2
            _coroutineRunner.StartCoroutine(LoadProgressCoroutine());
        }
        else
        {
            _progressService.Progress = LoadProgress() ?? NewProgress();
/*            Debug.Log($"Created PlayerProgress with CurrentLevel = {_progressService.Progress.CurrentLevel}," +
                        $"CurrentLoadedLevel = {_progressService.Progress.CurrentLoadedLevel}," +
                        $"MaxUnlockedLevel = {_progressService.Progress.MaxUnlockedLevel}," +
                        $"DeathCount = {_progressService.Progress.DeathCount}," +
                        $"Collectables = {_progressService.Progress.Collectables}");*/
        }
    }

    public IEnumerator LoadProgressCoroutine()
    {
        yield return new WaitUntil(() => YG2.isSDKEnabled);

        Debug.Log($"YG2.isFirstGameSession = {YG2.isFirstGameSession}");

        if (YG2.isFirstGameSession)
        {
            _progressService.Progress = NewProgress();
            Debug.Log("First launch - new save created!");
        }
        else
        {
            yield return new WaitUntil(() => YG2.saves != null);
            _progressService.Progress = YG2.saves is SavesYG savedProgress ? (PlayerProgress)savedProgress : NewProgress();
            Debug.Log("SaveGame loaded or new created from YG2!");
        }

/*        Debug.Log($"Created PlayerProgress with CurrentLevel = {_progressService.Progress.CurrentLevel}," +
                    $"CurrentLoadedLevel = {_progressService.Progress.CurrentLoadedLevel}," +
                    $"MaxUnlockedLevel = {_progressService.Progress.MaxUnlockedLevel}," +
                    $"DeathCount = {_progressService.Progress.DeathCount}," +
                    $"Collectables = {_progressService.Progress.Collectables}");*/
    }

    private PlayerProgress NewProgress()
    {
        return new PlayerProgress();
    }

/*    public void ResetProgress()
    {
        YG2.SetDefaultSaves();
    }*/

    public PlayerProgress LoadProgress()
    {
        return PlayerPrefs.GetString(ProgressKey)?
            .ToDeserialized<PlayerProgress>();
    }

    public void LoadProgressOrInitNew()
    {
        Debug.Log($"Mock LoadProgressOrInitNew()");

    }
}
