using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardUnitType
{
    Soldier,
    Beast,
    Mage,
}


public abstract class BaseCardCtrl : MonoBehaviour
{
    [SerializeField] protected CardUnitType cardUnitType;
    public CardUnitType CardUnitType => cardUnitType;

    [SerializeField] protected UnitSlotBase selectedUnit;
    [SerializeField] protected UnitSlotBase equippedUnitSlot;
    public UnitSlotBase EquippedUnitSlot => equippedUnitSlot;

    [SerializeField] Button equiqqButton;
    [SerializeField] Button upgradeGoldButton;
    [SerializeField] Button upgradeGemButton;


    private void OnEnable()
    {
        this.equippedUnitSlot = this.SetInitCardEquipped();
        this.selectedUnit = this.SetInitCardEquipped();
        this.UpdateUI();
    }
    protected abstract UnitSlotBase SetInitCardEquipped();
    // selectUnit de xem info
    public virtual void SelectUnit(UnitSlotBase baseUnitSlot)
    {
        this.selectedUnit = baseUnitSlot;
        equiqqButton.gameObject.SetActive(true);
        UpdateUI();

        UpgradeBoxCtrl.Instance.SetCurrentActiveCard(this);
    }
    public abstract void UpdateTickMarks();

    public virtual void EquipSelectedUnit()
    {
        if (selectedUnit == null) return;
        equippedUnitSlot = selectedUnit;

        switch (cardUnitType)
        {
            case CardUnitType.Soldier:
                GameController.Instance.dataContain.dataUser.SetCurrentCardSoldier(equippedUnitSlot.GetUnit());
                break;
            case CardUnitType.Beast:
                GameController.Instance.dataContain.dataUser.SetCurrentCardBeast(equippedUnitSlot.GetUnit());
                break;
            case CardUnitType.Mage:
                GameController.Instance.dataContain.dataUser.SetCurrentCardMage(equippedUnitSlot.GetUnit());
                break;
        }

        UpdateUI();
    }

    public virtual void UpdateUI()
    {
        this.UpdateUIInfoBox();
        UpdateInfoDisplayTop();
        UpdateTickMarks();

        if (selectedUnit != null)
        {
            PropertiesUnitsBase unit = selectedUnit.GetUnit();
            UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox
                .SetName_LevelUnits(unit.unitType.ToString(), "Level: " + unit.currentLevel.ToString());
            //UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox.SetSpriteStar(selectedUnit.unitsType);
            UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox.UpdateUI(selectedUnit.unitsType);
        }

        if (selectedUnit == equippedUnitSlot)
        {
            equiqqButton.gameObject.SetActive(false);
            upgradeGoldButton.gameObject.SetActive(true);
            upgradeGemButton.gameObject.SetActive(true);
        }
        else
        {
            equiqqButton.gameObject.SetActive(true);
            upgradeGoldButton.gameObject.SetActive(false);
            upgradeGemButton.gameObject.SetActive(false);
        }
    }

    public void UpdateUIInfoBox()
    {
        UpgradeBoxCtrl.Instance.BottomCtrl.InfoBox
            .SetInfoBox(selectedUnit.unitsType);
        equippedUnitSlot.SetInfoUnit(equippedUnitSlot.unitsType);

    }
    public void UpdateInfoDisplayTop()
    {
        equippedUnitSlot.DisplayTopUnit.UpdateUI(equippedUnitSlot.GetUnit());

    }
}
