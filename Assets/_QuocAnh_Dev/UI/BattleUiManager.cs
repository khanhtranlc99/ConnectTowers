using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;
using BestHTTP.Extensions;

public class BattleUiManager : MonoBehaviour
{
    // handle skill there
    public Button btnSkillRocket, btnSetting, btnSkillRocketAds;
    [SerializeField] private float totalHp, timeShowPopupWinLose = 0.5f;
    [SerializeField] private int gemSkillRocket = 20;
    [SerializeField] private Vector2 vectorHp;
    [SerializeField] private Image imgCountDown;
    [SerializeField] private TMP_Text curLevel, curTime;
    public GameObject boxBorderPlayerUIColor, playerUIColorParent, playerUIColorPrefab;
    public GameObject boxGold, boxAds, boxGem;
    public BoosterUICtl boosterUICtl;
    public ResourcesCtrl resourecesCtrl;

    public List<GameObject> playerUIColorList = new List<GameObject>();
    public bool isEnemyLive, initLevelDone, runOneTimeBool, skillActiveBool;

    private Coroutine c1;
    //[SerializeField] private UIController uiController;
    [HideInInspector] public float timeElapsed = 0f;


    public void Init()
    {
        RectTransform rectTransform = null;
        boxBorderPlayerUIColor.transform.TryGetComponent(out rectTransform);
        vectorHp = rectTransform.sizeDelta;
        totalHp = vectorHp.x;
        resourecesCtrl.Init();
        UpdateUIBattle();
        InitBtn();
    }
    private void UpdateUIBattle()
    {
        StartCoroutine( LoadUIStartGame());
        btnSetting.interactable = true;
        if (UseProfile.CurrentLevel < GamePlayController.Instance.uIController.levelStartRocket)
        {
            btnSkillRocket.gameObject.SetActive(false);
            btnSkillRocketAds.gameObject.SetActive(false);
        }
        else
        {
            btnSkillRocket.gameObject.SetActive(true);
            btnSkillRocketAds.gameObject.SetActive(true);
        }
        curLevel.text = UseProfile.CurrentLevel.ToString();

        ResetSkillRocket();
        //CheckUISkillRocket();
        
    }



    private void InitBtn()
    {
        btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OutCampaign(); });
        btnSkillRocket.onClick.AddListener(() =>{ CallActiveSkillRocket(); });
        btnSkillRocketAds.onClick.AddListener(() => { WatchAdsToActiveRocket(); });
        btnSkillRocket.interactable = true;
        imgCountDown.gameObject.SetActive(false);
        boosterUICtl.Init();
    }

    private void OutCampaign()
    {
        GamePlayController.Instance.isPlay = false;
        SettingGameBox.Setup().Show();
        SettingGameBox.Setup().SetupForScene("GamePlay");
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
        timeElapsed += Time.deltaTime;
        UpdateTime();
        isEnemyLive = false;
        for (int i = 0; i < playerUIColorList.Count; i++)
        {
            Vector2 tmp = vectorHp;
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
                        isEnemyLive = true;
                    }
                    break;
            }
        }
        if (!isEnemyLive && !GamePlayController.Instance.isStillGrayTower)
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
        GamePlayController.Instance.gameManager.EndGame();
        LoseBox.Setup().Show();
        GameController.Instance.musicManager.PlayLoseSound();
    }

    private void ShowWinPopupUI()
    {
        GamePlayController.Instance.gameManager.EndGame();
        WinBox_QA.Setup().Show();
        GameController.Instance.musicManager.PlayWinSound();
    }
    private void CallActiveSkillRocket()
    {
        GameController.Instance.musicManager.PlayClickSound();
        ActiveSkillRocket(false);
    }
    private void WatchAdsToActiveRocket()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowVideoReward(
            actionReward: () =>
            {
                
                ActiveSkillRocket(true);
            },
            actionNotLoadedVideo: () =>
            {
                GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                (btnSkillRocket.transform,
                btnSkillRocket.transform.position,
                "No video at the moment!",
                Color.white,
                isSpawnItemPlayer: true);
            },
            actionClose: null,
            ActionWatchVideo.Rocket_Booster,
            UseProfile.CurrentLevel.ToString()
        );
    }

    private void ActiveSkillRocket(bool watchAdsBool = true)
    {
        skillActiveBool = true;
        GamePlayController.Instance.ActiveSkillRocket();
        if (!watchAdsBool)
        {
            GameController.Instance.dataContain.dataUser.DeductGem(gemSkillRocket);
            this.PostEvent(EventID.UPDATE_COIN_GEM);
        }
        btnSkillRocket.interactable = false;
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
            if(resourecesCtrl.textGem.text.ToInt32() < gemSkillRocket)
            {
                btnSkillRocket.gameObject.SetActive(false);
                btnSkillRocketAds.gameObject.gameObject.SetActive(true);
            }
            else
            {
                btnSkillRocket.gameObject.SetActive(true);
                btnSkillRocketAds.gameObject.SetActive(false);
            }
        
        }
        imgCountDown.gameObject.SetActive(false);
        btnSkillRocket.interactable = true;

    }
    private void ResetSkillRocket()
    {
        skillActiveBool = false;
        btnSkillRocket.interactable = true;
        CheckUISkillRocket();
    }
    private IEnumerator LoadUIStartGame()
    {
        initLevelDone = false;
        yield return new WaitUntil(() => GamePlayController.Instance.isPlay);
        foreach (var item in playerUIColorList)
        {
            Destroy(item);
        }
        playerUIColorList = new List<GameObject>();
        runOneTimeBool = false;
        for (int i = 0; i < GamePlayController.Instance.playerDatas.Count; i++)
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

    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        curTime.text = $"{minutes:00}:{seconds:00}";
    }
}
