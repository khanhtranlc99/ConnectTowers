using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnUpgradeBase : LoadAutoComponents
{
    [SerializeField] Button btnMain;

    private void Start()
    {
        this.btnMain.onClick.AddListener(delegate { OnClick(); GameController.Instance.musicManager.PlayClickSound(); });
    }

    public abstract void OnClick();


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBtn();
    }

    protected virtual void LoadBtn()
    {
        this.btnMain = GetComponent<Button>();
    }
}
