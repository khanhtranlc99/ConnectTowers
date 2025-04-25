using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Q_PanelShowResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtAmountReward;
    [SerializeField] Button btnClose;

    private void Start()
    {
        this.btnClose.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void SetAmountReward(int amount)
    {
        this.txtAmountReward.text = amount.ToString();
    }
}
