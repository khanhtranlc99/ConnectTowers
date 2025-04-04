using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class UpgradeBoxCtrl : Singleton<UpgradeBoxCtrl>
{

    [SerializeField] protected U_CenterCtrl centerCtrl;
    public U_CenterCtrl CenterCtrl => centerCtrl;

    [SerializeField] protected U_BottomCtrl bottomCtrl;
    public U_BottomCtrl BottomCtrl => bottomCtrl;

    [SerializeField] protected U_TopCtrl topCtrl;
    public U_TopCtrl TopCtrl => topCtrl;

    [SerializeField] protected CoinGemInfo coinGemInfo;
    public CoinGemInfo CoinGemInfo => coinGemInfo;
    [Space(10)]
    [SerializeField] BaseCardCtrl currentActiveCard;
    public BaseCardCtrl CurrentCard => currentActiveCard;
    //set current card
    [Space(10)]
    [SerializeField] Sprite spriteStarOn;
    public Sprite SpriteStarOn => spriteStarOn;
    [SerializeField] Sprite spriteStarOff;
    public Sprite SpriteStarOff => spriteStarOff;

    [SerializeField] Model_Tab_Show model_Tab_Show;
    public Model_Tab_Show Model_Tab_Show => model_Tab_Show;

    public List<Transform> poolHolderModel = new();


    private void OnEnable()
    {
        GameController.Instance.dataContain.dataUser.LoadCardInventoryData();
    }

    private void Start()
    {
        this.currentActiveCard.EquippedUnitSlot.ShowModel();
    }

    public void SetCurrentActiveCard(BaseCardCtrl baseCardCtrl)
    {
        this.currentActiveCard = baseCardCtrl;
    }
    public void EquipCurrentUnit()
    {
        if (currentActiveCard != null) this.currentActiveCard.EquipSelectedUnit();
    }

    public void AddPool(Transform modelTrans)
    {
        if (this.poolHolderModel.Contains(modelTrans)) return;
            this.poolHolderModel.Add(modelTrans);
    }

    //lay unit de upgrade
    public UnitSlotBase GetEquippedUnit()
    {
        return this.currentActiveCard?.EquippedUnitSlot;
    }

}
