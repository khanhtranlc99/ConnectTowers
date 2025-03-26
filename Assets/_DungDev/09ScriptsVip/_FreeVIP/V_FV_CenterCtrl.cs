using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class V_FV_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_FV_SlotCategory> lsSlotCategorys = new();

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
