using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PanelGemCoin : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txtAmount;
    [SerializeField] Button btnClaim;

    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        this.transform.gameObject.SetActive(false);
    }


    public void SetDisplayResult(Sprite sprite, string amount)
    {
        this.icon.sprite = sprite;
        this.icon.SetNativeSize();
        this.txtAmount.text = amount;
    }
}
