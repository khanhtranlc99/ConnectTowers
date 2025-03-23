using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USER/UserDataShop")]

public class DataUserShop : ScriptableObject
{
    public Dictionary<string, ItemState> dicItemStates = new();

    public bool CanReceiveItem(string itemID)
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if(dicItemStates.ContainsKey(itemID)) return dicItemStates[itemID].receivedDate != today;

        return true;
    }

    public void ReceiveItem(string itemID)
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if(!dicItemStates.ContainsKey(itemID)) dicItemStates[itemID] = new ItemState();

        dicItemStates[itemID].isReceived = true;
        dicItemStates[itemID].receivedDate = today;
    }


    public void CheckAndResetDailyItems()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        foreach (var item in dicItemStates)
        {
            if(item.Value.receivedDate != today) item.Value.isReceived = false;
        }
    }


}

[System.Serializable]
public class ItemState
{
    public bool isReceived;
    public string receivedDate;
    
}
