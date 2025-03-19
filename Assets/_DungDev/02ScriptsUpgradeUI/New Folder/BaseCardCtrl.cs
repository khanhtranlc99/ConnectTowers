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
        ///TODO:
        ///start game set selectCard, equiq card
    }

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

        UpdateUI();
        UpdateTickMarks();
    }
    public void SetDisPlayInfoTop()
    {
        equippedUnitSlot.DisplayTopUnit.SetInfo(equippedUnitSlot.Icon.sprite,
            equippedUnitSlot.BG.sprite, equippedUnitSlot.BoxLevel.sprite,
            equippedUnitSlot.RankUnit, equippedUnitSlot.CurrentLevel);

        equippedUnitSlot.DisplayTopUnit.SetSpriteStar(equippedUnitSlot.unitsType);
        equippedUnitSlot.SetSpriteStar(equippedUnitSlot.unitsType);
    }
    public virtual void UpdateUI()
    {
        this.UpdateUIInfoBox();
        SetDisPlayInfoTop();

        if (selectedUnit != null)
        {
            PropertiesUnitsBase unit = selectedUnit.GetUnit();
            UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox
                .SetName_LevelUnits(unit.unitType.ToString(), "Level: " + unit.currentLevel.ToString());
            UpgradeBoxCtrl.Instance.BottomCtrl.EvolutionInfoBox.SetSpriteStar(selectedUnit.unitsType);
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
    }




}
