using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpinCtrl : MonoBehaviour
{
    [SerializeField] Transform wheelTrans;
    [SerializeField] Button btnStartSpin;
    [SerializeField] int stopAt;
    int rand;
    [SerializeField] float speedRotate = 700f;
    [SerializeField] float speedStop = 7f;
    [SerializeField] bool isStop = true; // stop khi co ket qua

    private void Start()
    {
        btnStartSpin.onClick.AddListener(StartSpin);
    }

    private void FixedUpdate()
    {
        this.Spinning();
    }

    void Stoping()
    {
        if (!this.isStop) return;

        if(this.stopAt == rand) this.isStop = false;
        this.speedRotate -= speedStop;
        if (this.speedRotate < 0)
        {
            this.speedRotate = 0;
            this.GetReward();
        } 
    }

    void Spinning()
    {
        if (this.isStop) return;
        this.Stoping();
        this.wheelTrans.Rotate(0,0, speedRotate * Time.fixedDeltaTime);
    }

    public void StartSpin()
    {
        this.isStop = false;
        StartCoroutine(WaitForResult());
    }

    IEnumerator WaitForResult()
    {
        yield return new WaitForSeconds(2f);
        rand = Random.Range(0,10);
        this.stopAt = rand;
        this.isStop = true;
    }
    public void GetReward()
    {
        float rot = wheelTrans.eulerAngles.z % 360; 
        int reward = (int)((rot / 36) + 1) * 100;
        Win(reward);
    }

    public void Win(int score)
    {
        Debug.Log("Win: " + score);
    }

}
