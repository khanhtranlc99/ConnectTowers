using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        this.vipParam = dataVip.CurrentVip;
        this.UpdateUI(null);

        this.RegisterListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, this.UpdateUI);
    }


    private void Start()
    {
        this.btnNext.onClick.AddListener(OnClickBtnNext);
        this.btnPrev.onClick.AddListener(OnClickBtnPrev);
    }

    void OnClickBtnNext()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        vipParam++;
        if (vipParam > 12) vipParam = 12;
        this.txtShowText.text = "Level " + vipParam + "VIP";
        this.txtShadow.text = "Level " + vipParam + "VIP";
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);
    }
    void OnClickBtnPrev()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        vipParam--;
        if (vipParam < 0) vipParam = 0;
        this.txtShowText.text = "Level " + vipParam + "VIP";
        this.txtShadow.text = "Level " + vipParam + "VIP";
        this.centerCtrl.UpdateUI(dataVip.LsRewardSystems[vipParam]);

    }

    void UpdateUI(object param)
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        var rewardSystem = dataVip.GetRewardSystem(dataVip.CurrentVip);

        this.iconVip.sprite = rewardSystem.IconVip;
        this.txtCurrentProgress.text = dataVip.CurrentProgress.ToString();
        this.txtTotalProgess.text = "/" + rewardSystem.TotalProgress.ToString();

        this.progressBar.fillAmount = dataVip.CurrentProgress / (float)rewardSystem.TotalProgress;
        this.txtShowText.text = "Level " + dataVip.CurrentVip.ToString() + " VIP";
        this.txtShadow.text = "Level " + dataVip.CurrentVip.ToString() + " VIP";
        this.txtTitle.text = "Acquire " + (rewardSystem.TotalProgress - dataVip.CurrentProgress).ToString() + " <sprite=0>  to reach 1";
    }
}
