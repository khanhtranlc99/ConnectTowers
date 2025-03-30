using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class WinBox_QA : BaseBox
{
    private static WinBox_QA _instance;
    public static WinBox_QA Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<WinBox_QA>(PathPrefabs.WIN_BOX_QA));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    public Button nextLevelBtn;
    public Button rewardBtn;
    public CanvasGroup canvasGroup;

    private void Init()
    {
        nextLevelBtn.onClick.AddListener(delegate { HandleNextLevel(); });
        rewardBtn.onClick.AddListener(delegate { HandleReward(); });

        UseProfile.CurrentLevel += 1;
        Debug.LogError("currentLevel " + UseProfile.CurrentLevel);
        if(UseProfile.CurrentLevel >= 10)
        {
            UseProfile.CurrentLevel = 10;
        }
        UseProfile.WinStreak += 1;
    }
    private void InitState()
    {
        Debug.LogError("currentLevel " + UseProfile.CurrentLevel);
        // day la firebase
        //GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);
    }

    private void HandleReward()
    {
        throw new NotImplementedException();
    }

    public void HandleNextLevel()
    {
        // con thieu nhieu
        this.Close();
        //Initiate.Fade("GamePlay", Color.black, 2f);
        GameManager.Instance.CreateGame();
    }

    private void LoadNextLevel()
    {
        throw new NotImplementedException();
    }
}