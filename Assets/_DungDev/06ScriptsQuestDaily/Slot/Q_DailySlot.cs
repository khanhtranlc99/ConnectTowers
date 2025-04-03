using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventDispatcher;

public class Q_DailySlot : LoadAutoComponents
{
    public int id;
    [SerializeField] Button btnClaim;
    [SerializeField] TextMeshProUGUI textRewardAmount;
    [SerializeField] Image iconGem;
    [SerializeField] Image icon;
    [SerializeField] Image effect;
    [SerializeField] int rewardAmount;

    [Space(10)]
    [SerializeField] Sprite close;
    [SerializeField] Sprite open;
    [SerializeField] Q_PanelShowResult panelShowResult;

    private void OnEnable()
    {
        this.SetInfo();
    }
    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameController.Instance.dataContain.dataUser.AddGems(rewardAmount);
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        GameController.Instance.dataContain.dataUser.DataDailyQuest.lsDailyTracker[id] = true;
        GameController.Instance.dataContain.dataUser.DataDailyQuest.isDailyTracker = true;
        QuestDailySave_Json.SaveDataQuestTopTracker(GameController.Instance.dataContain.dataUser.DataDailyQuest);

        this.panelShowResult.gameObject.SetActive(true);
        this.panelShowResult.transform.localScale = Vector3.zero;
        this.panelShowResult.transform.DOScale(1f, 0.4f);

        this.panelShowResult.SetAmountReward(rewardAmount);
        this.HiddenRewardSlot();
    }

    public void HiddenRewardSlot()
    {
        this.btnClaim.interactable = false;
        this.DisableEffect();
        this.icon.sprite = open;
        this.textRewardAmount.gameObject.SetActive(false);
        this.iconGem.gameObject.SetActive(false);


    }

    //reset qua ngay moi
    public void ResetValueDaily()
    {
        this.btnClaim.interactable = true;
        this.icon.sprite = close;
        this.textRewardAmount.gameObject.SetActive(true);
        this.iconGem.gameObject.SetActive(true);
    }

    public void EnableEffect()
    {
        this.effect.gameObject.SetActive(true);
    }
    public void DisableEffect()
    {
        this.effect.gameObject.SetActive(false);
    }

    public void SetActiveBtn(bool check)
    {
        this.btnClaim.interactable = check;
    }

    public void SetAmountReward(int amount)
    {
        this.rewardAmount = amount;
    }

    void SetInfo()
    {
        this.textRewardAmount.text = rewardAmount.ToString();
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnClaim = GetComponent<Button>();
        this.textRewardAmount = GetComponentInChildren<TextMeshProUGUI>();
        this.icon = transform.Find("icon").GetComponent<Image>();
        this.iconGem = transform.Find("iconGem").GetComponent<Image>();
        this.effect = transform.Find("effect").GetComponent<Image>();
    }
}
