using Crystal;
using DG.Tweening;
using Org.BouncyCastle.Crypto.Tls;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;
using System;


public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}

public class GamePlayController : Singleton<GamePlayController>
{
    public StateGame stateGame;
    public GameManager gameManager;
    public UIController uIController;
    public PlayerContain playerContain;
    public GameScene gameScene;
    public List<PlayerData> playerDatas = new List<PlayerData> ();

    public int total = 0;
    public GameObject prefabGold;

    public bool isPlay, isStillGrayTower;

 
 
    
    protected override void OnAwake()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.currentScene = SceneType.GamePlay;
        }
        gameManager.Init();
        gameManager.InitGame();
        uIController.InitUI();
    }
    public void StartGame()
    {
        playerContain.unitCtrl.unitGrid = new Stack<CharacterBase>[playerDatas.Count, 2];
        for (int i = 0; i < playerDatas.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                playerContain.unitCtrl.unitGrid[i, j] = new Stack<CharacterBase>(); // Khởi tạo từng ô
            }
        }
        playerContain.Init();
        CheckHp();
        isPlay = true;
        enabled = true;
        stateGame = StateGame.Playing;
        this.PostEvent(EventID.START_GAME);
        if(UseProfile.CurrentLevel == 1)
        {
            TutorialManager.Setup().StartTutorial(1);
        }
        else if(UseProfile.CurrentLevel == 2)
        {
            TutorialManager.Setup().StartTutorial(2);
        }
        else if(UseProfile.CurrentLevel == 3)
        {
            TutorialManager.Setup().StartTutorial(3);
        }
        else if (UseProfile.CurrentLevel == 5)
        {
            TutorialManager.Setup().StartTutorial(5);
        }
        else if( UseProfile.CurrentLevel == 7)
        {
            TutorialManager.Setup().StartTutorial(7);
        }
        else if(UseProfile.CurrentLevel == 11)
        {
            TutorialManager.Setup().StartTutorial(11);
        }
        else if(UseProfile.CurrentLevel == 14)
        {
            TutorialManager.Setup().StartTutorial(14);
        }
    }

    private void Update()
    {
        if (isPlay)
        {
            CheckHp();
            playerContain.inputCtrl.lineContain.DrawPath();
        }
    }

    public void ClearMap()
    {
        //playerContain.inputCtrl.lineContain.line.positionCount = 1;
        isPlay = false;
        CheckHp();
        for (int i=playerContain.unitCtrl.allyList.Count-1; i>=0; i--)
        {
            Destroy(playerContain.unitCtrl.allyList[i].gameObject);
        }
        playerContain.unitCtrl.allyList.Clear();
        if(playerContain.unitCtrl.unitGrid != null)
        {
            for(int row = 0; row<playerDatas.Count; row++)
            {
                for(int col = 0; col < 2; col++)
                {
                    Stack<CharacterBase> unitStack = playerContain.unitCtrl.unitGrid[row,col];
                    if(unitStack != null)
                    {
                        for(int i = 0, x=unitStack.Count; i < x; i++)
                        {
                            CharacterBase unitRemove = unitStack.Pop();
                            if(unitRemove != null)
                            {
                                Destroy(unitRemove.gameObject);
                            }
                        }
                    }
                }
            }
        }
        foreach(var item in playerContain.inputCtrl.lineContain.linesList)
        {
            Destroy(item.gameObject);
        }
        playerContain.inputCtrl.lineContain.linesList.Clear();
        enabled = false;
    }

    public void EndGame()
    {
        foreach(var item in playerContain.buildingCtrl.towerList)
        {
            item.enabled = false;
        }
        ClearMap();
    }
    public void CheckHp()
    {
        this.total = 0;
        int hp = 0;
        bool isLive = false;
        isStillGrayTower = false;
        foreach(var item in playerContain.buildingCtrl.towerList)
        {
            if(item.teamId == 0)
            {
                hp += item.Hp;
                isLive = true;
            }
            else if(item.teamId < 0 && !(item as GoldPack))
            {
                isStillGrayTower = true;
            }
        }
        playerDatas[0].Hp = hp;
        playerDatas[0].isLive = isLive;
        foreach(var item in playerDatas)
        {
            total += item.Hp;
        }
    }
    public void CreateGame()
    {
        playerContain.buildingCtrl.towerList.Clear();
        playerContain.buildingCtrl.armyTowerList.Clear();
        playerDatas.Clear();
        playerDatas.Add(gameManager.PlayerData);
        ClearMap();
    }

    
    #region skill mamager
    [Header("Skill Manager")]
    public float timeAnimeRocket = 1.5f;
    public float timeReActiveSkill = 20f;
    [SerializeField] private GameObject rocketSkillPrefabs; // vfx nx
    [SerializeField] private List<GameObject> rocketSkillList = new List<GameObject>();

    internal void ActiveSkillRocket()
    {
        int highestHp = 0;
        BuildingContain targetTow = null;
        foreach(var item in playerContain.buildingCtrl.towerList)
        {
            if(item.teamId > 0)
            {
                highestHp = item.Hp;
                targetTow = item;
                continue;
            }
        }
        foreach(var item in playerContain.buildingCtrl.towerList)
        {
            if(item.teamId > 0)
            {
                if(item.Hp> highestHp)
                {
                    highestHp = item.Hp;
                    targetTow= item;
                }
            }
        }
        if(targetTow == null)
        {
            return;
        }

        Vector3 tmp = targetTow.transform.position;
        tmp.y = targetTow.transform.position.y + 50;
        GameObject g = Instantiate(rocketSkillPrefabs);
        g.transform.position = tmp;
        g.SetActive(true);
        // handle vfx
        g.transform.DOMoveY(targetTow.transform.position.y, timeAnimeRocket).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g.activeSelf)
            {
                return ;
            }
            targetTow.Hp = 0;
            Destroy(g);
        });
    }
    #endregion

}
