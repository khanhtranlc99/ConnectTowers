using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResult : BtnUpgradeBase
{
    [SerializeField] Image imgResult;
    [SerializeField] TextMeshProUGUI nameResult;
    [SerializeField] Button btnClose;

    public override void OnClick()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void UpdateUI(Sprite sprite,string nameResult)
    {
        this.imgResult.sprite = sprite;
        this.imgResult.SetNativeSize();
        this.nameResult.text = nameResult;
    }
}
