using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_SlotCategory> lsSlotCategorys = new();

    public int test = 0;
    private void OnEnable()
    {
        foreach(var child in this.lsSlotCategorys) child.gameObject.SetActive(false);
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        var rewardSystem = dataVip.LsRewardSystems[test];

        for (int i = 0; i < rewardSystem.LsRewardCategorys.Count; i++)
        {
            this.lsSlotCategorys[i].gameObject.SetActive(true);
            this.lsSlotCategorys[i].UpdateUI(rewardSystem.LsRewardCategorys[i]);
        }
        
    }
}
