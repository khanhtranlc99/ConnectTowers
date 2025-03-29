using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class V_FV_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_FV_SlotCategory> lsSlotCategorys = new();
    [SerializeField] Sprite defaultBtn;
    [SerializeField] Sprite unClaimBtn;
    private void OnEnable()
    {
        this.UpdateUI(null);

        this.RegisterListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);

    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);

    }

    public void UpdateUI(object obj)
    {
        foreach (var child in this.lsSlotCategorys) child.gameObject.SetActive(false);

        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        for(int i = 0; i < dataVip.LsRewardDailySystems.Count; i++)
        {
            this.lsSlotCategorys[i].gameObject.SetActive(true);
            this.lsSlotCategorys[i].UpdateUI(dataVip.LsRewardDailySystems[i]);
            this.lsSlotCategorys[i].HandleStateBtnClaim(!dataVip.LsRewardDailySystems[i].isCollected);
            this.lsSlotCategorys[i].nextClaimDate = dataVip.LsRewardDailySystems[i].Day;

            if (this.lsSlotCategorys[i].nextClaimDate > dataVip.CurrentDay)
            {
                this.lsSlotCategorys[i].btnClaim.interactable = false;
                this.lsSlotCategorys[i].btnClaim.image.sprite = unClaimBtn;
                this.lsSlotCategorys[i].btnClaim.image.color = Color.white;
                continue;
            }
            this.lsSlotCategorys[i].btnClaim.interactable = true;
            this.lsSlotCategorys[i].btnClaim.image.sprite = defaultBtn;
            this.lsSlotCategorys[i].btnClaim.image.color = new Color32(0,255,255,255);
        }

    }

    [Button("SetUp lsSlotCatogorys")]
    void SetUp()
    {
        for (int i = 0; i < this.lsSlotCategorys.Count; i++)
        {
            this.lsSlotCategorys[i].idCategory = i;
        }
    }

}
