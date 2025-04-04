using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ShopMallSave_Json
{
    static string SHOP_MALL_COIN_GEM = "SHOP_MALL_REWARD_DAILY";

    static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void SaveDataShopMallCoin_Gem(DataUserShop dataUserShop)
    {
        ShopMallCoin_Gem shopMallCoin_Gem = new();
        for(int i = 0; i < dataUserShop.LsIsRewardCollected.Count; i++)
        {
            shopMallCoin_Gem.lsShopMallRewardDaily.Add(dataUserShop.LsIsRewardCollected[i].isCollected);
        }
        string json = JsonConvert.SerializeObject(shopMallCoin_Gem);
        File.WriteAllText(GetFilePath(SHOP_MALL_COIN_GEM), json);
    }

    public static ShopMallCoin_Gem GetDataCoinGem()
    {
        var filePath = GetFilePath(SHOP_MALL_COIN_GEM);
        if(!File.Exists(filePath))
        {
            Debug.LogError("Null check");
            return new ShopMallCoin_Gem();
        }
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ShopMallCoin_Gem>(json);
    }
    /// <summary>
    /// 
    /// </summary>
    static string SHOP_MALL_REROLL = "SHOP_MALL_REROLL";
    public static void SaveDataShopMallReroll(DataUserShop dataUserShop)
    {
        ShopMallRerollControl shopMallRerollCtrl = new();
        foreach(var reroll in dataUserShop.LsDataShopReroll)
        {
            ShopMallReroll shopMallReroll = new ShopMallReroll(reroll.propertiesUnits.iD, reroll.currentCostAmount);

            shopMallRerollCtrl.lsShopMallRerolls.Add(shopMallReroll);
        }
        string json = JsonConvert.SerializeObject(shopMallRerollCtrl);
        File.WriteAllText(GetFilePath(SHOP_MALL_REROLL),json);
    }

    public static ShopMallRerollControl GetDataShopReroll()
    {
        var filePath = GetFilePath(SHOP_MALL_REROLL);
        if (!File.Exists(filePath)) return new ShopMallRerollControl();
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<ShopMallRerollControl>(json);
    }

}
[System.Serializable]
public class ShopMallCoin_Gem
{
    public List<bool> lsShopMallRewardDaily = new();
}

[System.Serializable]
public class ShopMallRerollControl
{
    public List<ShopMallReroll> lsShopMallRerolls = new();
}


[System.Serializable]
public class ShopMallReroll
{
    public int idUnit;
    public int currentCostAmount;


    public ShopMallReroll(int idUnit, int costAmount)
    {
        this.idUnit = idUnit;
        this.currentCostAmount = costAmount;
    }
}