using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EventDispatcher;

public class ArmyTower : BuildingContain
{
    private int unitId = -1;
    private int unitLv = -1;
    //manage the line

    public List<int> listCanGo = new List<int>();
    public List<int> gate = new List<int>();

    public List<LineRenderer> road = new List<LineRenderer>();
    //manage spawnUnit
    [HideInInspector]
    public List<float> timeNow = new List<float>();
    public UnitType unitType;
    public float[] timeSpawnLevel;
    public float[] timeSpawnRoad;
    [HideInInspector] public float spawnBuff = 1f;
    [HideInInspector] public bool isSpeedBuff = false;
    private Stack<CharacterBase> myStack;
    public TextMeshPro roadDot;
    private int gateCnt;

    public CharacterBase unitPrefab;
    [SerializeField] private UnitBase unitBase;

    public override void OnEnable()
    {
        base.OnEnable();
        roadDot = transform.GetChild(1).GetComponent<TextMeshPro>();
        this.RegisterListener(EventID.START_GAME, delegate { CheckListCanGo(); });
        this.RegisterListener(EventID.RESET_MAP, delegate { CheckListCanGo(); });
        this.RegisterListener(EventID.CLEAR_MAP, delegate { ResetLevel(); });
        this.RegisterListener(EventID.END_GAME, delegate { ResetLevel(); });
        this.roadDot.gameObject.SetActive(true);
    }
    public void CreatePath()
    {
        gateCnt = gate.Count;
        //TimeAutoIncs = TimeAutonIncsFix;
        SetRoad();
    }
    public override void InitTower()
    {
        base.InitTower();
        if (this.teamId != -1)
        {
            switch (unitType)
            {
                case UnitType.Solider:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitSoldierId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitSoldierLv;
                    break;
                case UnitType.Tank:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitTankId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitTankLv;
                    break;
                case UnitType.Mage:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitMageId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitMageLv;
                    break;
            }
            unitBase = UnitData.Instance.GetUnit(unitId);
            if (GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] == null)
            {

                GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] = new Stack<CharacterBase>();
            }
            this.myStack = GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType];
        }
        this.SetRoad();
    }
    public override void UpdateTeam() // cập nhật lại khi teamId thay đổi
    {
        if (GamePlayController.Instance.isPlay)
        {
            base.UpdateTeam();
            switch (unitType)
            {
                case UnitType.Solider:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitSoldierId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitSoldierLv;
                    break;
                case UnitType.Tank:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitTankId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitTankLv;
                    break;
                case UnitType.Mage:
                    unitId = GamePlayController.Instance.playerDatas[teamId].unitMageId;
                    unitLv = GamePlayController.Instance.playerDatas[teamId].unitMageLv;
                    break;
            }
            unitBase = UnitData.Instance.GetUnit(unitId);
            if (GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] == null)
            {
                GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] = new Stack<CharacterBase>();
            }
            this.myStack = GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType];
            for (int i = this.gate.Count - 1; i >= 0; i--)
            {
                GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(this, GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.gate[i]]);
            }
            timeNow.Clear();
            GetUnitSkill();
            CreatePath();

        }
    }



    private void GetUnitSkill()
    {
        throw new NotImplementedException();
    }

    private void SetRoad()
    {
        string s = "";
        for (int i = 0; i < this.level + 1; i++)
        {
            if (i < this.gateCnt)
            {
                s += "<sprite name=Dot_0> ";
            }
            else
            {
                s += "<sprite name=Dot_1> ";
            }
        }
        this.roadDot.text = s;
    }
    public override void Update()
    {
        if (GamePlayController.Instance.isPlay)
        {

            for (int i = 0; i < this.gateCnt; i++)
            {
                if (timeNow[i] < 0)
                {
                    timeNow[i] = timeSpawnLevel[level] * timeSpawnRoad[gateCnt - 1] * spawnBuff;
                    SpawnArmy(this.gate[i]);
                }
                else
                {
                    timeNow[i] -= Time.deltaTime;
                }
                base.Update();
            }
        }

    }
    private void SpawnArmy(int to, bool canSpecial = true)
    {
        CharacterBase _unit = null;
        _unit = SimplePool2.Spawn(unitBase.GetPrefab, this.transform.position, Quaternion.identity).GetComponent<CharacterBase>();
        _unit.id = (int)this.buildingType;
        _unit.teamId = this.teamId;
        _unit.ResetData();
        _unit.Hp = unitBase.hp;
        _unit.dame = unitBase.dmg;
        _unit.speed = isSpeedBuff? unitBase.speed * 1.5f : unitBase.speed;
        _unit.heal = unitBase.heal;
        _unit.from = this.id;
        _unit.to = to;
        _unit.transform.position = this.transform.position;
        _unit.transform.LookAt(GamePlayController.Instance.playerContain.buildingCtrl.towerList[to].transform.position);
        GamePlayController.Instance.playerContain.unitCtrl.allyList.Add(_unit);
        _unit.gameObject.layer = ConfigData.Instance.unitLayer[teamId];

        // set skill
        if (_unit.transform.GetChild(0).GetChild(0).TryGetComponent(out MeshRenderer mesh)) // doan nay chua fix neu la model chuan
        {
            mesh.materials[0].mainTexture = ConfigData.Instance.texture[teamId + 1];
        }
        if (_unit.TryGetComponent(out Collider col))
        {
            if (!GamePlayController.Instance.playerContain.unitCtrl.componentDict.ContainsKey(col))
            {
                GamePlayController.Instance.playerContain.unitCtrl.componentDict.Add(col, _unit);
                return;
            }
        }
    }
    public void CheckListCanGo()
    {
        this.listCanGo.Clear();
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item != this && !(item is GoldPack && item.Hp <= 0))
            {
                RaycastHit[] hits = new RaycastHit[5];
                if (Physics.RaycastNonAlloc(this.transform.position,
                    item.transform.position - this.transform.position,
                    hits, Vector3.Distance(this.transform.position, item.transform.position),
                    ConfigData.Instance.obstacle) == 1)
                {
                    this.listCanGo.Add(item.id);
                }
            }
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        this.roadDot.gameObject.SetActive(false);
    }
    public void ResetLevel()
    {
        foreach (var item in road)
        {
            Destroy(item.gameObject);
        }
        this.road.Clear();
        this.gate.Clear();
        this.timeNow.Clear();
        gateCnt = 0;
    }
    private void OnDestroy()
    {
        this.RemoveListener(EventID.START_GAME, delegate { CheckListCanGo(); });
        this.RemoveListener(EventID.RESET_MAP, delegate { CheckListCanGo(); });
        this.RemoveListener(EventID.CLEAR_MAP, delegate { ResetLevel(); });
        this.RemoveListener(EventID.END_GAME, delegate { ResetLevel(); });
    }
    
}
