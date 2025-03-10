using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


[CreateAssetMenu(menuName = "PropertiesBase/ Super_QuackData")]

public class Super_QuackData : PropertiesBase
{
    public float increase_Speed;
    public float dupllice;
    public float increase_Attack;
    public float increase_Gold;

    public int double_Shield_0;
    public int bonus_Attack;
    public int dash;
    public int double_Shield_1;
    public int critical_Hit;

    public Super_QuackUpgrade super_QuackUpgrade;
}
