using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[System.Serializable]
public class PlayerUnit
{
    public UnitBase unit;
    private int lv;
    public int Lv
    {
        get => lv;
        set
        {
            lv = value;
            if (lv > 50)
            {
                lv= 50;
            }
            power = unit.Power + lv * unit.PowerPerLv;
            
        }
    }
    public int power;
    public PlayerUnit(UnitBase unit, int lv)
    {
        this.unit = unit;
        this.lv = lv;
        this.power = unit.Power + lv * unit.PowerPerLv;
    }
    public bool Selected()
    {
        return unit.id == GamePlayController.Instance.gameManager.PlayerData.GetEquipUnit(unit.unitType);
    }
}
