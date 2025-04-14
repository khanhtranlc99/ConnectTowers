using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;


public class UIController : MonoBehaviour
{
    public Button startGame, btnSetting;
    public bool isStartGameClick = false;
    public int levelStartShowAds = 12, levelStartSkill = 5;

    [Header("Play Game Variable")]
    public bool isPlayCampainBool;
    public WinBox_QA winPopupPrefab;
    public LoseBox losePopupPrefab;
    public BattleUiManager battleUiManager;
    public ResourcesCtrl resourecesCtrl;
    //public GameManager gameManager;
    public void InitUI()
    {
        startGame.onClick.AddListener(() =>
        {
            PlayCampainGame();
            //GameController.Instance.musicManager.PlayClickSound();
        });
        btnSetting.interactable = true;
        btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OutCampaign(); });
        resourecesCtrl.Init();
        battleUiManager.gameObject.SetActive(false);

    }
    private void PlayCampainGame()
    {
        GameController.Instance.musicManager.PlayClickSound();  
        isStartGameClick = true;

        startGame.gameObject.SetActive(false);
        battleUiManager.gameObject.SetActive(true);
        battleUiManager.Init();
        GamePlayController.Instance.enabled = true;
        GamePlayController.Instance.gameManager.StartGame();
        isPlayCampainBool = true;
        //battleUiManager.Init();
    }

    //private void ActiveMainUI()
    //{
    //    throw new NotImplementedException();
    //}

    public void TryAgain()
    {
        GameController.Instance.musicManager.PlayClickSound();
        isPlayCampainBool = false;
        battleUiManager.timeElapsed = 0;
        //ActiveMainUI();
        GamePlayController.Instance.gameManager.ResetGame();
    }
    public void SetInteractableButton(bool b)
    {
        if (winPopupPrefab.addMoreMoneyBtn != null)
        {
            winPopupPrefab.addMoreMoneyBtn.interactable = b;
        }
        if (winPopupPrefab.nextLevelBtn != null)
        {
            winPopupPrefab.nextLevelBtn.interactable = b;
        }
        if (losePopupPrefab.btnAdsRevive != null)
        {
            losePopupPrefab.btnAdsRevive.interactable = b;
        }
        if (losePopupPrefab.btnTryAgain != null)
        {
            losePopupPrefab.btnTryAgain.interactable = b;
        }
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        this.startGame.gameObject.SetActive(true);
        this.isStartGameClick = false;
        this.gameObject.SetActive(true);
    }
    public void ShowTutorial()
    {
        TutorialManager.Setup().Show();
    }
    private void OutCampaign()
    {
        GamePlayController.Instance.isPlay = false;
        SettingGameBox.Setup().Show();
        SettingGameBox.Setup().SetupForScene("GamePlay");
        // show ads
    }
}
