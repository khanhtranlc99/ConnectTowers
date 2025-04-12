using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EventDispatcher;
using DG.Tweening.Core.Easing;

public class SettingInBattle : BaseBox
{
    private static SettingInBattle instance;
    public static SettingInBattle Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingInBattle>(PathPrefabs.SETTING_IN_BATTLE));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    public Button backHomeBtn;
    public Button tryAgainBtn;
    public GameObject bottomHome;
    public GameObject bottonGameplay;
    private void Init()
    {
        btnClose.onClick.AddListener(BackGame);
        backHomeBtn.onClick.AddListener(BackHome);
        tryAgainBtn.onClick.AddListener(TryAgain);
    }
    private void InitState()
    {

    }

    private void BackGame()
    {
        Close();
        GamePlayController.Instance.isPlay = true;
    }
    private void BackHome()
    {
        GamePlayController.Instance.gameManager.EndGame();
        GamePlayController.Instance.EndGame();
        GamePlayController.Instance.uIController.EndGame();
        GameController.Instance.currentScene = SceneType.MainHome;
        GamePlayController.Instance.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        Initiate.Fade("HomeScene", Color.black, 1.5f);
    }

    public void SetupForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "HomeScene":
                bottomHome.SetActive(true);
                bottonGameplay.SetActive(false);
                break;
            case "GamePlay":
                bottomHome.SetActive(false);
                bottonGameplay.SetActive(true);
                break;
        }
    }

    private void TryAgain()
    {
        GamePlayController.Instance.uIController.TryAgain();
        Initiate.Fade("GamePlay", Color.black, 1.5f);
    }
}
