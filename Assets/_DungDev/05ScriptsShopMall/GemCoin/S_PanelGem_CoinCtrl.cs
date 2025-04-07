using DG.Tweening;
using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class S_PanelGem_CoinCtrl : MonoBehaviour
{
    [SerializeField] S_PanelGemCoin panelResult;
    public S_PanelGemCoin PanelResult => panelResult;

    [SerializeField] List<S_DailyTimer> lsDailyTimer = new();
    [SerializeField] List<S_GemCoinSlot> lsGemCoinSlots = new();
    private void OnEnable()
    {
        foreach(var child in this.lsDailyTimer)
        {
            child.Init();
        }
        var dataUser = GameController.Instance.dataContain.dataUser;
        dataUser.ResetDailyDay();
        //dataUser.DataShop.LoadShopMallCoin_GEM();



        for (int i = 0; i < dataUser.DataShop.LsIsRewardCollected.Count; i++)
        {
            this.lsGemCoinSlots[i].idSlot = i;
            this.lsGemCoinSlots[i].Init(dataUser.DataShop.LsIsRewardCollected[i].isCollected);
        }
        this.RegisterListener(EventID.PANEL_RESULT_GEM_COIN, ActiveTrans);

    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.PANEL_RESULT_GEM_COIN, ActiveTrans);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.PANEL_RESULT_GEM_COIN, ActiveTrans);
    }

    void ActiveTrans(object param)
    {
        this.panelResult.gameObject.SetActive(true);
        this.panelResult.transform.localScale = Vector3.zero;
        this.panelResult.transform.DOScale(1f,0.2f);
    }

    [Button("Set up id")]
    void SetUp()
    {
        for (int i = 0; i < this.lsGemCoinSlots.Count;i++)
        {
            this.lsGemCoinSlots[i].idSlot = i;
        }
    }
}
