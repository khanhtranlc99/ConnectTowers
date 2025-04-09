using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventDispatcher;

public class BoosterButton : MonoBehaviour
{
    public BoosterType boosterType;
    public Button activeBtn;
    public bool isActive = false;
    public GameObject turnObj;
    public TMP_Text turnIdx;
    public GameObject plusObj;
    public TMP_Text plus;
    public int turn;
    public virtual void Init()
    {
        activeBtn.onClick.AddListener(delegate { GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType); UpdateUI(); });
        UpdateUI();
    }
    public virtual void UpdateUI()
    {
        
    }

}
