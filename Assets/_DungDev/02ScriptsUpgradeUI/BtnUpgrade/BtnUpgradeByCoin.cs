using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnUpgradeByCoin : BtnUpgradeBase
{
    [SerializeField] TextMeshProUGUI textCoin;

    private void OnEnable()
    {
        this.UpdateUI();
    }
    public void UpdateUI()
    {
        if (UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot == null) return;
        PropertiesUnitsBase unitData = UpgradeBoxCtrl.Instance.CurrentCard?.EquippedUnitSlot?.GetUnit();
        this.textCoin.text = unitData.GetUpgradeCostCoin.ToString();
    }

    public override void OnClick()
    {
        if (!IsCanUpgrade()) return;

        UnitSlotBase unitToUpgrade = UpgradeBoxCtrl.Instance.GetEquippedUnit();
        if(unitToUpgrade != null) unitToUpgrade.UpgradeLevelUnit();
        UpgradeBoxCtrl.Instance.CurrentCard.UpdateUI();

        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.UpdateUI();

    }

    bool IsCanUpgrade()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        PropertiesUnitsBase unitData = UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot.GetUnit();

        if (UseProfile.D_COIN < unitData.GetUpgradeCostCoin) return false;
        this.UpgradeUnit(dataUser, unitData);
        GameController.Instance.dataContain.dataUser.DataDailyQuest.IncreaseQuestProgress(QuestType.UpgradeUnit, 1);
        return true;
    }

    void UpgradeUnit(DataUserGame dataUser, PropertiesUnitsBase unitData)
    {
        dataUser.DeductCoin(unitData.GetUpgradeCostCoin);
    }
}
