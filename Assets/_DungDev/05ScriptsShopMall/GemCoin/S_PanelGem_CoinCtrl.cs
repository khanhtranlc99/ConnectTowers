using DG.Tweening;
using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanelGem_CoinCtrl : MonoBehaviour
{
    [SerializeField] S_PanelGemCoin panelResult;
    public S_PanelGemCoin PanelResult => panelResult;

    [SerializeField] List<S_DailyTimer> lsDailyTimer = new();

    private void OnEnable()
    {
        this.RegisterListener(EventID.PANEL_RESULT_GEM_COIN, ActiveTrans);

        foreach(var child in this.lsDailyTimer)
        {
            child.ResetDay();
        }
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

}
