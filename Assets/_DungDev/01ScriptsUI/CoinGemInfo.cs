using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinGemInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textGem;

    private void OnEnable()
    {
        UpdateUI(null);
        this.RegisterListener(EventID.UPDATE_COIN_GEM, UpdateUI);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_COIN_GEM, UpdateUI);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_COIN_GEM, UpdateUI);
    }


    public void UpdateUI(object param)
    {
        this.textCoin.text = UseProfile.D_COIN.ToString();
        this.textGem.text = UseProfile.D_GEM.ToString();
    }
}
