using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    // handle skill there

    [SerializeField] private float totalHp, timeShowPopupWinLose=0.5f;
    [SerializeField] private Vector2 vectorHp;
    public GameObject boxBorderPlayerUIColor, boxPlayerUIColor, playerUIColorPrefab;

    public List<GameObject> playerUIColorList = new List<GameObject>();
    public bool isEnemyLive, initLevelDone, runOneTimeBool;

    private Coroutine c1;

    private void Awake()
    {
        RectTransform rectTransform = null;
        boxBorderPlayerUIColor.transform.TryGetComponent(out  rectTransform);
        //vectorHp = rectTransform.sizeDelta;
        totalHp = vectorHp.x;
        Debug.LogError("totalHp = "+totalHp);
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
}
