using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/Super_QuackData")]
public class Super_QuackData : PropertiesUnitsBase
{

    [Space(10)]
    [Header("Super_QuackData")]
    [SerializeField] float increase_Speed;
    [SerializeField] float dupllice;
    [SerializeField] float increase_Attack;
    [SerializeField] float increase_Gold;

    // Các biến rút gọn
    [SerializeField] int bonus_Attack;
    [SerializeField] int dash;
    [SerializeField] int critical_Hit;

    public Super_QuackUpgrade super_QuackUpgrade;


    public override float GetSkillValue(string name)
    {
        if (name == "Increase speed")
            return GetIncrease_Speed;
        if (name == "Increase attack")
            return GetIncrease_Attack;
        if (name == "Dupllice")
            return GetDupllice;
        if (name == "Increase gold")
            return GetIncrease_Gold;
        return base.GetSkillValue(name);
    }

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed + 0.7f * (float)currentLevel; }
    }

    public float GetDupllice
    {
        get { return dupllice + 0.2f * (float)currentLevel; }
    }

    public float GetIncrease_Attack
    {
        get { return increase_Attack + 0.5f * (float)currentLevel; }
    }

    public float GetIncrease_Gold
    {
        get { return increase_Gold + 3f * (float)currentLevel; }
    }

    public float GetDouble_Shield_0
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.double_Shield_0; }
    }

    public float GetDouble_Shield_1
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.double_Shield_1; }
    }

    public float GetBonus_Attack
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.bonus_Attack + bonus_Attack; }
    }

    public float GetDash
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.dash + dash; }
    }

    public float GetCritical_Hit
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.critical_Hit + critical_Hit; }
    }
}
