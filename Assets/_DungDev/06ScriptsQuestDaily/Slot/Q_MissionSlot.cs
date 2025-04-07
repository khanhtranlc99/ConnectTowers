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
    public QuestType questType;
    [SerializeField] Button btnClaim;
    [SerializeField] Button btnGo;

    [SerializeField] TextMeshProUGUI textNameMisstion;
    [SerializeField] TextMeshProUGUI textCurrentProgress;
    [SerializeField] TextMeshProUGUI textRequiredProgress;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI textAmountReward;

    public void Init()
    {
        this.btnClaim.onClick.RemoveAllListeners();
        this.btnGo.onClick.RemoveAllListeners();

        this.btnClaim.onClick.AddListener(delegate { OnClick(); });
        this.btnGo.onClick.AddListener(delegate { OnClickBtnGo(); });
        this.SetInfoQuest();
    }
    void OnClickBtnGo()
    {
        switch (questType)
        {
            case QuestType.UpgradeUnit:
            case QuestType.EvolveUnit:
                UpgradeBox.Setup().Show();
                GameController.Instance.musicManager.PlayClickSound();
                break;

            case QuestType.RerollShop:
                ShopMallBox.Setup().Show();
                GameController.Instance.musicManager.PlayClickSound();
                break;
            case QuestType.SpinWheel:
                WheelSpinBox.Setup().Show();
                GameController.Instance.musicManager.PlayClickSound();
                break;
            case QuestType.SummonSingle:
            case QuestType.SummonMulti:
                SummonBox.Setup().Show();
                GameController.Instance.musicManager.PlayClickSound();

                break;
        }
        QuestBox.Setup().Close();

    }


    void OnClick()
    {
        DailyQuest dailyQuest = GetQuest();
        dailyQuest.isClaimed = true;
        GameController.Instance.dataContain.dataUser.DataDailyQuest.SetCurentTotalReward(dailyQuest.amountReward);
        Debug.LogError(dailyQuest.amountReward);

        this.PostEvent(EventID.UPDATE_PROGESSBAR_QUEST);
        this.btnClaim.gameObject.SetActive(false);
        this.btnGo.gameObject.SetActive(false);

        GameController.Instance.musicManager.PlayClickSound();

        //QuestDailySave_Json.SaveDataQuestDaily(GameController.Instance.dataContain.dataUser.DataDailyQuest);
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
        return GameController.Instance.dataContain.dataUser.DataDailyQuest.lsDailyQuests[idQuest];
    }


    public void SetIdMission(int param)
    {
        this.idQuest = param;
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
