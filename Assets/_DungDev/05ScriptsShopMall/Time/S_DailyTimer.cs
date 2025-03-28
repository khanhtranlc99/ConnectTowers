using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DailyTimer : LoadAutoComponents
{
    [SerializeField] TextMeshProUGUI txtCountTime;
    public bool wasCountTime = false;
    public float countTime;

    private void Update()
    {
        if (!wasCountTime) return;

        if(this.countTime > 0)
        {
            countTime -= Time.deltaTime;
            txtCountTime.text = TimeManager.ShowTime2((long)countTime);
        }
        else
        {
            txtCountTime.text = "What up";
        }
    }
    
    public void ResetDay()
    {
        wasCountTime = false;
        countTime = TimeManager.TimeLeftPassTheDay(System.DateTime.Now);
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.txtCountTime = transform.Find("TextTimer").GetComponent<TextMeshProUGUI>();
    }

}
