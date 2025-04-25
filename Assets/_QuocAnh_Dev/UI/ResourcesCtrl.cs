using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ResourcesCtrl : MonoBehaviour
{
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textGem;

    public void Init()
    {
        UpdateUi(null);
        this.RegisterListener(EventID.UPDATE_COIN_GEM, UpdateUi);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_COIN_GEM, UpdateUi);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_COIN_GEM, UpdateUi);
    }


    public void UpdateUi(object param)
    {
        if(UseProfile.D_COIN <= 0 && UseProfile.D_GEM <= 0)
        {
            UseProfile.D_COIN = 0;
            UseProfile.D_GEM = 0;
        }
        this.textCoin.text = UseProfile.D_COIN.ToString();
        this.textGem.text = UseProfile.D_GEM.ToString();
    }
}
