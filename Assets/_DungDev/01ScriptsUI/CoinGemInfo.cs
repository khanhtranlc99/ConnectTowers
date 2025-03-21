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
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        
        this.textCoin.text = dataUser.Coin.ToString();
        this.textGem.text = dataUser.Gem.ToString();
    }
}
