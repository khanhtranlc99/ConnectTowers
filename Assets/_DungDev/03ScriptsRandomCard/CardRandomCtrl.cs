using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardRandomCtrl : MonoBehaviour
{
    public float randomEffectDuration = 1f;
    public Button btnSummon;
    [SerializeField] Button btnClaim;
    [SerializeField] Transform trans;
    [SerializeField] Image imgUnit;
    [Space(10)]
    [SerializeField] Sprite defaultSprite;

    [SerializeField] Image effect;
    [SerializeField] List<BaseCardRandomSlot> lsCardRandomSlots;

    private void Start()
    {
        btnClaim.onClick.AddListener(ClaimComplete);
    }

    public IEnumerator SummonRoutine()
    {
        List<int> randomNumbers = new List<int>();
        for (int i = 0; i < 11; i++)
        {
            randomNumbers.Add(Random.Range(0, lsCardRandomSlots.Count));
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
        this.SetDisplayResultUnit(lsCardRandomSlots[finalCardIndex].SpriteResult);

        Debug.Log(lsCardRandomSlots[finalCardIndex]);
    }

    public void SetDisplayResultUnit(Sprite sprite)
    {
        this.imgUnit.gameObject.SetActive(true);
        this.effect.gameObject.SetActive(true);
        this.imgUnit.sprite = sprite;
        this.imgUnit.SetNativeSize();
    }

    void ClaimComplete()
    {
        this.imgUnit.sprite = defaultSprite;
        this.effect.gameObject.SetActive(false);
        this.trans.gameObject.SetActive(false);
    }
}
