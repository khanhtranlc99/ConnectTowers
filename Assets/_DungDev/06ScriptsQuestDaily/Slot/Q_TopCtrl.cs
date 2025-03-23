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
    private void OnEnable()
    {
        this.RegisterListener(EventID.UPDATE_PROGESSBAR_QUEST, UpdateUI);
        this.UpdateUI(null);
        this.ResetDailyQuest();
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_PROGESSBAR_QUEST, UpdateUI);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_PROGESSBAR_QUEST, UpdateUI);
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
        float step = total / lsDailySlots.Count;

        for (int i = 0; i < lsDailySlots.Count; i++)
        {
            if (lsDailySlots[i].isClaim)
            {
                lsDailySlots[i].DisableEffect();
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
