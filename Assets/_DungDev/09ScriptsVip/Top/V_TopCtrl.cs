using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class V_TopCtrl : MonoBehaviour
{
    [SerializeField] V_CenterCtrl centerCtrl;
    [Space(10)]
    [Header("Title")]
    [SerializeField] TextMeshProUGUI txtTitle;
    [Space(10)]
    [SerializeField] Image iconVip;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI txtCurrentProgress;
    [SerializeField] TextMeshProUGUI txtTotalProgess;

    [Header("Show Vip Level")]
    [SerializeField] TextMeshProUGUI txtShadow;
    [SerializeField] TextMeshProUGUI txtShowText;

    [SerializeField] Button btnNext;
    [SerializeField] Button btnPrev;
    int vipParam;
    private void OnEnable()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        this.HandleVipParam(dataVip.LsRewardSystems[UseProfile.CurrentVip]);
        this.UpdateTileVIPBOX(UseProfile.CurrentVip);
        this.UpdateUI(null);
        this.iconVip.sprite = dataVip.LsRewardSystems[UseProfile.CurrentVip].IconVip;
        iconVip.SetNativeSize();

        this.RegisterListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
        this.RegisterListener(EventID.UPDATE_TILE_VIPBOX, this.UpdateTileVIPBOX);
        this.RegisterListener(EventID.UPDATE_VIPPARAM, this.HandleVipParam);
        this.RegisterListener(EventID.UPDATE_AVATAR_VIP, this.UpdateIconVip);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
        this.RemoveListener(EventID.UPDATE_TILE_VIPBOX, this.UpdateTileVIPBOX);
        this.RemoveListener(EventID.UPDATE_VIPPARAM, this.HandleVipParam);
        this.RemoveListener(EventID.UPDATE_AVATAR_VIP, this.UpdateIconVip);

    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
        this.RemoveListener(EventID.UPDATE_TILE_VIPBOX, this.UpdateTileVIPBOX);
        this.RemoveListener(EventID.UPDATE_VIPPARAM, this.HandleVipParam);
        this.RemoveListener(EventID.UPDATE_AVATAR_VIP, this.UpdateIconVip);

    }


    private void Start()
    {
        this.btnNext.onClick.AddListener(OnClickBtnNext);
        this.btnPrev.onClick.AddListener(OnClickBtnPrev);
        
    }

    void OnClickBtnNext()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        var rewardSystems = dataVip.LsRewardSystems[UseProfile.CurrentVip];
        vipParam++;
        if (vipParam > dataVip.LsRewardSystems.Count - 1) vipParam = dataVip.LsRewardSystems.Count - 1;
        this.UpdateTileVIPBOX(vipParam);
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);
        foreach (var child in centerCtrl.LsSlotCategorys)
        {
            if (!child.gameObject.activeSelf) continue;

            bool canClaim = vipParam <= UseProfile.CurrentVip && !rewardSystems.LsRewardCategorys[child.idSlot].isClaim;
            child.HandleBtnState(canClaim);
            child.vipParam = vipParam;
        }

        GameController.Instance.musicManager.PlayClickSound();

    }
    void OnClickBtnPrev()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        vipParam--;
        if (vipParam < 0) vipParam = 0;
        var rewardSystems = dataVip.LsRewardSystems[vipParam];
        this.UpdateTileVIPBOX(vipParam);
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);
        foreach (var child in centerCtrl.LsSlotCategorys)
        {
            if (!child.gameObject.activeSelf) continue;
            bool canClaim = vipParam <= UseProfile.CurrentVip && !rewardSystems.LsRewardCategorys[child.idSlot].isClaim;
            child.HandleBtnState(canClaim);
            child.vipParam = vipParam;

        }

        GameController.Instance.musicManager.PlayClickSound();

    }

    void HandleVipParam( object param)
    {
        this.vipParam = UseProfile.CurrentVip;

        foreach(var child in this.centerCtrl.LsSlotCategorys)
        {
            if(!child.gameObject.activeSelf) continue;
            child.vipParam = vipParam;
        }
    }


    void UpdateUI(object param)
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        var rewardSystem = dataVip.GetRewardSystem(UseProfile.CurrentVip);
        this.txtCurrentProgress.text = UseProfile.CurrentProgress.ToString();
        this.txtTotalProgess.text = "/" + rewardSystem.TotalProgress.ToString();
        this.progressBar.fillAmount = UseProfile.CurrentProgress / (float)rewardSystem.TotalProgress;
        this.txtTitle.text = "Acquire " + (rewardSystem.TotalProgress - UseProfile.CurrentProgress).ToString() + " <sprite=0>  to reach 1";
    }

    void UpdateTileVIPBOX(object obj)
    {
        if (!(obj is int currentVip)) return;

        this.txtShowText.text = "Level " + currentVip.ToString() + " VIP";
        this.txtShadow.text = "Level " + currentVip.ToString() + " VIP";
    }

    void UpdateIconVip(object param)
    {

        if (!(param is Sprite sprite)) return;
        this.iconVip.sprite = sprite;
        this.iconVip.SetNativeSize();
        this.iconVip.transform.localScale = Vector3.zero;
        this.iconVip.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }

}
