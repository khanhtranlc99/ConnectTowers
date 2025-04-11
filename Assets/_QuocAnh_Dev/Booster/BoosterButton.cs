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
    public bool isActive = true;
    public GameObject turnObj;
    public TMP_Text turnIdx;
    public GameObject plusObj;
    public TMP_Text plus;
    public GameObject cntDown;
    public TMP_Text cntDownText;
    public int turn;
    private float duration = 60f;
    private float timer;
    [HideInInspector] public GiftType giftType;
    public virtual void Init()
    {
        cntDown.SetActive(false);
        activeBtn.onClick.AddListener(delegate { CheckIdx(); UpdateUI(); CoolDown(); });
        UpdateUI();
    }
    public virtual void UpdateUI()
    {
        if (turn == 0)
        {
            turnObj.SetActive(false);
            plusObj.SetActive(true);
        }
        else
        {
            turnObj.SetActive(true);
            plusObj.SetActive(false);
            turnIdx.text = turn.ToString();
        }
    }
    public virtual void CheckIdx()
    {
        
    }
    private void CoolDown()
    {
        StartCoroutine(CoolDownCoroutine());
    }
    private IEnumerator CoolDownCoroutine()
    {
        isActive = false;
        cntDown.SetActive(true);
        activeBtn.interactable = false;
        timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            cntDownText.text = ((int)timer).ToString();
            yield return null;
        }
        activeBtn.interactable = true;
        cntDown.SetActive(false);
        isActive = true;
    }
}