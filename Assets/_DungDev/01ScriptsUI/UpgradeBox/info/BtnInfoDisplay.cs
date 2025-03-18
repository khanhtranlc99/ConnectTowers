using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnInfoDisplay : BtnUpgradeBase
{
    [SerializeField] Transform trans;

    public override void OnClick()
    {
        trans.gameObject.SetActive(!trans.gameObject.activeSelf);
    }
}
