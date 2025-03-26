using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData = new PlayerData();
    public List<PlayerUnit> playerUnits = new List<PlayerUnit>();

    public PlayerData PlayerData => playerData;

    private void Awake()
    {
        playerData = Saver.Read();
        if(playerData == null || playerData.unitSoldierId ==0 || playerData.unitTankId ==0 || playerData.unitMageId == 0)
        {
            playerData = new PlayerData();
            Saver.Write(playerData);
        }

        ValidataPlayerData();
        if(GameSave.PlayerLevel <= 0)
        {
            GameSave.PlayerLevel = 1;
        }
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
            playerData.unitSoldierId = 1;
            playerData.unitTankId = 2;
            playerData.unitMageId = 3;
            playerData.unitSoldierLv = playerData.GetUnitInfo(1).level;
            playerData.unitTankLv = playerData.GetUnitInfo(2).level;
            playerData.unitMageLv = playerData.GetUnitInfo(3).level;
        }
    }
    private object CreateNewGame()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        Saver.Write(playerData);
    }
}
