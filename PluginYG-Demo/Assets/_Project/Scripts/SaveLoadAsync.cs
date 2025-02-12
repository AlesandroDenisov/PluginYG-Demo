using HomoLudens.Data;
using HomoLudens.Services;
using HomoLudens.Services.PersistentProgress;
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

    public PlayerProgress progress;

    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
        //progress = new PlayerProgress();
        _progressService = AllServices.Container.Single <IPersistentProgressService>();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        //progress = _progressService.Progress;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddLevelButton.onClick.AddListener(delegate { AddLevel(); });
        LoadLevelButton.onClick.AddListener(delegate { LoadProgressOrInitNewAsync(); });
        SaveLevelButton.onClick.AddListener(delegate { SaveProgressCloud(); });
        ResetLevelButton.onClick.AddListener(delegate { ResetProgress(); });

        //progress = (PlayerProgress)YG2.saves;
        //_progressService.Progress = (PlayerProgress)YG2.saves;
        
        //_progressService.Progress = YG2.saves.Progress;

        LoadProgressOrInitNewAsync();

        UpdateText();
    }

    private void AddLevel()
    {
        Debug.Log($"[SaveLoadAsync.AddLevel() - Before] YG2.saves.Progress = {YG2.saves.Progress.ToJson()}");
        Debug.Log($"[SaveLoadAsync.AddLevel() - Before] _progressService.Progress = {_progressService.Progress.ToJson()}");

        _currentLevel++;
        Debug.Log($"_currentLevel = {_currentLevel}");

        //progress.SetCurrentLevel(_currentLevel);
        //Debug.Log($"progress.CurrentLevel = {progress.CurrentLevel}");

        _progressService.Progress.SetCurrentLevel(_currentLevel);
        Debug.Log($"_progressService.Progress.CurrentLevel = {_progressService.Progress.CurrentLevel}");

        Debug.Log($"[SaveLoadAsync.AddLevel() - After] YG2.saves.Progress = {YG2.saves.Progress.ToJson()}");
        Debug.Log($"[SaveLoadAsync.AddLevel() - After] _progressService.Progress = {_progressService.Progress.ToJson()}");

        //PlayerPrefs.SetInt("CurrentLevel", _currentLevel);

        UpdateText();
    }

    private void LoadProgressOrInitNewAsync()
    {
        if (YG2.saves == null)
            Debug.Log("YG2.saves is NULL");

        // Сохраняем из SavesYG YG2.saves локальную переменную
        //_currentLevel = YG2.saves.CurrentLevel;
        _currentLevel = YG2.saves.Progress.CurrentLevel;
        Debug.Log($"[Load] _currentLevel = {_currentLevel}");

        // Сохраняем из SavesYG YG2.saves в PlayerProgress
        //progress = (PlayerProgress)YG2.saves;
        //Debug.Log($"[Load] progress = {progress.ToJson()}");

        //_progressService.Progress = (PlayerProgress)YG2.saves;
        _progressService.Progress = YG2.saves.Progress.DeepCopy();
        Debug.Log($"[Load] _progressService.Progress = {_progressService.Progress.ToJson()}");

        UpdateText();
    }
    private void SaveProgressCloud()
    {
        _saveLoadService.SaveProgress();

        UpdateText();
    }
    private void ResetProgress()
    {
        _saveLoadService.ResetProgress();

        LoadProgressOrInitNewAsync();

        UpdateText();
    }

    private void UpdateText()
    {
        //currentLevelText.text = "Current level: " + progress.CurrentLevel.ToString();
        currentLevelText.text = "Current level: " + _progressService.Progress.CurrentLevel.ToString();
        isSDKEnabledText.text = "isSDKEnabled: " + YG2.isSDKEnabled.ToString();
        isFirstGameSessionText.text = "isFirstGameSession: " + YG2.isFirstGameSession.ToString();

        YG2savesText.text = YG2.saves.ToJson().ToString();
    }

}
