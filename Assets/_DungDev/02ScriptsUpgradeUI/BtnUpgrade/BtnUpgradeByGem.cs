using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnUpgradeByGem : BtnUpgradeBase
{
    [SerializeField] TextMeshProUGUI textGem;

    private void OnEnable()
    {
        this.UpdateUI();
    }
    public void UpdateUI()
    {
        PropertiesUnitsBase unitData = UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot.GetUnit();
        this.textGem.text = unitData.GetUpgradeCostGem.ToString();
    }

    public override void OnClick()
    {
        if (!IsCanUpgrade()) return;

        UnitSlotBase unitToUpgrade = UpgradeBoxCtrl.Instance.GetEquippedUnit();
        if (unitToUpgrade != null) unitToUpgrade.UpgradeStarUnit();
        UpgradeBoxCtrl.Instance.CurrentCard.UpdateUI();
        UpgradeBoxCtrl.Instance.CoinGemInfo.UpdateUI();

        this.UpdateUI();
    }

    bool IsCanUpgrade()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        PropertiesUnitsBase unitData = UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot.GetUnit();
        if (dataUser.Gem < unitData.GetUpgradeCostGem) return false;
        if (dataUser.FindUnitCard(unitData).cardCount < unitData.GetCostCard) return false;

        this.UpgradeUnit(dataUser, unitData);
        return true;
    }

    void UpgradeUnit(DataUserGame dataUser, PropertiesUnitsBase unitData)
    {
        dataUser.DeductCard(unitData.GetCostCard);
        dataUser.DeductGem(unitData.GetUpgradeCostGem);
        UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox.UpdateUI(unitData.unitType);

    }
}
