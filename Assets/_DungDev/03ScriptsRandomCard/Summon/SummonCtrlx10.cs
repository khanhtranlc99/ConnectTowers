using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SummonCtrlx10 : MonoBehaviour
{
    [SerializeField] Transform trans;
    [SerializeField] Button btnClaim;
    [SerializeField] Sprite spriteDefault;
    [SerializeField] List<CardSlot_x10> lsCardSlots = new();

    private void Start()
    {
        foreach (var child in this.lsCardSlots) child.SetDisPlayCardDefault(spriteDefault);
        this.btnClaim.onClick.AddListener(ClaimComplete);
        this.btnClaim.gameObject.SetActive(false);
    }

    public IEnumerator SummonRoutine()
    {
        //hien thi khung card user nhan duoc
        this.btnClaim.gameObject.SetActive(false);
        foreach (var child in this.lsCardSlots)
        {
            child.GenerateRandomUnit();
        }

        foreach(var child in this.lsCardSlots)
        {
            child.ResultBG();
        }

        // set de hien thi card
        for (int i = 0; i< lsCardSlots.Count; i++)
        {
            Transform cardTrans = lsCardSlots[i].transform;
            yield return cardTrans.DORotate(new Vector3(0,90,0), 0.2f).WaitForCompletion();
            lsCardSlots[i].ResultUnit();
            yield return cardTrans.DORotate(Vector3.zero, 0.2f).WaitForCompletion();
        }
        this.btnClaim.gameObject.SetActive(true);

    }
    void ClaimComplete()
    {
        this.trans.gameObject.SetActive(false);
        foreach (var child in this.lsCardSlots) child.SetDisPlayCardDefault(spriteDefault);
        //save json
        CardUnitsSaveSystem_Json.SaveDataCardInventory(GameController.Instance.dataContain.dataUser);
    }
}
