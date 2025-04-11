using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseBooster : BaseBox
{
    public static PurchaseBooster _instance;
    public static PurchaseBooster Setup(GiftType giftType, bool isBoosterTut = false)
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<PurchaseBooster>(PathPrefabs.PURCHASE_BOOSTER));
            _instance.Init();
        }
        _instance.InitState(giftType, isBoosterTut);
        return _instance;
    }

    public Button btnClose;
    public Button payByCoinBtn;
    public Button payByAdsBtn;
    public Button payByIAPBtn;
    public int priceGem;
    public int priceIAP;
    GiftType currentGift;
    ActionWatchVideo actionWatchVideo;
    public Image iconDecor1;
    public Image iconDecor2;
    public Image iconDecor3;
    public void Init()
    {
        btnClose.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); Close(); });

        payByCoinBtn.onClick.AddListener(delegate { HandlePayByCoin(); });
        payByAdsBtn.onClick.AddListener(delegate {HandlePayByAds(); });
        payByIAPBtn.onClick.AddListener(delegate { HandlePayByIAP(); });
    }

    public void InitState(GiftType giftType, bool isTut)
    {
        currentGift = giftType;
        switch (giftType)
        {
            case GiftType.Meteor_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.Meteor_Booster;
                break;
            case GiftType.ArrowRain_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.ArrowRain_Booster;
                break;
            case GiftType.Freeze_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.Freeze_Booster;
                break;
            case GiftType.HealingUp_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.HealingUp_Booster;
                break;
            case GiftType.SpeedUp_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.SpeedUp_Booster;
                break;
            case GiftType.SpawnsUp_Booster:
                priceGem = 20;
                actionWatchVideo = ActionWatchVideo.SpawnsUp_Booster;
                break;

        }
        iconDecor1.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(giftType);
        iconDecor1.SetNativeSize();
        iconDecor2.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(giftType);
        iconDecor2.SetNativeSize();
        iconDecor3.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(giftType);
        iconDecor3.SetNativeSize();
        if (isTut)
        {
            payByAdsBtn.gameObject.SetActive(false);
            payByCoinBtn.gameObject.SetActive(false);
            payByIAPBtn.gameObject.SetActive(false);
        }
        else
        {
            payByAdsBtn.gameObject.SetActive(true);
            payByCoinBtn.gameObject.SetActive(true);
            payByIAPBtn.gameObject.SetActive(true);
        }
    }


    public void HandlePayByAds()
    {

        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowVideoReward(
                     actionReward: () =>
                     {
                         switch (currentGift)
                         {
                             case GiftType.Meteor_Booster:
                                     HandleClaimGift();
                                 break;
                             case GiftType.ArrowRain_Booster:
                                     HandleClaimGift();
                                 break;
                             case GiftType.Freeze_Booster:
                                 HandleClaimGift();
                                 break;
                             case GiftType.HealingUp_Booster:
                                 HandleClaimGift();
                                 break;
                             case GiftType.SpeedUp_Booster:
                                 HandleClaimGift();
                                 break;
                             case GiftType.SpawnsUp_Booster:
                                 HandleClaimGift();
                                 break;
                         }
                     },
                     actionNotLoadedVideo: () =>
                     {
                         GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                          (
                             payByAdsBtn.transform
                             ,
                          payByAdsBtn.transform.position,
                          "No video at the moment!",
                          Color.white,
                          isSpawnItemPlayer: true
                          );
                     },
                     actionClose: null,
                     actionWatchVideo,
                     UseProfile.CurrentLevel.ToString());
    }

    public void HandlePayByCoin()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Coin >= priceGem)
        {
            GameController.Instance.dataContain.dataUser.DeductGem(priceGem);
            this.PostEvent(EventID.UPDATE_COIN_GEM);
            HandleClaimGift();
        }
        else
        {
            ShopBox.Setup(ButtonShopType.Gem).Show();
        }


    }

    public void HandlePayByIAP()
    {
        GameController.Instance.musicManager.PlayClickSound();
        // TODO: Gọi IAP thực tế ở đây
        Debug.Log("Đang xử lý mua IAP...");
        HandleClaimGift();
    }

    public void HandleClaimGift()
    {

        Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 2);
        //List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        //giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        //PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });

    }
    public void HandleClaimGiftX1()
    {


        Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 1);
        //List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        //giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        //PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });

    }
}


