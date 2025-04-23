using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using EventDispatcher;
using TMPro;

public class HomeScene : BaseScene
{
    public D_CanvasHomeScene canvasHomeScene;
    [Space(10)]

    public Button btnSetting;


    public Button btnPlay;
    public Button btnMatch;
    public Button btnShop;


    public CoinHeartBar coinHeartBar;


    public TMP_Text tvLevel;
    public Text tvDifficut;
    public Image imgLevelType;
    public Sprite easySprite;
    public Sprite hardSprite;
    public Sprite veryHardSprite;
    public Button btnUpgrade;
    public Button btnSummon;
    public Button btnWheelSpin;
    public Button btnQuest;
    public Button btnNoAds;
    public Button btnOfflineReward;
    public Button btnVip;
    public Button btnProfile;
    public int NumberPage(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.ShopButton:
                return 0;

            case ButtonType.HomeButton:
                return 1;

            case ButtonType.RankButton:
                return 2;
        }
        return 0;
    }


    public void Init()
    {

        // coinHeartBar.Init();

        btnUpgrade.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound();
            UpgradeBox.Setup().Show();
            canvasHomeScene.SetSateThis(false);
        });

        btnSummon.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); SummonBox.Setup().Show(); });
        btnWheelSpin.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); WheelSpinBox.Setup().Show(); });
        btnQuest.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); QuestBox.Setup().Show(); });
        btnNoAds.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); NoAdsBox.Setup().Show(); });
        btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); SettingGameBox.Setup().Show(); SettingGameBox.Setup().SetupForScene("HomeScene"); });
        btnOfflineReward.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OfflineRewardChestBox.Setup().Show(); });
        btnVip.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound();
            VipBox.Setup().Show();
            canvasHomeScene.SetStateCanvasDynamic(false);
        });
        btnProfile.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); ProfileBox.Setup().Show(); });
		btnMatch.onClick.AddListener(delegate { FindMatch(); });

        //btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OnSettingClick(); });


        btnShop.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound();
            canvasHomeScene.SetSateThis(false);
            ShopMallBox.Setup().Show(); });

        tvLevel.text = "LEVEL " + UseProfile.CurrentLevel.ToString();

        //btnPlay.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); });

    }

    private void FindMatch()
    {
        GameController.Instance.currentScene = SceneType.GamePlay;
        //GameManager.Instance.isFindMatch = true;

        //SceneLoader.Instance.LoadScene("GamePlay", OnGamePlayLoaded);

        Initiate.Fade("GamePlay", Color.black, 1.5f);
    }

    //private void OnGamePlayLoaded()
    //{
    //    GamePlayController.Instance.gameObject.SetActive(true);
    //    GameManager.Instance.enabled = true;
    //    GamePlayController.Instance.enabled = true;
    //    uIController.StartGame();
    //    GameManager.Instance.CreateGame();
    //    //this.gameObject.SetActive(false);
    //}

    //private void Update()
    //{

    //       // OnScreenChange();


    //}





    public override void OnEscapeWhenStackBoxEmpty()
    {
        //Hiển thị popup bạn có muốn thoát game ko?
    }
    private void OnSettingClick()
    {
        SettingBox.Setup(false).Show();
        //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    


}
