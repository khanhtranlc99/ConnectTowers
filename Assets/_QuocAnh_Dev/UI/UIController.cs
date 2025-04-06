using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;


public class UIController : MonoBehaviour
{
    public Button startGame;
    public bool isStartGameClick = false;
    public int levelStartShowAds = 12, levelStartRocket = 3, levelStartBooster = 5;

    [Header("Play Game Variable")]
    public bool isPlayCampainBool;
    public WinBox_QA winPopupPrefab;
    public LoseBox losePopupPrefab;
    public BattleUiManager battleUiManager;
    public GameManager gameManager;
    public void InitUI()
    {
        startGame.onClick.AddListener(() =>
        {
            PlayCampainGame();
            //GameController.Instance.musicManager.PlayClickSound();
        });
        battleUiManager.Init();
    }
    private void PlayCampainGame()
    {
        isStartGameClick = true;

        startGame.gameObject.SetActive(false);
        battleUiManager.enabled = true;
        GamePlayController.Instance.enabled = true;
        gameManager.StartGame();
        isPlayCampainBool = true;
        battleUiManager.Init();
    }

    private void ActiveMainUI()
    {
        throw new NotImplementedException();
    }

    public void TryAgain()
    {

        isPlayCampainBool = false;
        battleUiManager.timeElapsed = 0;
        //ActiveMainUI();
        gameManager.ResetGame();
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
}
