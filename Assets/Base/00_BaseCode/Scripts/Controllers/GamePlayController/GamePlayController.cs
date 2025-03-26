using Crystal;
using DG.Tweening;
using Org.BouncyCastle.Crypto.Tls;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;


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
    public PlayerContain playerContain;
    public GameScene gameScene;
    public List<PlayerData> playerDatas = new List<PlayerData> ();

    public int total = 0;
    public GameObject prefabGold;

    [HideInEditorMode] public bool isPlay, isStillGrayTower;
 
 
    
    protected override void OnAwake()
    {
        GameController.Instance.currentScene = SceneType.GamePlay;
    }
    public void StartGame()
    {
        playerContain.unitCtrl.unitGrid = new Stack<CharacterBase>[playerDatas.Count, 2];
        playerContain.Init();
        CheckHp();
        isPlay = true;
        enabled = true;
        this.PostEvent(EventID.START_GAME);
    }
    private void Update()
    {
        CheckHp();
        playerContain.inputCtrl.lineContain.DrawPath();
    }

    public void ClearMap()
    {
        playerContain.inputCtrl.lineContain.line.positionCount = 1;
        isPlay = false;
        CheckHp();
        for(int i=playerContain.unitCtrl.allyList.Count-1; i>=0; i--)
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
                    Stack<CharacterBase> unitStack = new Stack<CharacterBase>();
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

    public void EnGame()
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
        //playerDatas.Add(GameMan)
    }
   
}
