using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class S_PanelCardCtrl : MonoBehaviour
{
    [HideInInspector]
    public DataUserShop dataShop;
    [HideInInspector]
    public DataUnits dataUnit;

    [SerializeField] List<S_CardSlot> lsCardSlots = new();
    public List<S_CardSlot> LsCardSlots => lsCardSlots;

    public S_DailyTimer rerollTimer;

    private void OnEnable()
    {
        var dataUser = GameController.Instance.dataContain.dataUser;
        dataUser.ResetDailyDay();
        //dataUser.DataShop.LoadShopMallReroll();
        rerollTimer.Init();
        if (dataUser.DataShop.LsDataShopReroll.Count < 9) return;
        foreach (var child in this.lsCardSlots)
        {
            DataShopReroll dataShopReroll = dataUser.DataShop.GetDataShopReroll(child.iD);
            child.Init(dataShopReroll);
            child.SetPropertiesCard(dataShopReroll.propertiesUnits);
            child.SetInfoCard(dataShopReroll.propertiesUnits);
            child.ShowTextCoinAmount(dataShopReroll.currentCostAmount);
            if (dataShopReroll.DefaultCostAmount > 0) {
                int percentage = Mathf.RoundToInt((1f - (dataShopReroll.currentCostAmount / (float)dataShopReroll.DefaultCostAmount)) * 100f);
                if (percentage < 1) continue;
                child.ShowIconSale(percentage);
            }
        }

    }
    

    #region odin
    [Button("SetUp ID, lsCardRank",ButtonSizes.Large)]
    void SetUpRan()
    {
        for(int i = 0; i < this.lsCardSlots.Count; i++)
        {
            //setup id + lsCardRank
            this.lsCardSlots[i].iD = i;
            if (this.lsCardSlots[i].iD == dataShop.LsDataShopReroll[i].idCard)
            {
                this.lsCardSlots[i].lsCardRanks = dataShop.LsDataShopReroll[i].lsUnitRanks;
            }
        }
        //set up result
        foreach (var cardSlot in this.lsCardSlots)
        {
            cardSlot.lsPro.Clear();

            foreach (var unit in this.dataUnit.lsPropertiesBases)
            {
                if (cardSlot.lsCardRanks.Contains(unit.unitRank))
                {
                    cardSlot.lsPro.Add(unit);
                }
            }
        }
    }
    #endregion
}
