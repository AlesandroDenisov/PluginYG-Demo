using HomoLudens.Data;
using HomoLudens.Services;
using HomoLudens.Services.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;

// [Test2] Save and load by PluginYG2 with ISaveLoadService
public class SaveLoadAsync : MonoBehaviour
{
    public Button AddLevelButton;
    public Button SaveLevelButton;
    public Button LoadLevelButton;
    public Button ResetLevelButton;

    private int _currentLevel = 0;

    public Text currentLevelText;
    public Text isSDKEnabledText;
    public Text isFirstGameSessionText;
    public Text YG2savesText;

    public TMP_Text currentLevelText_TMP;
    public TMP_Text isSDKEnabledText_TMP;
    public TMP_Text isFirstGameSessionText_TMP;
    public TMP_Text YG2savesText_TMP;

    public PlayerProgress progress;

    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
        progress = new PlayerProgress();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddLevelButton.onClick.AddListener(delegate { AddLevel(); });
        LoadLevelButton.onClick.AddListener(delegate { LoadProgressOrInitNewAsync(); });
        SaveLevelButton.onClick.AddListener(delegate { SaveProgressCloud(); });
        ResetLevelButton.onClick.AddListener(delegate { ResetProgress(); });

        progress = (PlayerProgress)YG2.saves;

        LoadProgressOrInitNewAsync();

        UpdateText();
    }

    private void AddLevel()
    {

        _currentLevel++;
        Debug.Log($"_currentLevel = {_currentLevel}");

        progress.SetCurrentLevel(_currentLevel);
        Debug.Log($"progress.CurrentLevel = {progress.CurrentLevel}");

        //PlayerPrefs.SetInt("CurrentLevel", _currentLevel);

        UpdateText();
    }

    private void LoadProgressOrInitNewAsync()
    {
        /*        Debug.Log($"YG2.isFirstGameSession = {YG2.isFirstGameSession}");

                //_coroutineRunner.StartCoroutine(LoadProgressCoroutine());
                StartCoroutine(LoadProgressCoroutine());

                if (YG2.isFirstGameSession)
                {
                    //_progressService.Progress = NewProgress();
                    _currentLevel = YG2.saves.CurrentLevel;
                    Debug.Log($"[LoadProgressCloud] Progress created new!");
                }
                else
                {
                    _currentLevel = YG2.saves.CurrentLevel;
                    Debug.Log($"[LoadProgressCloud] Progress loaded!");
                }*/

        //UpdateText();

        if (YG2.saves == null)
            Debug.Log("YG2.saves is NULL");

        // Сохраняем из SavesYG YG2.saves локальную переменную
        _currentLevel = YG2.saves.CurrentLevel;
        Debug.Log($"[Load] _currentLevel = {_currentLevel}");

        // Сохраняем из SavesYG YG2.saves в PlayerProgress
        progress = (PlayerProgress)YG2.saves;
        Debug.Log($"[Load] progress = {progress.ToJson()}");

        UpdateText();
    }
    private void SaveProgressCloud()
    {
        _saveLoadService.SaveProgress(progress);

        UpdateText();
    }
    private void ResetProgress()
    {
        _saveLoadService.ResetProgress(progress);

        LoadProgressOrInitNewAsync();

        UpdateText();
    }

    private void UpdateText()
    {
        currentLevelText.text = "Current level: " + progress.CurrentLevel.ToString();
        isSDKEnabledText.text = "isSDKEnabled: " + YG2.isSDKEnabled.ToString();
        isFirstGameSessionText.text = "isFirstGameSession: " + YG2.isFirstGameSession.ToString();

        YG2savesText.text = YG2.saves.ToJson().ToString();
    }

}
