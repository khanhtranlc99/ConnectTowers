using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,
    Gem,
    Common,
    Rare,
    Epic,
}

[CreateAssetMenu(menuName = "SpineWheel/SpineRate")]
public class SpineRateSO : ScriptableObject
{
    public List<ItemSpine> lsItemSpines;

    public ItemSpine GetItemSpine(ItemType itemType)
    {
        foreach (var child in this.lsItemSpines)
        {
            if (child.itemType == itemType) return child;
        }
        return null;
    }
}

[System.Serializable]
public class ItemSpine
{
    public int idItem;
    public ItemType itemType;
    public int itemCount;
    public int itemRare;
}
