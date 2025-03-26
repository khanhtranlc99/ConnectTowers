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
        this.HandleVipParam(dataVip.LsRewardSystems[dataVip.CurrentVip].IconVip);

        this.UpdateUI(null);
        this.RegisterListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
        this.RegisterListener(EventID.UPDATE_VIPPARAM, this.HandleVipParam);
        this.RegisterListener(EventID.UPDATE_AVATAR_VIP, this.UpdateIconVip);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
        this.RemoveListener(EventID.UPDATE_VIPPARAM, this.HandleVipParam);
        this.RemoveListener(EventID.UPDATE_AVATAR_VIP, this.UpdateIconVip);

    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
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
        var rewardSystems = dataVip.LsRewardSystems[dataVip.CurrentVip];
        vipParam++;
        if (vipParam > dataVip.LsRewardSystems.Count - 1) vipParam = dataVip.LsRewardSystems.Count - 1;
        this.txtShowText.text = "Level " + vipParam + " VIP";
        this.txtShadow.text = "Level " + vipParam + " VIP";
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);

        Debug.LogError("VIPPARAM: " + vipParam);

        foreach (var child in centerCtrl.LsSlotCategorys)
        {
            if (!child.gameObject.activeSelf) continue;

            //bool canClaim = !(vipParam > dataVip.CurrentVip && rewardSystems.LsRewardCategorys[child.idSlot].isClaim);
            bool canClaim = vipParam <= dataVip.CurrentVip && !rewardSystems.LsRewardCategorys[child.idSlot].isClaim;
            child.HandleBtnState(canClaim);
        }
        GameController.Instance.musicManager.PlayClickSound();

    }
    void OnClickBtnPrev()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        vipParam--;
        if (vipParam < 0) vipParam = 0;
        var rewardSystems = dataVip.LsRewardSystems[vipParam];
        this.txtShowText.text = "Level " + vipParam + " VIP";
        this.txtShadow.text = "Level " + vipParam + " VIP";
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);

        foreach (var child in centerCtrl.LsSlotCategorys)
        {
            if (!child.gameObject.activeSelf) continue;
            bool canClaim = vipParam <= dataVip.CurrentVip && !rewardSystems.LsRewardCategorys[child.idSlot].isClaim;

            child.HandleBtnState(canClaim);
        }

        GameController.Instance.musicManager.PlayClickSound();

    }

    void HandleVipParam( object param)
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        this.vipParam = dataVip.CurrentVip;
    }


    void UpdateUI(object param)
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        var rewardSystem = dataVip.GetRewardSystem(dataVip.CurrentVip);
        this.txtCurrentProgress.text = dataVip.CurrentProgress.ToString();
        this.txtTotalProgess.text = "/" + rewardSystem.TotalProgress.ToString();

        this.progressBar.fillAmount = dataVip.CurrentProgress / (float)rewardSystem.TotalProgress;

        this.txtShowText.text = "Level " + dataVip.CurrentVip.ToString() + " VIP";
        this.txtShadow.text = "Level " + dataVip.CurrentVip.ToString() + " VIP";
        this.txtTitle.text = "Acquire " + (rewardSystem.TotalProgress - dataVip.CurrentProgress).ToString() + " <sprite=0>  to reach 1";
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
