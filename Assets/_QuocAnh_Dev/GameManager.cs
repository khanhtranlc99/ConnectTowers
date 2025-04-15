using EventDispatcher;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = new PlayerData();
    //public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    private Transform Level;


    public PlayerData PlayerData => playerData;

    public void Init()
    {
        playerData = UseProfile.ReadUser();
        Debug.LogError("playerData was read" + playerData);

        if (playerData == null || playerData.unitSoldierId == 0 || playerData.unitTankId == 0 || playerData.unitMageId == 0)
        {
            Debug.LogWarning("playerData was null");
            playerData = new PlayerData();
            UseProfile.WriteUser(playerData);
        }

        ValidataPlayerData();

        if (UseProfile.CurrentLevel <= 0)
        {
            Debug.LogError("set CurrentLevel is 1");
            UseProfile.CurrentLevel = 1;
        }
    }

    public void InitGame()
    {
        Invoke(nameof(CreateNewGame), 0.2f);
    }

    public void ValidataPlayerData()
    {
        if (playerData.playerUnitsDatas == null || playerData.playerUnitsDatas.Count == 0)
        {
            playerData.playerUnitsDatas = new List<PlayerUnitData>();
            playerData.unitSoldierId = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.iD+1;
            playerData.unitTankId = GameController.Instance.dataContain.dataUser.CurrentCardBeast.iD + 1;
            playerData.unitMageId = GameController.Instance.dataContain.dataUser.CurrentCardMage.iD + 1;
            playerData.unitSoldierLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
            playerData.unitTankLv = GameController.Instance.dataContain.dataUser.CurrentCardBeast.currentLevel;
            playerData.unitMageLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
            //playerData.soldierUnit = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.unitType.ToString();
            //playerData.tankUnit = GameController.Instance.dataContain.dataUser.CurrentCardBeast.unitType.ToString();
            //playerData.mageUnit = GameController.Instance.dataContain.dataUser.CurrentCardMage.unitType.ToString();
        }
        else
        {
            playerData.unitSoldierId = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.iD + 1;
            playerData.unitTankId = GameController.Instance.dataContain.dataUser.CurrentCardBeast.iD + 1;
            playerData.unitMageId = GameController.Instance.dataContain.dataUser.CurrentCardMage.iD + 1;
            playerData.unitSoldierLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
            playerData.unitTankLv = GameController.Instance.dataContain.dataUser.CurrentCardBeast.currentLevel;
            playerData.unitMageLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
            //playerData.soldierUnit = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.unitType.ToString();
            //playerData.tankUnit = GameController.Instance.dataContain.dataUser.CurrentCardBeast.unitType.ToString();
            //playerData.mageUnit = GameController.Instance.dataContain.dataUser.CurrentCardMage.unitType.ToString();
        }
    }
    public void CreateNewGame()
    {
        CreateGame(UseProfile.CurrentLevel);
    }

    [Button]
    public void CreateGame(int lv = -1)
    {
        if (lv != -1)
        {
            UseProfile.CurrentLevel = lv;
        }
        string localPath = "Levels/Level_" + UseProfile.CurrentLevel;
        Debug.LogError(Resources.Load<GameObject>(localPath));
        if (Resources.Load<GameObject>(localPath) != null)
        {
            UseProfile.FakePlayerLevel = 0;
            GameObject levelPrefab = Resources.Load<GameObject>(localPath);
            if (Level != null)
            {
                Destroy(Level.gameObject);
            }
            GameObject _lv = Instantiate(levelPrefab);
            Level = _lv.transform;
            Level.position = Vector3.zero;
            _lv.name = "Level";
            Invoke(nameof(DelayCreateGame), 0.2f);
            return;
        }
        else
        {
            if (UseProfile.FakePlayerLevel == 0)
            {
                UseProfile.FakePlayerLevel++;
            }
            int total = ConfigData.Instance.lv.Count;
            int numCanRepeat = total - 10;
            int numRepeat = UseProfile.FakePlayerLevel % numCanRepeat;
            localPath = "Levels/Level_" + 10 + numRepeat;
            if (Resources.Load<GameObject>(localPath) != null)
            {
                GameObject levelPrefab = Resources.Load<GameObject>(localPath);
                if (Level != null)
                {
                    Destroy(Level.gameObject);
                }
                GameObject _lv = Instantiate(levelPrefab);
                Level = _lv.transform;
                Level.position = Vector3.zero;
                _lv.name = "Level";
                Invoke(nameof(DelayCreateGame), 0.2f);
                return;
            }
        }
    }

    private void DelayCreateGame()
    {
        GamePlayController.Instance.CreateGame();

        this.PostEvent(EventID.CREATE_GAME);

        GamePlayController.Instance.uIController.battleUiManager.runOneTimeBool = false;

        if (GamePlayController.Instance.enabled == false && GamePlayController.Instance.uIController.isStartGameClick)
        {
            GamePlayController.Instance.enabled = true;
            Invoke(nameof(TestGame), 0.2f);
        }
    }
    public void TestGame()
    {
        GamePlayController.Instance.StartGame();

    }
    public void StartGame()
    {
        playerData.unitSoldierId = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.iD + 1;
        playerData.unitTankId = GameController.Instance.dataContain.dataUser.CurrentCardBeast.iD + 1;
        playerData.unitMageId = GameController.Instance.dataContain.dataUser.CurrentCardMage.iD + 1;
        playerData.unitSoldierLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
        playerData.unitTankLv = GameController.Instance.dataContain.dataUser.CurrentCardBeast.currentLevel;
        playerData.unitMageLv = GameController.Instance.dataContain.dataUser.CurrentCardSoldier.currentLevel;
        GamePlayController.Instance.StartGame();
        // firebase
    }
    [Button]
    public void ResetGame()
    {
        GamePlayController.Instance.isPlay = false;
        this.PostEvent(EventID.CLEAR_MAP);
        GamePlayController.Instance.ClearMap();
        //firebase
        GamePlayController.Instance.StartGame();
    }
    [Button]
    public void EndGame()
    {
        GamePlayController.Instance.isPlay = false;
        GamePlayController.Instance.EndGame();
        this.PostEvent(EventID.END_GAME);
    }

    public void Save()
    {
        UseProfile.WriteUser(playerData);
    }

}
