using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmyTower : BuildingContain
{
    //manage the line
    public List<int> gate = new List<int>(); 

    public List<LineRenderer> road = new List<LineRenderer>();
    //manage spawnUnit
    [HideInInspector]
    public List<float> timeNow = new List<float>();
    public UnitType unitType;
    public float[] timeSpawnLevel;
    public float[] timeSpawnRoad;
    [HideInInspector]
    public float TimeAutoIncs;
    //handle tower
    private float TimeAutonIncsFix;
    private Stack<CharacterBase> myStack;
    public TextMeshPro roadDot;
    private int gateCnt;

    public CharacterBase unitPrefab;
    private UnitBase unitBase;

    public override void Awake()
    {
        base.Awake();
        roadDot = transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    public void CreatePath()
    {
        gateCnt = gate.Count;
        TimeAutoIncs = TimeAutonIncsFix;
        SetRoad();
    }
    public override void InitTower()
    {
        base.InitTower();
        TimeAutonIncsFix = ConfigData.Instance.TimeAutoIncs;
        if(this.teamId != -1)
        {
            unitBase = UnitData.Instance.GetUnit(1); // chua xet cac unit khac
            if (GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] == null)
            {
                GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] = new Stack<CharacterBase>();
            }
            this.myStack = GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType];
        }
        this.SetRoad();
    }
    public override void UpdateTeam()
    {
        if (GamePlayController.Instance.isPlay)
        {
            base.UpdateTeam();
            unitBase = UnitData.Instance.GetUnit(1);
            if (GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] == null)
            {
                GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType] = new Stack<CharacterBase>();
            }
            this.myStack = GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, (int)this.unitType];
            for(int i = this.gate.Count - 1; i >= 0; i--)
            {
                GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(this, GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.gate[i]]);
            }
            timeNow.Clear();
            GetUnitSkill();
            CreatePath();

        }
    }

    public override void CallChangeLevelTower()
    {
        if (lvTowerList.Count > 0)
        {
            switch (level)
            {
                case 0:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    lvTowerList[1].transform.DOLocalMoveY(-0.03f, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        lvTowerList[0].transform.DOLocalMoveY(-0.0365f, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    });
                    break;
                case 1:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    if (lvTowerList[1].transform.localPosition.y == 0)
                    {
                        lvTowerList[1].transform.DOLocalMoveY(-0.03f, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                        {
                            lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                        });
                    }
                    else
                    {
                        lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    }
                    break;
                default:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        lvTowerList[1].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    });
                    break;
            }
        }
    }

    private void GetUnitSkill()
    {
        throw new NotImplementedException();
    }

    private void SetRoad()
    {
        string s = "";
        for(int i = 0; i < this.level + 1; i++)
        {
            if (i < this.gateCnt)
            {
                s += "<sprite name=BlackDot> ";
            }
            else
            {
                s += "<sprite name=WhiteDot> ";
            }
        }
        this.roadDot.text = s;
    }
    private void Update()
    {
        for(int i = 0; i < this.gateCnt; i++)
        {
            if (timeNow[i] < 0)
            {
                timeNow[i] = timeSpawnLevel[level] * timeSpawnRoad[gateCnt - 1];
                SpawnArmy(this.gate[i]);
            }
            else
            {
                timeNow[i] -= Time.deltaTime;
            }
            if(gateCnt == 0 && teamId!= -1)
            {
                TimeAutoIncs -= Time.deltaTime;
                if(TimeAutoIncs < 0)
                {
                    this.Hp++;
                    TimeAutoIncs = TimeAutonIncsFix;
                }
            }
        }
        // để tạm ở đây thôi chưa fix
        
    }
    private void SpawnArmy(int to, bool canSpecial = true)
    {
        CharacterBase _unit = null;
        _unit = SimplePool2.Spawn(unitPrefab, this.transform.position, Quaternion.identity).GetComponent<CharacterBase>();
        _unit.id = (int)this.buildingType;
        _unit.teamId = this.teamId;
        _unit.ResetData();
        _unit.from = this.id;
        _unit.to = to;
        _unit.transform.position = this.transform.position;
        _unit.transform.LookAt(GamePlayController.Instance.playerContain.buildingCtrl.towerList[to].transform.position);
        GamePlayController.Instance.playerContain.unitCtrl.allyList.Add(_unit);
        _unit.gameObject.layer = ConfigData.Instance.unitLayer[teamId];
        // set skill
        if(_unit.TryGetComponent(out MeshRenderer mesh)) // doan nay chua fix neu la model chuan
        {
            mesh.materials[0].mainTexture = ConfigData.Instance.texture[teamId+1];
        }
        if (_unit.TryGetComponent(out Collider col))
        {
            if (GamePlayController.Instance.playerContain.unitCtrl.componentDict.ContainsKey(col))
            {
                return;
            }

            GamePlayController.Instance.playerContain.unitCtrl.componentDict.Add(col, _unit);
        }
        /*
        if (myStack.Count > 0)
        {
            CharacterBase _unit = myStack.Pop();
            //ResetData(_unit);
            _unit.transform.position = transform.position;
            _unit.transform.LookAt(GamePlayController.Instance.playerContain.buildingCtrl.towerList[to].transform.position);
            _unit.from = this.id;
            _unit.to = to;
            GamePlayController.Instance.playerContain.unitCtrl.allyList.Add(_unit);
            //TriggerSkillLoop(_unit, canSpecial);
        }
        else
        {
            CharacterBase _go = Instantiate(unitPrefab);
            if (_go.TryGetComponent(out CharacterBase _unit))
            {
                _unit.ResetData();

                _unit.id = (int)unitType;
                _unit.from = this.id;
                _unit.to = to;
                _unit.teamId = this.teamId;
                _unit.transform.position = transform.position;
                _unit.transform.LookAt(GamePlayController.Instance.playerContain.buildingCtrl.towerList[to].transform.position);
                GamePlayController.Instance.playerContain.unitCtrl.allyList.Add(_unit);
                _unit.gameObject.layer = ConfigData.Instance.unitLayer[this.teamId];
                //_unit.skillUnits = skillUnits;
                //TriggerSkillOne(_unit, canSpecial);
                if (_go.transform.GetChild(0).TryGetComponent(out MeshRenderer mesh))
                {
                    mesh.materials[0].mainTexture = ConfigData.Instance.texture[this.teamId + 1];
                }
                if (_unit.TryGetComponent(out Collider col))
                {
                    GamePlayController.Instance.playerContain.unitCtrl.componentDict.Add(col, _unit);
                }
            }
        }
        */        
    }
    public void CheckListCanGo()
    {
        this.listCanGo.Clear();
        foreach(var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if(item != this && !(item is GoldPack && item.Hp <= 0))
            {
                RaycastHit[] hits = new RaycastHit[5];
                if(Physics.RaycastNonAlloc(this.transform.position, item.transform.position-this.transform.position, hits, Vector3.Distance(this.transform.position, item.transform.position), ConfigData.Instance.obstacle) == 1)
                {
                    this.listCanGo.Add(item.id);
                }
            }
        }
    }
    private void OnDisable()
    {
        base.OnDisable();
        this.roadDot.gameObject.SetActive(false);
    }
    public void ResetLevel()
    {
        foreach(var item in road)
        {
            Destroy(item.gameObject);
        }
        this.road.Clear();
        this.gate.Clear();
        this.timeNow.Clear();
        gateCnt = 0;
    }
}
