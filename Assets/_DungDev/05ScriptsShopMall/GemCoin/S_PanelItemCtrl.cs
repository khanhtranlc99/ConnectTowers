using DG.Tweening;
using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanelItemCtrl : MonoBehaviour
{
    [SerializeField] S_PanelGemCoin panelResult;
    public S_PanelGemCoin PanelResult => panelResult;
    private void OnEnable()
    {
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

}
