using HomoLudens.Data;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SaveLoad : MonoBehaviour
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

    private PlayerProgress progress;

    // Start is called before the first frame update
    void Start()
    {
        AddLevelButton.onClick.AddListener(delegate { AddLevel(); });
        SaveLevelButton.onClick.AddListener(delegate { SaveProgressCloud(); });
        LoadLevelButton.onClick.AddListener(delegate { LoadProgressCloud(); });
        ResetLevelButton.onClick.AddListener(delegate { ResetProgress(); });

        progress = new PlayerProgress();

        /*        if (YG2.isSDKEnabled)
                    LoadProgressCloud();*/


        //_currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

    }

    private void OnEnable() => YG2.onGetSDKData += LoadProgressCloud;

    private void OnDisable() => YG2.onGetSDKData -= LoadProgressCloud;

    private void LoadProgressCloud()
    {
        Debug.Log($"YG2.isFirstGameSession = {YG2.isFirstGameSession}");

        if (YG2.isFirstGameSession)
        {
            //_currentLevel = YG2.saves.CurrentLevel;
            _currentLevel = YG2.saves.Progress.CurrentLevel;
            Debug.Log($"[LoadProgressCloud] Progress created new!");
        }
        else
        {
            //_currentLevel = YG2.saves.CurrentLevel;
            _currentLevel = YG2.saves.Progress.CurrentLevel;
            Debug.Log($"[LoadProgressCloud] Progress loaded!");
        }

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

    private void SaveProgressCloud()
    {
        //YG2.saves += progress;
        YG2.saves.Progress = progress.DeepCopy();

        YG2.SaveProgress(); 
        Debug.Log($"[SaveProgressCloud] Progress saved!");
    }

    public void ResetProgress()
    {
        YG2.SetDefaultSaves();
        YG2.SaveProgress();
        Debug.Log($"[ResetProgress] Progress was reset!");

        UpdateText();
    }

    private void UpdateText()
    {
        currentLevelText.text = SetCurrentLevel();
        isSDKEnabledText.text = "isSDKEnabled: " + YG2.isSDKEnabled.ToString();
        isFirstGameSessionText.text = "isFirstGameSession: " + YG2.isFirstGameSession.ToString();

        YG2savesText.text = YG2.saves.ToJson().ToString();
    }

    private string SetCurrentLevel()
    {
        return "Текущий уровень " + _currentLevel.ToString();
    }


}
