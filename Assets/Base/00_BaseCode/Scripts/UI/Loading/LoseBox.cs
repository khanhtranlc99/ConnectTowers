using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoseBox : BaseBox
{
    public static LoseBox _instance;
    public static LoseBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<LoseBox>(PathPrefabs.LOSE_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
    public Button btnAdsRevive;
    public Button btnTryAgain;
    public Button btnUpgrade;

    public void Init()
    {
        btnAdsRevive.onClick.AddListener(() =>
        {
            HandleAdsRevive();
        });
        btnTryAgain.onClick.AddListener(() => 
        {
            TryAgainLevel();
        }
        );
        btnUpgrade.onClick.AddListener(() =>
        {
            HandleBackHome();
        });

    }

    private void TryAgainLevel(bool rewarAdsBool = false)
    {
        if(!rewarAdsBool && UseProfile.CurrentLevel > GamePlayController.Instance.uIController.levelStartShowAds)
        {
            // ads
        }
        GamePlayController.Instance.uIController.TryAgain();
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "InterWinBox");
        void Next()
        {

            Close();
            Initiate.Fade("GamePlay", Color.black, 2f);

        }
        //GamePlayController.Instance.uIController.TryAgain();
    }

    public void InitState()
    {
        GameController.Instance.AnalyticsController.LoseLevel(UseProfile.CurrentLevel);
    }
    public void HandleReviveByCoin()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Coin >= 100)
        {
            UseProfile.Coin -= 100;
            GamePlayController.Instance.stateGame = StateGame.Playing;
    
         
 
            Close();
        
        }
        else
        {
            ShopBox.Setup(ButtonShopType.Gold).Show();
     
        }


    }
    public void HandleAdsRevive()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowVideoReward(
                    actionReward: () =>
                    {
                       
                            Close();
                  
                  
                    },
                    actionNotLoadedVideo: () =>
                    {
                        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                         (btnAdsRevive.transform
                            ,
                         btnAdsRevive.transform.position,
                         "No video at the moment!",
                         Color.white,
                         isSpawnItemPlayer: true
                         );
                    },
                    actionClose: null,
                    ActionWatchVideo.ReviveFreeLoseBox,
                    UseProfile.CurrentLevel.ToString());



    }
    public void HandleClose()
    {
        GameController.Instance.musicManager.PlayClickSound();
        //Close();
        BackHomeBox.Setup(TypeBackHOme.BackHome).Show();

    }
    public void HandleBackHome()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.currentScene = SceneType.MainHome;
        Initiate.Fade("HomeScene", Color.black, 1.5f);
    }

}
