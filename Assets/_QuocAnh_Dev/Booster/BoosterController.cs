using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
    public List<BoosterBase> boosterList;
    public void ActiveBooster(BoosterType boosterType)
    {
        switch (boosterType)
        {
            case BoosterType.Meteor:
                boosterList[0].Active();
                break;
            case BoosterType.ArrowRain:
                boosterList[1].Active();
                break;
            case BoosterType.Freeze:
                boosterList[2].Active();
                break;
            case BoosterType.HealingUp:
                boosterList[3].Active();
                break;
            case BoosterType.SpeedUp:
                boosterList[4].Active();
                break;
            case BoosterType.SpawnsUp:
                boosterList[5].Active();
                break;
        }
    }
    public BoosterBase GetBooster(BoosterType boosterType)
    {
        foreach (var item in boosterList)
        {
            if (item.boosterName == boosterType)
            {
                return item;
            }
        }
        return null;
    }
    public void AddBuff(BoosterType boosterType)
    {
        foreach (var item in boosterList)
        {
            if (item.boosterName == boosterType)
            {
                item.Active();
                break;
            }
        }
    }
}

public enum BoosterType
{
    Meteor,
    ArrowRain,
    Freeze,
    HealingUp,
    SpeedUp,
    SpawnsUp
}
