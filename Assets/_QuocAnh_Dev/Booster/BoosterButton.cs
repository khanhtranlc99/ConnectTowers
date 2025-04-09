using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoosterButton : MonoBehaviour
{
    public BoosterType boosterType;
    public Button activeBtn;
    public bool isActive = false;
    public GameObject turnObj;
    public TMP_Text turnIdx;
    public GameObject plusObj;
    public TMP_Text plus;
    public void Init()
    {
        activeBtn.onClick.AddListener(delegate { GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType); checkIdx(); });
        //plusObj.gameObject.SetActive(false);
    }

    private void checkIdx()
    {

    }

}
