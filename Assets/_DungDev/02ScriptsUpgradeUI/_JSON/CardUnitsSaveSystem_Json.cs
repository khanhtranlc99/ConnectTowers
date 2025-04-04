using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class CardUnitsSaveSystem_Json
{
    static string saveCardKey = "saveCardKey";
    
    static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
    public static void SaveDataCardInventory(DataUserGame dataUser)
    {
        CardInventorySystem cardInventory = new();
        foreach (var card in dataUser.LsDataUnitsCard)
        {
            DataCard cardData = new DataCard(card.unit.iD, card.cardCount, card.unit.currentLevel, card.unit.starLevel);
            cardInventory.lsCards.Add(cardData);
        }
        cardInventory.id_Soldier = dataUser.CurrentCardSoldier.iD;
        cardInventory.id_Beast = dataUser.CurrentCardBeast.iD;
        cardInventory.id_Mage = dataUser.CurrentCardMage.iD;
        
        string json = JsonConvert.SerializeObject(cardInventory);
        File.WriteAllText(GetFilePath(saveCardKey),json);
    }
    public static CardInventorySystem GetDataCardInventory()
    {
        var filePath = GetFilePath(saveCardKey);
        if (!File.Exists(filePath))
        {
            Debug.LogError("saveCardKey null");
            return new CardInventorySystem();
        }
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<CardInventorySystem>(json);
    }
}
[System.Serializable]
public class CardInventorySystem
{
    public List<DataCard> lsCards = new();
    public int id_Soldier;
    public int id_Beast;
    public int id_Mage;
}

[System.Serializable]
public class DataCard
{
    public int id; // card
    public int cardCount;
    public int level;
    public int star;

    public DataCard(int id, int cardCount, int level, int star)
    {
        this.id = id;
        this.cardCount = cardCount;
        this.level = level;
        this.star = star;
    }
}