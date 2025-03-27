using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_GemCoinTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtTimer;
    float remainingTime;

    private void OnEnable()
    {
        remainingTime = TimeManager.TimeLeftPassTheDay(DateTime.Now);
    }

    private void Update()
    {
        remainingTime -= 1 * Time.deltaTime;
        txtTimer.text = TimeManager.ShowTime2((long)remainingTime);
    }

}
