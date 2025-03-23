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
    public RewardSpinData RewardSpinData => rewardData;

    PropertiesUnitsBase dataCurrentCard;
    public PropertiesUnitsBase DataCurrentCard => dataCurrentCard;

    Color colorNameUnit;
    public Color ColorNameUnit => colorNameUnit;


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
                GenerateRandomUnit();
                break;
        }
    }
    public void GenerateRandomUnit()
    {
        this.dataCurrentCard = this.GiveRandom();
        this.GiveRandomCard();
    }

    private void GiveRandomCard()
    {
        GameController.Instance.dataContain.dataUser.AddCards(dataCurrentCard, 1);
    }
     PropertiesUnitsBase GiveRandom()
    {
        DataUnits dataUnit = GameController.Instance.dataContain.dataUnits;
        List<PropertiesUnitsBase> lsResults = new();

        foreach (var child in dataUnit.lsPropertiesBases)
        {
            if (child.unitRank == rewardData.cardRank)
                lsResults.Add(child);
        }
        int rand = Random.Range(0, lsResults.Count);
        this.SetColorNameUnit(lsResults[rand]);
        this.dataCurrentCard = lsResults[rand];
        return dataCurrentCard;
    }
    public void SetColorNameUnit(PropertiesUnitsBase unitData)
    {
        switch (unitData.unitRank)
        {
            case UnitRank.Uncommon:
                this.colorNameUnit = Color.green;
                break;
            case UnitRank.Rare:
                this.colorNameUnit = new Color32(0, 122, 255, 255);
                break;
            case UnitRank.Epic:
                this.colorNameUnit = new Color32(175, 82, 222, 255);
                break;
            case UnitRank.Legend:
                this.colorNameUnit = new Color32(255, 159, 0, 255);
                break;
            default:
                this.colorNameUnit = Color.white;
                break;
        }
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

