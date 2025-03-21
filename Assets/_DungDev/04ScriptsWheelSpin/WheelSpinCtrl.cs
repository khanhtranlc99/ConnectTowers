using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventDispatcher;

public class WheelSpinCtrl : MonoBehaviour
{
    [SerializeField] Transform wheelTrans;
    [SerializeField] List<WheelSpinSlot> lsWheelSpinSlots = new();
    [SerializeField] Button btnSpin;

    [Space(10)]
    [SerializeField] Sprite imgCoin;
    [SerializeField] Sprite imgGem;
    [SerializeField] DisplayResult panelDisplayResult;
    int[] rewardAngle = { 36*4, 216, 36 * 8, 36 * 9, 36 * 7, 0, 36 * 5, 36 * 1, 36 * 3, 36 * 2 };

    int rand;
    int resultID;
    float finalAngle;
    public IEnumerator SpinningWheel()
    {
        this.btnSpin.interactable = false;
        resultID = GetRandomResult();
        finalAngle = rewardAngle[resultID];

        Debug.Log(lsWheelSpinSlots[resultID] + "Complete");
        // xoay ngau nhien 5 vong
        Tween wheelTween = wheelTrans.DORotate(new Vector3(0,0,+360 * 5 + finalAngle),3f,RotateMode.FastBeyond360);
        yield return wheelTween.WaitForCompletion();
        lsWheelSpinSlots[resultID].GrantReward();
        this.AnimRewardResult(lsWheelSpinSlots[resultID]);

        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.btnSpin.interactable = true;
    }

    void AnimRewardResult(WheelSpinSlot slot)
    {
        this.panelDisplayResult.gameObject.SetActive(true);
        switch (slot.RewardSpinData.rewardWheelType)
        {
            case RewardSpinType.Coin:
                panelDisplayResult.UpdateUI(imgCoin, slot.RewardSpinData.amount.ToString());
                break;
            case RewardSpinType.Gem:
                panelDisplayResult.UpdateUI(imgGem, slot.RewardSpinData.amount.ToString());
                break;
            default:
                panelDisplayResult.UpdateUI(slot.DataCurrentCard.SpriteUnit, slot.RewardSpinData.amount.ToString());
                break;
        }
        panelDisplayResult.transform.localScale = Vector3.zero;
        panelDisplayResult.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    int GetRandomResult()
    {
        rand = Random.Range(0, 101); // Random từ 0 - 100

        if (rand < 30) return 0;  // 100 Coins (30%)
        if (rand < 50) return 1;  // 500 Coins (20%)
        if (rand < 60) return 2;  // 2000 Coins (10%)
        if (rand < 67) return 3;  // Common Unit (7%)
        if (rand < 73) return 4;  // Uncommon Unit (6%)
        if (rand < 80) return 5;  // 500 Gems (7%)
        if (rand < 86) return 6;  // Epic Unit (6%)
        if (rand < 91) return 7;  // Rare Unit (5%)
        if (rand < 95) return 8;  // Chest (4%)
        return 9;                 // 100 Gems (5%)
    }

}
