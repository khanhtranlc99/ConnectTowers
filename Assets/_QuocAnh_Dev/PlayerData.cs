using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class PlayerData
{
    public int Hp;
    public bool isLive;
    private int _gold;
    public int gold
    {
        get => _gold;
        set
        {
            if(_gold != value)
            {
                _gold = value;
                Debug.LogError("Show Gold");
            }
        }
    }
    private int _gem;
    public int gem
    {
        get => _gem;
        set
        {
            if(_gem!= value)
            {
                _gem = value;
                Debug.LogError("Show Gem"); 
            }
        }
    }
    [PropertyOrder(1)]
    [ReadOnly]
    public int unitSoldierId = 1;
    [PropertyOrder(1)]
    [Min(0)]
    public int unitSoldierLv = 1;
    [PropertyOrder(3)]
    [ReadOnly]
    public int unitTankId = 2;
    [PropertyOrder(3)]
    [Min(0)]
    public int unitTankLv = 1;
    [PropertyOrder(5)]
    [ReadOnly]
    public int unitMageId = 3;
    [PropertyOrder(5)]
    [Min(0)]
    public int unitMageLv = 1;
#if UNITY_EDITOR
    [PropertyOrder(0)]
    [ValueDropdown("GetSoldierName")]
    [OnValueChanged("GetUnitId")]
    public string soldierUnit;
    public List<string> GetSoldierName()
    {
        List<string> list = new List<string>();
        foreach(var item in UnitData.Instance.unitBases)
        {
            if(item.unitType == UnitType.Solider)
            {
                list.Add(item.name);
            }
        }
        return list;
    }
    public void GetUnitId()
    {
        foreach(var item in UnitData.Instance.unitBases)
        {
            if(soldierUnit == item.name)
            {
                if(item.unitType == UnitType.Solider)
                {
                    unitSoldierId = item.id;
                }
            }
        }
    }
    [PropertyOrder(2)]
    [ValueDropdown("GetTankName")]
    [OnValueChanged("GetTankId")]
    public string tankUnit;
    public List<string> GetTankName()
    {
        List<string> list = new List<string>();
        foreach(var item in UnitData.Instance.unitBases)
        {
            if(item.unitType == UnitType.Tank)
            {
                list.Add(item.name);
            }
        }
        return list;
    }
    public void GetTankId()
    {
        foreach(var item in UnitData.Instance.unitBases)
        {
            if(tankUnit == item.name)
            {
                if(item.unitType == UnitType.Tank)
                {
                    unitTankId = item.id;
                }
            }
        }
    }
    [PropertyOrder(4)]
    [ValueDropdown("GetMageName")]
    [OnValueChanged("GetMageId")]
    public string mageUnit;
    public List<string> GetMageName()
    {
        List<string> list = new List<string>();
        foreach (var item in UnitData.Instance.unitBases)
        {
            if (item.unitType == UnitType.Mage)
            {
                list.Add(item.name);
            }
        }
        return list;
    }
    public void GetMageId()
    {
        foreach (var item in UnitData.Instance.unitBases)
        {
            if (mageUnit == item.name)
            {
                if (item.unitType == UnitType.Mage)
                {
                    unitMageId = item.id;
                }
            }
        }
    }

#endif
    public int GetEquipUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Solider:
                return unitSoldierId;
            case UnitType.Tank:
                return unitTankId;
            case UnitType.Mage:
                return unitMageId;
        }
        return -1;
    }
    public List<PlayerUnitData> playerUnitsDatas;
    public PlayerUnitData GetUnitInfo(int id)
    {
        foreach(var item in playerUnitsDatas)
        {
            if(item.unitId == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class PlayerUnitData
{
    public int unitId;
    public int level;
    public PlayerUnitData(int unit, int level)
    {
        this.unitId = unit;
        this.level = level;
    }
}
