using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum RewardSpinType
{
    Coin,
    Gem,
    Card,
}

public class WheelSpinSlot : MonoBehaviour
{
    [SerializeField] private RewardSpinData rewardData;
    public void GrantReward()
    {
        switch (rewardData.rewardWheelType)
        {
            case RewardSpinType.Coin:
                GameController.Instance.dataContain.dataUser.AddCoins(rewardData.amount);
                break;
            case RewardSpinType.Gem:
                GameController.Instance.dataContain.dataUser.AddGems(rewardData.amount);
                break;
            case RewardSpinType.Card:
                GiveRandomCard();
                break;
        }
    }

    private void GiveRandomCard()
    {
        DataUnits dataUnit = GameController.Instance.dataContain.dataUnits;
        List<PropertiesUnitsBase> lsResults = new();

        foreach (var child in dataUnit.lsPropertiesBases)
        {
            if (child.unitRank == rewardData.cardRank)
                lsResults.Add(child);
        }

        if (lsResults.Count == 0)
        {
            Debug.LogWarning("Không tìm thấy card nào phù hợp!");
            return;
        }

        int rand = Random.Range(0, lsResults.Count);
        GameController.Instance.dataContain.dataUser.AddCards(lsResults[rand], rewardData.amount);
    }
}

[System.Serializable]
public class RewardSpinData
{
    [OnValueChanged("OnRewardTypeChanged")]
    public RewardSpinType rewardWheelType;

    [ShowIf("rewardWheelType", RewardSpinType.Card)]
    public UnitRank cardRank;

    [DisableIf("rewardWheelType", RewardSpinType.Card)]
    public int amount = 1; // Mặc định là 1

    private void OnRewardTypeChanged()
    {
        if (rewardWheelType == RewardSpinType.Card)
            amount = 1; // Đặt lại giá trị nếu chọn Card
    }

    public override string ToString()
    {
        return $"Reward: {rewardWheelType}, Amount: {amount}, Rank: {cardRank}";
    }
}

