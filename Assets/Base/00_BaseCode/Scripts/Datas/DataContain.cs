using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContain : MonoBehaviour
{
    public GiftDatabase giftDatabase;
    //public LevelChestData levelChestData;
    public DataUnits dataUnits;
    public DataTowersCtrl dataTowersCtrl;
    public DataUserGame dataUser;

    public void InitData()
    {
        dataUser.LoadCardInventoryData();
        dataUser.DataShop.LoadShopMallCoin_GEM();
        dataUser.DataShop.LoadShopMallReroll();
        dataUser.DataDailyQuest.LoadQuestData();
        dataUser.DataDailyQuest.LoadQuestTracker();
        dataUser.DataUserVip.LoadVipData();
        dataUser.DataUserVip.LoadVipDataDaily();
        dataUser.DataOfflineRewardChest.CaculateHourOffine();
    }

    private void OnApplicationQuit()
    {
        ShopMallSave_Json.SaveDataShopMallReroll(dataUser.DataShop);
        ShopMallSave_Json.SaveDataShopMallCoin_Gem(dataUser.DataShop);
        QuestDailySave_Json.SaveDataQuestTopTracker(dataUser.DataDailyQuest);
        QuestDailySave_Json.SaveDataQuestDaily(dataUser.DataDailyQuest);

        CardUnitsSaveSystem_Json.SaveDataCardInventory(dataUser);
        VipRewardSaveSystem.SaveDataReward(dataUser.DataUserVip.LsRewardSystems);
        VipRewardSaveSystem.SaveDataRewardDaily(dataUser.DataUserVip.LsRewardDailySystems);
        UseProfile.OffineRewardTime = System.DateTime.Now;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {

            ShopMallSave_Json.SaveDataShopMallReroll(dataUser.DataShop);
            ShopMallSave_Json.SaveDataShopMallCoin_Gem(dataUser.DataShop);
            QuestDailySave_Json.SaveDataQuestTopTracker(dataUser.DataDailyQuest);
            QuestDailySave_Json.SaveDataQuestDaily(dataUser.DataDailyQuest);

            CardUnitsSaveSystem_Json.SaveDataCardInventory(dataUser);
            VipRewardSaveSystem.SaveDataReward(dataUser.DataUserVip.LsRewardSystems);
            VipRewardSaveSystem.SaveDataRewardDaily(dataUser.DataUserVip.LsRewardDailySystems);

            UseProfile.OffineRewardTime = System.DateTime.Now;

            Debug.LogError("PAUSE + SAVE DATA COMPLETE");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            
            ShopMallSave_Json.SaveDataShopMallReroll(dataUser.DataShop);
            ShopMallSave_Json.SaveDataShopMallCoin_Gem(dataUser.DataShop);
            QuestDailySave_Json.SaveDataQuestTopTracker(dataUser.DataDailyQuest);
            QuestDailySave_Json.SaveDataQuestDaily(dataUser.DataDailyQuest);

            CardUnitsSaveSystem_Json.SaveDataCardInventory(dataUser);
            VipRewardSaveSystem.SaveDataReward(dataUser.DataUserVip.LsRewardSystems);
            VipRewardSaveSystem.SaveDataRewardDaily(dataUser.DataUserVip.LsRewardDailySystems);
            UseProfile.OffineRewardTime = System.DateTime.Now;


            Debug.LogError("PAUSE + SAVE DATA COMPLETE");
        }
    }
}
