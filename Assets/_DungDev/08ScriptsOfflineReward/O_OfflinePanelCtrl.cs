using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class O_OfflinePanelCtrl : MonoBehaviour
{
    [SerializeField] Button btnClaim;
    [SerializeField] Image imgBtnClaim;
    [SerializeField] Button btnClaimAds;
    [SerializeField] Sprite claimedSpriteBtn;
    [SerializeField] Sprite defaultSpriteBtn;
    [Header("Right")]
    [SerializeField] TextMeshProUGUI gemRewardText;
    [SerializeField] TextMeshProUGUI coinRewardText;

    [SerializeField] List<O_OfflineRewardSlot> lsOfflineRewardSlots = new();

    private void OnEnable()
    {

        this.imgBtnClaim.sprite = defaultSpriteBtn;
        this.imgBtnClaim.color = new Color32(0, 255, 255, 255);

        this.btnClaimAds.image.sprite = defaultSpriteBtn;

        var DataOffline = GameController.Instance.dataContain.dataUser.DataOfflineRewardChest;

        this.gemRewardText.text = (DataOffline.GemPerHour * 3).ToString();
        this.coinRewardText.text = (DataOffline.CoinPerHour * 3).ToString();

    }

    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        this.imgBtnClaim.sprite = claimedSpriteBtn;
        this.imgBtnClaim.color = Color.white;

        this.btnClaimAds.image.sprite = claimedSpriteBtn;

        var DataUser = GameController.Instance.dataContain.dataUser;

        DataUser.AddCoins(DataUser.DataOfflineRewardChest.CoinTotal);
        DataUser.AddGems(DataUser.DataOfflineRewardChest.GemTotal);

        DataUser.DataOfflineRewardChest.DeductClaimReward();

        // hoi cong kenh
        this.UpdateUI();
        this.PostEvent(EventID.UPDATE_COIN_GEM);
    }


    void UpdateUI()
    {
        foreach(var child in this.lsOfflineRewardSlots)
        {
            child.UpdateUI();
        }
    }


}
