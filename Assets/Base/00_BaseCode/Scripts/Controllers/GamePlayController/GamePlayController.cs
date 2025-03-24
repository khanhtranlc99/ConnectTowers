using Crystal;
using DG.Tweening;
using Org.BouncyCastle.Crypto.Tls;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        //  GameController.Instance.currentScene = SceneType.GamePlay;

     
        Init();
        CheckHp();
    }
    private void Update()
    {
        CheckHp();
        if(total == 0) Time.timeScale = 0;
    }

    public void Init()
    {

   
        playerContain.unitCtrl.unitGrid = new Stack<CharacterBase>[playerDatas.Count, 2];
        playerContain.Init();
     
     
      
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
