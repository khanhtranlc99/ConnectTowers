using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SummonCtrlx1 : MonoBehaviour
{
    public float randomEffectDuration = 1f;
    public Button btnSummon;
    [SerializeField] Button btnClaim;
    [SerializeField] Transform trans;
    [SerializeField] Image imgUnit;
    [SerializeField] TextMeshProUGUI txtNameUnit;
    [Space(10)]
    [SerializeField] Sprite defaultSprite;

    [SerializeField] Image effect;
    [SerializeField] List<CardSlot_x1> lsCardRandomSlots;

    private void Start()
    {
        btnClaim.onClick.AddListener(ClaimComplete);
    }

    public IEnumerator SummonRoutine()
    {
        List<int> randomNumbers = new List<int>();
        for (int i = 0; i < 11; i++)
        {
            randomNumbers.Add(GetRandomCardIndex());
        }

        WaitForSeconds wait = new WaitForSeconds(randomEffectDuration / randomNumbers.Count);

        int previousIndex = -1;
        int finalCardIndex = randomNumbers[randomNumbers.Count - 1];

        for (int i = 0; i < randomNumbers.Count; i++)
        {
            int currentIndex = randomNumbers[i];

            if(previousIndex != -1)
                lsCardRandomSlots[previousIndex].imgChoose.gameObject.SetActive(false);

            lsCardRandomSlots[currentIndex].imgChoose.gameObject.SetActive(true);
            previousIndex = currentIndex;

            yield return wait;
        }
        lsCardRandomSlots[finalCardIndex].imgChoose.gameObject.SetActive(true);
        //tra ve ket qua
        lsCardRandomSlots[finalCardIndex].ResultUnit();
        this.btnClaim.gameObject.SetActive(true);
        /// hien thi hinh anh
        this.SetDisplayResultUnit(lsCardRandomSlots[finalCardIndex].SpriteResult, 
                                    lsCardRandomSlots[finalCardIndex].NameUnit,
                                    lsCardRandomSlots[finalCardIndex].ColorNameUnit);

        Debug.Log(lsCardRandomSlots[finalCardIndex]);
    }

    // ti le random Card
    int GetRandomCardIndex()
    {
        int rand = Random.Range(0, 100); // Random từ 0 -> 99
        if (rand < 35) return 0;   // 35% Common
        if (rand < 60) return 1;   // 25% Uncommon (35 + 25)
        if (rand < 80) return 2;   // 20% Rare (35 + 25 + 20)
        if (rand < 95) return 3;   // 15% Epic (35 + 25 + 20 + 15)
        return 4;                  // 5% Legend (35 + 25 + 20 + 15 + 5)
    }

    void SetDisplayResultUnit(Sprite sprite, string textParam, Color colorText)
    {
        this.txtNameUnit.text = textParam;
        this.txtNameUnit.color = colorText;
        this.txtNameUnit.gameObject.SetActive(true);


        this.effect.gameObject.SetActive(true);
        this.effect.transform.localScale = Vector3.zero;
        this.effect.transform.DOScale(2f,0.3f);


        this.imgUnit.gameObject.SetActive(true);
        this.imgUnit.sprite = sprite;
        this.imgUnit.transform.localScale = Vector3.zero;
        this.imgUnit.transform.DOScale(1.5f, 0.3f);
        this.imgUnit.SetNativeSize();

    }

    void ClaimComplete()
    {
        this.imgUnit.sprite = defaultSprite;
        this.imgUnit.transform.localScale = new Vector3(5, 5, 5);
        this.imgUnit.SetNativeSize();

        this.btnClaim.gameObject.SetActive(false);
        this.txtNameUnit.gameObject.SetActive(false);
        this.effect.gameObject.SetActive(false);
        foreach (var child in this.lsCardRandomSlots) child.imgChoose.gameObject.SetActive(false);

        this.trans.gameObject.SetActive(false);
    }
}
