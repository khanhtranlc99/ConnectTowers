using Sirenix.OdinInspector;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EventDispatcher;

public class AiController : MonoBehaviour
{
    public PlayerData playerData;
#if UNITY_EDITOR
    [HideInInspector]
    public bool showErr;
    [HideInInspector]
    public Color color;
    public Color GetColor()
    {
        return color;
    }
    public void OnChangeTeam()
    {
        if(this.teamId == -1)
        {
            this.color = Color.gray;
        }
        else
        {
            color = ConfigData.Instance.colors[teamId];
        }
    }
    [ProgressBar(-1, 4, ColorGetter = "GetColor", Segmented = true, Height = 20)]
    [OnValueChanged("OnChangeTeam")]
    [InfoBox(
        "Team has already existed and must != 0 (PLAYER).",
        InfoMessageType.Error,
        VisibleIf = "showerr"
        )]
#endif
    public int teamId;
    [SerializeField] private float delay;
    private float intervalMin;
    private float intervalMax;
    private float actionCount;
    public int[] hardRate = new int[4];

    private int rank;

    // handle FSM
    private List<AI_Behaviour> listTrigger = new List<AI_Behaviour>(); //hành vi theo sự kiện
    private List<AI_Behaviour> listAuto = new List<AI_Behaviour>(); // hành vi AI tự động
    private List<float> autoTime = new List<float>(); // thời gian tự động kích hoạt hành vi
    private List<int> rate= new List<int>(); // tỷ lệ thực hiện hành động
    private List<float> autoInterval = new List<float>(); // khoảng thời gian giữa các hành ododjng tự động
    private int totalRate = 0; //tổng xác suất, tính khả năng thực hiện

    private float _interval; //khoảng thời gian giữa các hanhf động
    private float _delay; //khoảng thời gian trễ trước khi hành động
    private List<BuildingContain> listTower;
    private List<ArmyTower> listArmyTower;
    private List<int> listTowerID = new List<int>();
    private List<int> listArmyTowerID = new List<int>();


    // certain that gameobj doesn't have 2 overlapping teamID;
#if UNITY_EDITOR
    private void OnValidate()
    {
        AiController[] AI = GetComponents<AiController>();
        foreach(var ai in AI)
        {
            ((AiController)ai).CheckTeam();
        }
    }

    private void CheckTeam()
    {
        AiController[] AI = GetComponents<AiController>();
        showErr = false;
        if(teamId == 0)
        {
            showErr = true;
        }
        foreach(var ai in AI)
        {
            if(ai != this && ((AiController)ai).teamId == teamId)
            {
                showErr = true;
            }
        }

    }
#endif

    private void Awake()
    {
        this.RegisterListener(EventID.CREATE_GAME, _ => CreateGame());
        this.RegisterListener(EventID.START_GAME, _ => StartGame());
        this.RegisterListener(EventID.CLEAR_MAP, _ => ClearMap());
        this.RegisterListener(EventID.END_GAME, _ => ClearMap());
        enabled = false;
    }
    public void StartGame()
    {
        rank = 0;
        listTower = GamePlayController.Instance.playerContain.buildingCtrl.towerList;
        listArmyTower = GamePlayController.Instance.playerContain.buildingCtrl.armyTowerList;
        GetHP();
        InitAIRank();
        this.enabled = true;
    }

    public void CreateGame()
    {
        while(GamePlayController.Instance.playerDatas.Count < teamId + 1)
        {
            GamePlayController.Instance.playerDatas.Add(null);
        }
        GamePlayController.Instance.playerDatas[teamId] = playerData;
    }

    private void InitAIRank()
    {
        int _rate = hardRate[rank];
        HardRateData _rank = AIData.Instance.GetRankAI(_rate);

        intervalMax = _rank.interavalMax;
        intervalMin = _rank.interavalMin;
        actionCount = _rank.actionCount;

        listArmyTowerID.Clear();
        listTowerID.Clear();
        for(int i=0;i<listTower.Count;i++)
        {
            listTowerID.Add(i);
        }
        for(int i=0;i< listArmyTower.Count; i++)
        {
            listArmyTowerID.Add(i);
        }
        _interval = 0;
        listTrigger.Clear();
        listAuto.Clear();
        autoInterval.Clear();
        totalRate = 0;
        foreach(var item in _rank.configList)
        {
            if (item.enable)
            {
                if (item.auto)
                {
                    listAuto.Add(item.name);
                    autoTime.Add(item.coolDown);
                    autoInterval.Add(item.coolDown);
                }
                else
                {
                    listTrigger.Add(item.name);
                    rate.Add(item.rate);
                    totalRate += item.rate;
                }
            }
        }
    }

    private void GetHP()
    {
        
        int x = 0;
        bool isLive = false;
        foreach(var item in listTower)
        {
            if(item.teamId == teamId)
            {
                x += item.Hp;
                isLive = true;
            }
        }
        playerData.Hp = x;
        playerData.isLive = isLive;
    }
    private void Update()
    {
        if (_delay <= 0)
        {
            if (_interval <= 0)
            {
                _interval = UnityEngine.Random.Range(intervalMin, intervalMax);
                int x;
                for(int _trigger = 0; _trigger < actionCount; _trigger++)
                {
                    x = UnityEngine.Random.Range(0, totalRate);
                    for(int i=0;i<rate.Count;i++)
                    {
                        x-=rate[i];
                        if(x < 0)
                        {
                            ChangeState(listTrigger[i]);
                            break;
                        } 
                    }
                }
            }
            else
            {
                _interval-=Time.deltaTime;
            }
            for(int _auto = 0; _auto < autoTime.Count; _auto++)
            {
                if(autoTime[_auto] <= 0)
                {
                    autoTime[_auto] = autoInterval[_auto];
                    ChangeState(listAuto[_auto]);
                }
                else
                {
                    autoTime[_auto]-=Time.deltaTime;
                }
            }
        }
        else
        {
            _delay -=Time.deltaTime;
        }
        CheckHp();
    }

    private void CheckHp()
    {
        GetHP();
        int pos = Mathf.FloorToInt(playerData.Hp / GamePlayController.Instance.total / 4.0f);
        if (pos != rank)
        {
            rank= pos;
            InitAIRank();
        }
    }

    public void ChangeState(AI_Behaviour _behaviour)
    {
        listArmyTowerID.Shuffle();
        listTowerID.Shuffle();
        switch (_behaviour)
        {
            case AI_Behaviour.Idle:
                break;
            case AI_Behaviour.Attack_1:
                foreach(var item in listArmyTowerID)
                {
                    if (listArmyTower[item].teamId == this.teamId)
                    {
                        if (listArmyTower[item].road.Count < listArmyTower[item].level + 1)
                        {
                            listArmyTower[item].listCanGo.Shuffle();
                            foreach(var _road in listArmyTower[item].listCanGo)
                            {
                                if (listTower[_road].teamId != teamId)
                                {
                                    if (!listArmyTower[item].gate.Contains(_road))
                                    {
                                        GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(listArmyTower[item], listTower[_road]);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            case AI_Behaviour.Attack_2:
                // find enemy tower
                foreach(var item in listTower)
                {
                    if(item.teamId!=this.teamId && item is AttackTower atkTow)
                    {
                        // find nearest armytower
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var _army in listArmyTower)
                        {
                            if(_army.teamId == this.teamId && _army.listCanGo.Contains(item.id) && _army.gate.Count < _army.level + 1)
                            {
                                float x = _army.transform.position.DistanceSqrt(item.transform.position);
                                if (x < near)
                                {
                                    near = x;
                                    army = _army;
                                }
                            }
                        }
                        if(army!= null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, item);
                        }
                        return;
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Attack_3:
                //find anemy tower
                foreach(var item in listTower)
                {
                    if(item.teamId != this.teamId && item is AttackTower atkTower)
                    {
                        // find farest 
                        float far = 0;
                        ArmyTower army = null;
                        foreach(var _army in listArmyTower)
                        {
                            if(_army.teamId == this.teamId && _army.listCanGo.Contains(item.id) && _army.gate.Count < _army.level + 1)
                            {
                                float x = _army.transform.position.DistanceSqrt(item.transform.position);
                                if (x > far)
                                {
                                    far = x;
                                    army = _army;
                                }
                            }
                        }
                        if(army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, item);
                        }
                        return;
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Attack_4:
                // sort the enemy' house based on hp inscreasing
                listTowerID = listTowerID
                    .OrderBy(id => listTower.Find(tower => tower.id == id)?.Hp ?? int.MaxValue)
                    .ToList();
                foreach (var item in listTowerID)
                {
                    if (listTower[item].teamId != this.teamId && !(listTower[item] is GoldPack))
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var _army in listArmyTower)
                        {
                            if(_army.teamId == this.teamId && _army.listCanGo.Contains(listTower[item].id) && !_army.gate.Contains(listTower[item].id) && _army.gate.Count < _army.level + 1)
                            {
                                float x = _army.transform.position.DistanceSqrt(listTower[item].transform.position);
                                if (x < near)
                                {
                                    near = x;
                                    army = _army;
                                }
                            }
                        }
                        if(army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, listTower[item]);
                        }
                        return;
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Attack_5:
                // sort the enemy's house based on priority
                listTowerID = listTowerID
    .OrderBy(id => listTower.Find(tower => tower.id == id)?.priority ?? int.MaxValue)
    .ToList();
                foreach (var item in listTowerID)
                {
                    if (listTower[item].teamId != this.teamId && !(listTower[item] is GoldPack))
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var _army in listArmyTower)
                        {
                            if(_army.teamId == this.teamId && _army.listCanGo.Contains(listTower[item].id) && !_army.gate.Contains(listTower[item].id) && _army.gate.Count < _army.level + 1)
                            {
                                float x = _army.transform.position.DistanceSqrt(listTower[item].transform.position);
                                if (x < near)
                                {
                                    near= x;
                                    army= _army;
                                }
                            }
                        }
                        if(army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, listTower[item]);
                            return;
                        }
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Attack_6:
                listTowerID = listTowerID
    .OrderBy(id => listTower.Find(tower => tower.id == id)?.priority ?? int.MaxValue)
    .ToList();
                foreach (var item in listTowerID)
                {
                    if (listTower[item].teamId != this.teamId && !(listTower[item] is GoldPack))
                    {
                        float far = 0;
                        ArmyTower army = null;
                        foreach (var _army in listArmyTower)
                        {
                            if (_army.teamId == this.teamId && _army.listCanGo.Contains(listTower[item].id) && !_army.gate.Contains(listTower[item].id) && _army.gate.Count < _army.level + 1)
                            {
                                float x = _army.transform.position.DistanceSqrt(listTower[item].transform.position);
                                if (x > far)
                                {
                                    far = x;
                                    army = _army;
                                }
                            }
                        }
                        if (army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, listTower[item]);
                            return;
                        }
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Collective:
                foreach(var to in listTower)
                {
                    if(to.Hp>0 && to is GoldPack)
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var from in listArmyTower)
                        {
                            if(from.teamId == this.teamId && from.Hp < 65) // because max hp =65;
                            {
                                if (from.listCanGo.Contains(to.id) && !from.gate.Contains(to.id) && from.gate.Count < from.level+1) 
                                {
                                    float x = from.transform.position.DistanceSqrt(to.transform.position);
                                    if (x<near)
                                    {
                                        near = x;
                                        army = from;
                                    }
                                }
                            }
                        }
                        if (army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, to);
                            return;
                        }
                    }
                }
                ChangeState(AI_Behaviour.Attack_1);
                break;
            case AI_Behaviour.Defensive_1:
                foreach(var to in listTower)
                {
                    if(to.teamId == this.teamId && to.Hp == 65)
                    {
                        foreach(var _fromID in listArmyTowerID)
                        {
                            if(_fromID != to.id && listArmyTower[_fromID].teamId == this.teamId &&listArmyTower[_fromID].listCanGo.Contains(to.id) && listArmyTower[_fromID].gate.Count < listArmyTower[_fromID].level + 1)
                            {
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(listArmyTower[_fromID], to);
                                return;
                            }
                        }
                    }
                }
                break;
            case AI_Behaviour.Defensive_2:
                foreach(var item in listArmyTowerID)
                {
                    if (listArmyTower[item].teamId == this.teamId && listArmyTower[item].gate.Count < listArmyTower[item].level + 1)
                    {
                        foreach(var _tow in listTowerID)
                        {
                            if(item != _tow && listTower[_tow].teamId == this.teamId && listArmyTower[item].listCanGo.Contains(_tow) && listTower[_tow].Hp < listArmyTower[item].Hp)
                            {
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(listArmyTower[item], listTower[_tow]);
                                return;
                            }
                        }
                    }
                }
                ChangeState(AI_Behaviour.Defensive_1);
                break;
            case AI_Behaviour.CounterRoad_1:
                foreach(var item in listTower)
                {
                    if(item.teamId == this.teamId && item is AttackTower && item.Hp==65)
                    {
                        foreach(var _army in listArmyTower)
                        {
                            if (_army.gate.Contains(item.id))
                            {
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(_army, item);
                            }
                        }
                    }
                }
                break;
            case AI_Behaviour.CounterRoad_2:
                foreach(var to in listTower)
                {
                    if(to.teamId == this.teamId && to.Hp < 10)
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var from in listArmyTower)
                        {
                            if(from.teamId == this.teamId && from.Hp > 20)
                            {
                                if(from.listCanGo.Contains(to.id) && !from.gate.Contains(to.id))
                                {
                                    float x = from.transform.position.DistanceSqrt(to.transform.position);
                                    if (x < near)
                                    {
                                        near = x;
                                        army= from; 
                                    }
                                }
                            }
                        }
                        if(army != null)
                        {
                            if(army.gate.Count == army.level + 1)
                            {
                                int x = UnityEngine.Random.Range(0, army.level + 1);
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(army, listTower[army.gate[x]]);
                            }
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, to);
                            return;
                        }
                        
                    }
                }
                ChangeState(AI_Behaviour.Defensive_1);
                break;
            case AI_Behaviour.CounterRoad_3:
                foreach(var to in listTower)
                {
                    if(to.teamId == this.teamId && to.Hp < 20)
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach (var from in listArmyTower)
                        {
                            if (from.teamId == this.teamId && from.Hp > 30)
                            {
                                if (from.listCanGo.Contains(to.id) && !from.gate.Contains(to.id))
                                {
                                    float x = from.transform.position.DistanceSqrt(to.transform.position);
                                    if (x < near)
                                    {
                                        near = x;
                                        army = from;
                                    }
                                }
                            }
                        }
                        if(army != null)
                        {
                            if (army.gate.Count == army.level + 1)
                            {
                                int x = UnityEngine.Random.Range(0, army.level + 1);
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(army, listTower[army.gate[x]]);
                            }
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(army, to);
                            return;
                        }
                    }

                }
                ChangeState(AI_Behaviour.Defensive_1);
                break;
            case AI_Behaviour.CounterRoad_4:
                foreach(var to in listTower)
                {
                    if(to.teamId == this.teamId && to.Hp==65 && to is ArmyTower armyTo && armyTo.gate.Count < armyTo.level + 1)
                    {
                        float near = 10000;
                        ArmyTower army = null;
                        foreach(var _army in listArmyTower)
                        {
                            if(_army.teamId == this.teamId && _army.gate.Contains(to.id))
                            {
                                float x = _army.transform.position.DistanceSqrt(to.transform.position);
                                if (x<near)
                                {
                                    near = x;
                                    army = _army;
                                }
                            }
                        }
                        if(army != null)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(army, to);
                        }
                        break;
                    }
                }
                ChangeState(AI_Behaviour.Defensive_1);
                break;
        }
    }

    public void ClearMap()
    {
        this.enabled = false;
    }
    private void OnDestroy()
    {
        this.RemoveListener(EventID.CREATE_GAME, _ => CreateGame());
        this.RemoveListener(EventID.START_GAME, _ => StartGame());
        this.RemoveListener(EventID.CLEAR_MAP, _ => ClearMap());
        this.RemoveListener(EventID.END_GAME, _ => ClearMap());
    }
}

[System.Serializable]
public class AI_Config
{
    [ReadOnly]
    public AI_Behaviour name;
    public bool enable;
    public bool auto;

    [ShowIf("auto")]
    [MinValue(0.5f)]
    public float coolDown;
    [ShowIf("@auto == false")]
    [MinValue(0)]
    public int rate;
}

public enum AI_Behaviour
{
    Idle,
    Attack_1,
    Attack_2,
    Attack_3,
    Attack_4,
    Attack_5,
    Attack_6,
    Collective,
    Defensive_1,
    Defensive_2,
    CounterRoad_1,
    CounterRoad_2,
    CounterRoad_3,
    CounterRoad_4,
}

