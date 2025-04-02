using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    // handle skill there
    public Button btnSkillRocket, btnSetting;
    [SerializeField] private float totalHp, timeShowPopupWinLose=0.5f;
    [SerializeField] private int gemSkillRocket = 20;
    [SerializeField] private Vector2 vectorHp;
    [SerializeField] private Image imgCountDown;
    [SerializeField] private TMP_Text goldText, gemText, curLevel, curTime;
    public GameObject boxBorderPlayerUIColor, playerUIColorParent, playerUIColorPrefab;
    public GameObject boxGold, boxAds, boxGem;
    public Button skill1, skill2, skill3, skill4, skill5, skill6;

    public List<GameObject> playerUIColorList = new List<GameObject>();
    public bool isEnemyLive, initLevelDone, runOneTimeBool, skillActiveBool;

    private Coroutine c1;
    [HideInInspector] public float timeElapsed = 0f;


    private void Awake()
    {
        RectTransform rectTransform = null;
        boxBorderPlayerUIColor.transform.TryGetComponent(out  rectTransform);
        vectorHp = rectTransform.sizeDelta;
        totalHp = vectorHp.x;

    }
    private void OnEnable()
    {
        StopAndStartMyCoroute(ref c1, DelayLoadUIStartGame());
        btnSetting.interactable = true;
        if (UseProfile.CurrentLevel < UIController.Instance.levelStartRocket)
        {
            btnSkillRocket.gameObject.SetActive(false);
        }
        else
        {
            btnSkillRocket.gameObject.SetActive(true);
        }
        UpdateUIGold();
        UpdateUIGem();
        ResetSkillRocket();
        CheckUISkillRocket();
    }

    

    private void Start()
    {
        btnSetting.onClick.AddListener(() =>
        {
            OutCampaign();
            //MusicData.Pla play sound click;
        });
        btnSkillRocket.onClick.AddListener(() =>
        {
            CallActiveSkillRocket();
            //MusicData.Pla play sound click;
        });
        btnSkillRocket.interactable = true;
        imgCountDown.gameObject.SetActive(false);
    }

    private void OutCampaign()
    {
        GamePlayController.Instance.isPlay = false;
        SettingInBattle.Setup().Show();
        // show ads
    }

    private void Update()
    {
        if (!GamePlayController.Instance.isPlay)
        {
            return;
        }
        if (!initLevelDone)
        {
            return;
        }
        timeElapsed+=Time.deltaTime;
        UpdateTime();
        isEnemyLive = false;
        for(int i = 0; i < playerUIColorList.Count; i++)
        {
            Vector2 tmp= vectorHp;
            tmp.x = (float)GamePlayController.Instance.playerDatas[i].Hp / GamePlayController.Instance.total * totalHp;
            playerUIColorList[i].GetComponent<RectTransform>().sizeDelta = tmp;
            switch (i)
            {
                case 0:
                    if (!GamePlayController.Instance.playerDatas[0].isLive)
                    {
                        if (!runOneTimeBool)
                        {
                            runOneTimeBool = true;
                            Invoke(nameof(ShowLosePopupUI), timeShowPopupWinLose);
                        }
                        
                    }
                    break;
                default:
                    if (GamePlayController.Instance.playerDatas[i].isLive)
                    {
                        isEnemyLive= true;
                    }
                    break;
            }
        }
        if(!isEnemyLive && !GamePlayController.Instance.isStillGrayTower)
        {
            if (!runOneTimeBool)
            {
                runOneTimeBool = true;
                Invoke(nameof(ShowWinPopupUI), timeShowPopupWinLose);
                btnSetting.interactable = false;
            }
        }
    }

    private void ShowLosePopupUI()
    {
        GameManager.Instance.EndGame();
        LoseBox.Setup().Show();
        
    }

    private void ShowWinPopupUI()
    {
        GameManager.Instance.EndGame();
        WinBox_QA.Setup().Show();
        // destroy all sound
        //GameController.Instance.musicManager.PlayMusic();
    }
    private void CallActiveSkillRocket()
    {
        //check xem con gold kh
        ActiveSkillRocket(false);
    }

    private void ActiveSkillRocket(bool watchAdsBool = true)
    {
        skillActiveBool =true;
        GamePlayController.Instance.ActiveSkillRocket();
        if (!watchAdsBool)
        {
            GameManager.Instance.PlayerData.gem-=gemSkillRocket;
            UpdateUIGem();
        }
        imgCountDown.gameObject.SetActive(true);
        imgCountDown.fillAmount = 1;
        imgCountDown.DOFillAmount(0, GamePlayController.Instance.timeReActiveSkill).SetEase(Ease.Linear).OnComplete(() =>
        {
            ResetSkillRocket();
        });
    }
    private void CheckUISkillRocket()
    {
        if (!skillActiveBool)
        {
            // kiem tra con vang kh neu kh con thi kh dung dc
        }
        imgCountDown.gameObject.SetActive(false);
        btnSkillRocket.interactable = true;

    }
    private void ResetSkillRocket()
    {
        skillActiveBool = false;
        CheckUISkillRocket();
    }
    private IEnumerator DelayLoadUIStartGame()
    {
        initLevelDone = false;
        yield return new WaitUntil(() => GamePlayController.Instance.isPlay);
        foreach (var item in playerUIColorList)
        {
            Destroy(item);
        }
        playerUIColorList = new List<GameObject>();
        runOneTimeBool = false;
        for(int i = 0; i < GamePlayController.Instance.playerDatas.Count; i++)
        {
            Vector2 tmp = vectorHp;
            tmp.x = (float)GamePlayController.Instance.playerDatas[i].Hp / GamePlayController.Instance.total * totalHp;
            GameObject g = Instantiate(playerUIColorPrefab, playerUIColorParent.transform);
            playerUIColorList.Add(g);
            playerUIColorList[i].GetComponent<RectTransform>().sizeDelta = tmp;
            playerUIColorList[i].GetComponent<Image>().color = ConfigData.Instance.colors[i];
        }
        initLevelDone = true;
    }
    public void StopAndStartMyCoroute(ref Coroutine c, IEnumerator ie)
    {
        if(c != null) StopCoroutine(c);
        if(ie != null) StartCoroutine(ie);
    }
    private void UpdateUIGold()
    {
        goldText.text = Helper.ConvertNumberToString(GameManager.Instance.PlayerData.gold);
    }
    private void UpdateUIGem()
    {
        gemText.text = Helper.ConvertNumberToString(GameManager.Instance.PlayerData.gem);
    }
    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        curTime.text = $"{minutes:00}:{seconds:00}";
    }
}
