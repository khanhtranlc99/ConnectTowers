using EventDispatcher;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerData playerData = new PlayerData();
    public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    private Transform Level;

    public bool isFindMatch = false;

    public PlayerData PlayerData => playerData;
    public GamePlayController gamePlayController;
    public BattleUiManager battleUiManager;

    private void OnEnable()
    {
        Instance = this;
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
        //if (isFindMatch) CreateGame();
    }

    private void Start()
    {
        Invoke(nameof(CreateNewGame), 0.2f);
    }

    public void ValidataPlayerData()
    {
        if (playerData.playerUnitsDatas == null || playerData.playerUnitsDatas.Count == 0)
        {
            playerData.playerUnitsDatas = new List<PlayerUnitData>();
            playerData.playerUnitsDatas.Add(new PlayerUnitData(1, 1)); // add common soldier
            playerData.playerUnitsDatas.Add(new PlayerUnitData(2, 1)); // add common tank
            playerData.playerUnitsDatas.Add(new PlayerUnitData(3, 1)); // add common mage
            playerData.unitSoldierId = 1;
            playerData.unitTankId = 2;
            playerData.unitMageId = 3;
            playerData.unitSoldierLv = playerData.GetUnitInfo(1).level;
            playerData.unitTankLv = playerData.GetUnitInfo(2).level;
            playerData.unitMageLv = playerData.GetUnitInfo(3).level;
        }
        else
        {
            playerData.unitSoldierLv = playerData.GetUnitInfo(playerData.unitSoldierId).level;
            playerData.unitTankLv = playerData.GetUnitInfo(playerData.unitTankId).level;
            playerData.unitMageLv = playerData.GetUnitInfo(playerData.unitMageId).level;
        }
    }
    public void CreateNewGame()
    {
        CreateGame(-1);
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
            //DelayCreateGame();
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
                //DelayCreateGame();
                Invoke(nameof(DelayCreateGame), 0.2f);
                return;
            }
        }
    }

    private void DelayCreateGame()
    {
        gamePlayController.CreateGame();

        this.PostEvent(EventID.CREATE_GAME);

        battleUiManager.runOneTimeBool = false;

        if (isFindMatch)
        {
            GamePlayController.Instance.enabled = true;
            Invoke(nameof(TestGame), 0.2f);
        }
        if (GamePlayController.Instance.enabled == false && UIController.Instance.isStartGameClick)
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
        PlayerData.unitSoldierId = playerData.GetUnitInfo(playerData.unitSoldierId).level;
        playerData.unitTankLv = playerData.GetUnitInfo(playerData.unitTankId).level;
        playerData.unitMageLv = playerData.GetUnitInfo(playerData.unitMageId).level;
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
