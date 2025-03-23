using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Q_DailySlot : LoadAutoComponents
{
    [SerializeField] Button btnClaim;
    [SerializeField] TextMeshProUGUI textRewardAmount;
    [SerializeField] Image iconGem;
    [SerializeField] Image icon;
    [SerializeField] Image effect;
    [SerializeField] int rewardAmount;

    [Space(10)]
    [SerializeField] Sprite close;
    [SerializeField] Sprite open;


    public bool isClaim = false;

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
        this.isClaim = true;
        this.icon.sprite = open;
        this.HiddenRewardSlot();
    }

    public void HiddenRewardSlot()
    {
        this.btnClaim.interactable = false;
        this.DisableEffect();
        this.textRewardAmount.gameObject.SetActive(false);
        this.iconGem.gameObject.SetActive(false);
    }

    //reset qua ngay moi
    public void ResetValueDaily()
    {
        this.btnClaim.interactable = true;
        this.icon.sprite = close;
        this.isClaim = false;
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
