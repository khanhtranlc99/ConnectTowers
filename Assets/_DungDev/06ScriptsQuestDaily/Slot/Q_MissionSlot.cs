using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Q_MissionSlot : LoadAutoComponents
{
    [Header("ID Quest")]
    [SerializeField] int idQuest;
    
    [SerializeField] Button btnClaim;
    [SerializeField] Button btnGo;

    [SerializeField] TextMeshProUGUI textNameMisstion;
    [SerializeField] TextMeshProUGUI textCurrentProgress;
    [SerializeField] TextMeshProUGUI textRequiredProgress;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI textAmountReward;
    DataDailyQuest dataDailyQuest;

    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        this.dataDailyQuest = GameController.Instance.dataContain.dataUser.DataDailyQuest;
        this.SetInfoQuest();
    }

    void OnClick()
    {
        DailyQuest dailyQuest = GetQuest();
        dailyQuest.SetCurrentProgess(idQuest);
        dailyQuest.isClaimed = true;

        dataDailyQuest.SetCurentTotalReward(dailyQuest.amountReward);

        this.PostEvent(EventID.UPDATE_PROGESSBAR_QUEST);
        this.btnClaim.gameObject.SetActive(false);
        this.btnGo.gameObject.SetActive(false);
    }

    void SetInfoQuest()
    {
        DailyQuest dailyQuest = GetQuest();
        this.textNameMisstion.text = dailyQuest.questName;
        this.textCurrentProgress.text = dailyQuest.currentProgess.ToString();
        this.textRequiredProgress.text = "/" +dailyQuest.requiredProgess.ToString();
        this.textAmountReward.text = dailyQuest.amountReward.ToString();
        this.progressBar.fillAmount = (float)dailyQuest.currentProgess / (float)dailyQuest.requiredProgess;

        this.btnClaim.gameObject.SetActive(dailyQuest.IsCompleted() && !dailyQuest.isClaimed);
        this.btnGo.gameObject.SetActive(!dailyQuest.IsCompleted() && !dailyQuest.isClaimed);
    }

    DailyQuest GetQuest()
    {
        return dataDailyQuest.GetQuestByID(idQuest);
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnClaim = transform.Find("Right").Find("BtnClaim").GetComponent<Button>();
        this.btnClaim.gameObject.SetActive(false);
        this.btnGo = transform.Find("Right").Find("BtnGo").GetComponent<Button>();

        this.textAmountReward = transform.Find("Left").Find("amountReward").GetComponent<TextMeshProUGUI>();
        this.textNameMisstion = transform.Find("Center").Find("nameQuest").GetComponent<TextMeshProUGUI>();
        this.progressBar = transform.Find("Center").Find("process").Find("processValue").GetComponent<Image>();
        this.textCurrentProgress = transform.Find("Right").Find("txtCurrent").GetComponent<TextMeshProUGUI>();
        this.textRequiredProgress = transform.Find("Right").Find("txtPassMisstion").GetComponent<TextMeshProUGUI>();
    }
}
