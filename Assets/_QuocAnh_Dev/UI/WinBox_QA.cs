using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;
using BestHTTP.Extensions;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    public Button addMoreMoneyBtn;
    public Button rewardBtn;
    public CanvasGroup canvasGroup;
    public TMP_Text gift;

    private void Init()
    {
        nextLevelBtn.onClick.AddListener(delegate { HandleNextLevel(); });
        rewardBtn.onClick.AddListener(delegate { HandleReward(); });

        UseProfile.CurrentLevel += 1;
        Debug.LogError("currentLevel " + UseProfile.CurrentLevel);
        //if (UseProfile.CurrentLevel >= 10)
        //{
        //    UseProfile.CurrentLevel = 10;
        //}
        UseProfile.WinStreak += 1;
        gift.text = ConfigData.Instance.lv[Mathf.Clamp(UseProfile.CurrentLevel, 0, ConfigData.Instance.lv.Count - 1)].Reward.ToString();
    }
    private void InitState()
    {
        Debug.LogError("currentLevel " + UseProfile.CurrentLevel);
        // day la firebase
        //GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);
        
    }

    private void HandleReward()
    {
        GameController.Instance.musicManager.PlayClickSound();
    }

    public void HandleNextLevel()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GamePlayController.Instance.gameManager.CreateGame();
        GamePlayController.Instance.uIController.battleUiManager.Init();
        GameController.Instance.dataContain.dataUser.AddCoins(gift.text.ToInt32());
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.gameObject.SetActive(false);
    }

}