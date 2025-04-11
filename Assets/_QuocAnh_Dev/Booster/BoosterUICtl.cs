using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterUICtl : MonoBehaviour
{
    public List<BoosterButton> boosterLists = new List<BoosterButton>();
    public void Init()
    {
        foreach (var item in boosterLists)
        {
            item.Init();
        }
    }
}
