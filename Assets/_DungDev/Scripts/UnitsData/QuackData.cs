using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PropertiesBase/ QuackData")]

public class QuackData : PropertiesBase
{
    public float increase_Speed;
    public float dupllice;

    public int bonus_Move_Speed_0;
    public int bonus_Attack_To_All;
    public int bonus_Move_Speed_1;
    public int bonus_Dupllicate_To_All;
    public int bonus_health_To_All;


    public QuackUpgrade quackUpgrade;
}
