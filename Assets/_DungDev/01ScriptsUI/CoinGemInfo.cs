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
        UpdateUI();
    }

    public void UpdateUI()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        
        this.textCoin.text = dataUser.Coin.ToString();
        this.textGem.text = dataUser.Gem.ToString();
    }
}
