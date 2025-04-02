using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;


public class UIController : Singleton<UIController>
{
    public Button startGame;
    public bool isStartGameClick=false;
    public int levelStartShowAds = 12, levelStartRocket = 3, levelStartBooster = 5;

    [Header("Play Game Variable")]
    public bool isPlayCampainBool;
    public WinBox_QA winPopupPrefab;
    public LoseBox losePopupPrefab;
    public BattleUiManager battleUiManager;
    private void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            PlayCampainGame();
            //GameController.Instance.musicManager.PlayClickSound();
        });
    }
    private void PlayCampainGame()
    {
        isStartGameClick = true;
        
        startGame.gameObject.SetActive(false);
        battleUiManager.enabled = true;
        GameManager.Instance.StartGame();
        isPlayCampainBool = true;
    }

    private void ActiveMainUI()
    {
        throw new NotImplementedException();
    }

    public void TryAgain()
    {
        
        isPlayCampainBool = false;
        //ActiveMainUI();
        GameManager.Instance.ResetGame();
    }
    public void SetInteractableButton(bool b)
    {
        if(winPopupPrefab.addMoreMoneyBtn != null)
        {
            winPopupPrefab.addMoreMoneyBtn.interactable = b;
        }
        if(winPopupPrefab.nextLevelBtn != null)
        {
            winPopupPrefab.nextLevelBtn.interactable= b;
        }
        if(losePopupPrefab.btnAdsRevive!= null)
        {
            losePopupPrefab.btnAdsRevive.interactable = b;
        }
        if(losePopupPrefab.btnTryAgain!= null)
        {
            losePopupPrefab.btnTryAgain.interactable = b;
        }
    }
}
