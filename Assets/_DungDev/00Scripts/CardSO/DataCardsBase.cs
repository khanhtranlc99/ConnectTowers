using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Common,
    UnCommon,
    Rare,
    Epic,
    Legend
}


[CreateAssetMenu(menuName = "DataCardsBase/DataCardsBase")]
public class DataCardsBase : ScriptableObject
{
    public List<DataCardsRate> lsDataCardsRate = new();

    public DataCardsRate GetCardRandomRate()
    {
        int totalProbability = 0;


        foreach (var child in this.lsDataCardsRate)
        {
            totalProbability += child.rate;
        }

        float randomValue = Random.Range(0,totalProbability);

        int currentSum = 0;

        foreach (var child in lsDataCardsRate)
        {
            currentSum += child.rate;
            if(randomValue < currentSum) return child;
        }
        return null;
    }



}

[System.Serializable]
public class DataCardsRate
{
    public CardType cardType;
    public int rate;
}

