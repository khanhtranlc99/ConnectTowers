
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PropertiesUnitsBase : ScriptableObject
{
    public int iD;
    public UnitsType unitType;
    public UnitRank unitRank;
    public int currentLevel;
    public int starLevel;

    [Header("Real properties")]
    public int hp;
    public int atk;
    public float speed;

    [Space(10)]
    [SerializeField] Sprite spriteUnit;
    public Sprite SpriteUnit => spriteUnit;

    [SerializeField] Sprite boxRank;
    public Sprite BoxRank => boxRank;

    [SerializeField] Sprite frameRank;
    public Sprite FrameRank => frameRank;

    [Space(10)]
    [Header("SetUp Cost Upgrade Unit")]
    [SerializeField] protected int baseCoinCost;
    [SerializeField] protected float costCoinPerLevel;

    [SerializeField] protected int baseGemCost;
    [SerializeField] protected float costGemPerStar;

    [SerializeField] protected int baseCardCost;
    /// <summary>
    /// Odin inspeector setup
    /// </summary>
    ///
    #region OdinInspector
    private Dictionary<UnitRank, (int baseCoin, float costPerLevel, int baseGem, float costPerStar, int baseCardCost)> rankMultipliers =
        new Dictionary<UnitRank, (int, float, int, float, int)>()
        {
            { UnitRank.Common,    (100, 1.0f, 20, 1.2f,2) },
            { UnitRank.Uncommon,  (200, 1.2f, 40, 1.5f,4) },
            { UnitRank.Rare,      (500, 1.5f, 100, 1.8f,8) },
            { UnitRank.Epic,      (1000, 1.8f, 200, 2.0f,12) },
            { UnitRank.Legend,    (2000, 2.2f, 400, 2.5f,20) }
        };
    [Button("Auto Setup Costs")]
    private void AutoSetupCosts()
    {
        if (rankMultipliers.ContainsKey(unitRank))
        {
            var (coinBase, coinLevel, gemBase, gemStar, cardBase) = rankMultipliers[unitRank];

            baseCoinCost = coinBase;
            costCoinPerLevel = coinLevel;
            baseGemCost = gemBase;
            costGemPerStar = gemStar;
            baseCardCost = cardBase;
            Debug.Log($"[AutoSetup] {unitRank}: CoinCost={baseCoinCost}, CoinPerLevel={costCoinPerLevel}, GemCost={baseGemCost}, GemPerStar={costGemPerStar}");
        }
        else
        {
            Debug.LogWarning("Rank không hợp lệ, không thể auto setup!");
        }
    }

    [Button("Reset level va Star")]
    private void ResetLevelUnit()
    {
        this.currentLevel = 1;
        this.starLevel = 0;
    }
    #endregion

    [Header("Special Skills")]
    public List<UnitSpecialSkill> lsUnitSpecialSkills = new();

    [Header("Star Level Bonus")]
    public List<StarLevelBonus> lsStartLevelBonus = new();
    public virtual float GetSkillValue(string name)
    {
        foreach (var child in lsUnitSpecialSkills)
        {
            if(child.skillName.Equals(name)) return child.skillValue;
        }
        return 0f;
    }

    //cost upgrade unit
    public virtual int GetUpgradeCostCoin => (int)(baseCoinCost * costCoinPerLevel*(1.1f + 0.02f *(float)currentLevel));
    public virtual int GetUpgradeCostGem => (int)(baseGemCost * costGemPerStar * (1.1f + 0.02f *(float)starLevel + 1));

    public virtual int GetCostCard => Mathf.RoundToInt(baseCardCost * Mathf.Pow((1.5f + 0.2f * starLevel), starLevel));
}

[System.Serializable]
public class StarLevelBonus
{
    public string bonusName;  
    public float bonusValue;  
}

[System.Serializable]
public class UnitSpecialSkill
{
    public string skillName;  
    public float skillValue;  
    public Sprite skillIcon;  
}
