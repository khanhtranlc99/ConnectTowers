using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UI_EffectZoomBtn : LoadAutoComponents
{
    [SerializeField] Image icon;
    [SerializeField] float minScale = 0.9f;
    [SerializeField] float duration = 0.5f;
    [SerializeField] Ease easeType = Ease.InOutSine;
    [SerializeField] LoopType loopType = LoopType.Yoyo;
    private Tween scaleTween;
    private void OnEnable()
    {
        this.StartPulseEffect();
    }

    void StartPulseEffect()
    {
        if (scaleTween != null && scaleTween.IsActive()) scaleTween.Kill();
        scaleTween = icon.transform.DOScale(minScale, duration)
            .SetEase(easeType)
            .SetLoops(-1, loopType);
    }

    private void OnDisable()
    {
        this.KillTween();
    }

    private void OnDestroy()
    {
        this.KillTween();
    }

    void KillTween()
    {
        if (scaleTween == null) return;
        scaleTween.Kill();
        icon.transform.localScale = Vector3.one;
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.icon = transform.Find("icon").GetComponent<Image>();
    }
}
