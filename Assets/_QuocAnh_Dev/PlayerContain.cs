using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerContain : MonoBehaviour
{
    public InputController inputCtrl;
    public BuildingController buildingCtrl;
    public UnitController unitCtrl;
    public void Init()
    {
        buildingCtrl.Init();
    }

   


}
