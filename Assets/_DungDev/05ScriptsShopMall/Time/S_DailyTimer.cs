using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DailyTimer : LoadAutoComponents
{
    [SerializeField] TextMeshProUGUI txtCountTime;
    public float countTime;

    public void Init()
    {
        countTime = TimeManager.TimeLeftPassTheDay(System.DateTime.Now);
    }
    private void Update()
    {
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

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.txtCountTime = transform.Find("TextTimer").GetComponent<TextMeshProUGUI>();
    }

}
