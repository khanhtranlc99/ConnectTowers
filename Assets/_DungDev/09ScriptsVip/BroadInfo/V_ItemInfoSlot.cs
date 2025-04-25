using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class V_ItemInfoSlot : LoadAutoComponents
{
    [SerializeField] TextMeshProUGUI txtInfo;


    public void UpdateUI(string param)
    {
        this.txtInfo.text = param;
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.txtInfo = GetComponent<TextMeshProUGUI>();
    }
}
