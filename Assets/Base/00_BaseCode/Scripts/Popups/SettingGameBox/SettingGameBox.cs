using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingGameBox : BaseBox
{
    private static SettingGameBox instance;
    public static SettingGameBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingGameBox>(PathPrefabs.SETTING_GAME_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(Close);
        backHomeBtn.onClick.AddListener(BackHome);
        tryAgainBtn.onClick.AddListener(TryAgain);
    }
    public Button backHomeBtn;
    public Button tryAgainBtn;
    public GameObject bottomHome;
    public GameObject bottonGameplay;
    private void InitState()
    {

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
        this.gameObject.SetActive(false);
    }
}
