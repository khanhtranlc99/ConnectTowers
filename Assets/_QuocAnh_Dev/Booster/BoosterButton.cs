using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterButton : MonoBehaviour
{
    public BoosterType boosterType;
    public Button activeBtn;
    public bool isActive = false;

    public void Init()
    {
        activeBtn.onClick.AddListener(delegate { GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType); });
    }
}
