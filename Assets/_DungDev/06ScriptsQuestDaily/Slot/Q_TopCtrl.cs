using EventDispatcher;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q_TopCtrl : MonoBehaviour
{
    [SerializeField] Image progressBar;
    [Space(10)]
    [SerializeField] List<Q_DailySlot> lsDailySlots = new();
    public List<Q_DailySlot> LsDailySlots => lsDailySlots;

    public void Init()
    {
        this.UpdateUI(null);
        this.ResetDailyQuest();
    }
    public void UpdateUI(object param)
    {

        float current = (float)GameController.Instance.dataContain.dataUser.DataDailyQuest.CurrentTotalRewardAmount;
        float total = (float)GameController.Instance.dataContain.dataUser.DataDailyQuest.TotalRewardAmount;
        this.progressBar.fillAmount = current / total;
        this.UpdateRewardSlots(current, total);
    }

    private void UpdateRewardSlots(float current, float total)
    {
        var dataQuest = GameController.Instance.dataContain.dataUser.DataDailyQuest;
        float step = total / (float)lsDailySlots.Count;
        for (int i = 0; i < lsDailySlots.Count; i++)
        {
            if (dataQuest.lsDailyTracker[i])
            {
                lsDailySlots[i].HiddenRewardSlot();
                continue;
            }
            if (current >= (i + 1) * step)
            {
                lsDailySlots[i].EnableEffect();
                lsDailySlots[i].SetActiveBtn(true);

            }
            else
            {
                lsDailySlots[i].DisableEffect();
                lsDailySlots[i].SetActiveBtn(false);
            }
        }
    }

    public void ResetDailyQuest()
    {
        if (GameController.Instance.dataContain.dataUser.DataDailyQuest.CurrentTotalRewardAmount > 0) return;
        foreach(var child in this.lsDailySlots)
        {
            child.ResetValueDaily();
        }
    }


    #region Odin

    [Button("SetUp Amount")]
    void SetUp()
    {
        for(int i = 0; i < lsDailySlots.Count; i++)
        {
            lsDailySlots[i].id = i;

            switch (i)
            {
                case 0:
                    lsDailySlots[i].SetAmountReward(10);
                    break;
                case 1:
                    lsDailySlots[i].SetAmountReward(20);
                    break;
                case 2:
                    lsDailySlots[i].SetAmountReward(50);
                    break;
                case 3:
                    lsDailySlots[i].SetAmountReward(100);
                    break;
                case 4:
                    lsDailySlots[i].SetAmountReward(500);
                    break;
            }
                
        }

        
    }
    #endregion
}
