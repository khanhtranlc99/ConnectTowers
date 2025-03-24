using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class S_PanelCardCtrl : MonoBehaviour
{
    [SerializeField] List<S_CardSlot> lsCardSlots = new();
    public List<S_CardSlot> LsCardSlots => lsCardSlots;

    private void OnEnable()
    {
        if (GameController.Instance.dataContain.dataUser.DataShop.LsDataShopReroll.Count < 9) return;

        foreach (var child in this.lsCardSlots)
        {
            DataShopReroll dataShopReroll = GameController.Instance.dataContain.dataUser.DataShop.GetDataShopReroll(child.iD);
            child.SetPropertiesCard(dataShopReroll.propertiesUnits);
            child.SetInfoCard(dataShopReroll.propertiesUnits);
        }
    }

    [Button("Set Up ID Card Slot")]
    void SetUpID()
    {
        for (int i = 0;i < lsCardSlots.Count; i++)
        {
            lsCardSlots[i].iD = i;
        }       
    }
}
