using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    // handle skill there
    public Button btnSkillRocket;
    [SerializeField] private float totalHp, timeShowPopupWinLose=0.5f;
    [SerializeField] private Vector2 vectorHp;
    [SerializeField] private Image imgCountDown;
    public GameObject boxBorderPlayerUIColor, boxPlayerUIColor, playerUIColorPrefab;

    public List<GameObject> playerUIColorList = new List<GameObject>();
    public bool isEnemyLive, initLevelDone, runOneTimeBool, skillActiveBool;

    private Coroutine c1;

    private void Awake()
    {
        RectTransform rectTransform = null;
        boxBorderPlayerUIColor.transform.TryGetComponent(out  rectTransform);
        //vectorHp = rectTransform.sizeDelta;
        totalHp = vectorHp.x;
        Debug.LogError("totalHp = "+totalHp);
        //this.enabled = false;
    }
    private void OnEnable()
    {
        btnSkillRocket.gameObject.SetActive(true);
    }
    private void Start()
    {
        btnSkillRocket.onClick.AddListener(delegate { CallActiveSkillRocket(); });
        imgCountDown.gameObject.SetActive(false);
    }

    

    private void Update()
    {
        if (!GamePlayController.Instance.isPlay)
        {
            return;
        }
        //if (!initLevelDone)
        //{
        //    return;
        //}
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
            }
        }
    }

    private void ShowLosePopupUI()
    {
        // check độ khó ở đây
        Debug.LogError("ShowLosePopupUI");
    }

    private void ShowWinPopupUI()
    {
        GameManager.Instance.EndGame();
        WinBox_QA.Setup().Show();

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
        // tru vang
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

    }
    private void ResetSkillRocket()
    {
        skillActiveBool = false;
        CheckUISkillRocket();
    }
}
