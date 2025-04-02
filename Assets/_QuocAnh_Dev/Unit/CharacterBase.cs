
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterBase : MonoBehaviour
{

    // DataPerLevel here
    public int id;
    public int teamId;
    public UnitType unitType;
    public int level;


    
    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            if(!this.isDead && _hp <= 0)
            {
                Die();
            }
        }
    }
    [Header("---------Data-----------")]
    [SerializeField] private int _hp=-1;
    public int dame;
    public float speed;
    public int heal;
    public int from;
    public int to;
    public int roadGo;

    public bool isDead;

    protected virtual void Awake()
    {
        
    }
    protected virtual void Init()
    {
        LoadScriptAbleObj();
        LoadHandleData();
    }
    public void Update()
    {
        UnitMove();
    }

    private void LoadHandleData()
    {
        // handle scriptableobj
    }

    private void LoadScriptAbleObj()
    {
        // handleData
    }
    protected virtual void InitData()
    {
        // HandleDataUnit
    }

    public void TakeDame(int dame)
    {
        int takeDame = Mathf.Max(0, this.Hp - dame);
        this.Hp -= takeDame;
    }
    public void UpgradeLevel()
    {
        this.level++;
        LoadHandleData();
    }

    public void UnitMove()
    {
        if (!this.isDead)
        {
            if (this.transform.position.CustomOutNormalize(GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].transform.position, out Vector3 direction))
            {
                this.transform.position += this.speed * Time.deltaTime * direction;
            }
            else
            {
                if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].teamId == this.teamId)
                {
                    if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].isMax && this.roadGo < 6)
                    {
                        if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to] is ArmyTower tow)
                        {
                            if(tow.gate.Count > 0)
                            {
                                this.from = tow.id;
                                this.to = tow.gate[Random.Range(0, tow.gate.Count)];
                                this.roadGo++;
                                this.transform.LookAt((GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].transform.position));
                            }
                        }
                    }
                    else
                    {
                        GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp += this.heal;
                    }
                }
                else
                {
                    if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to] is GoldPack pick)
                    {
                        if(pick.Hp > 0)
                        {
                            this.heal++;
                            pick.Hp--;
                            GameObject _gold = Instantiate(GamePlayController.Instance.prefabGold, this.transform);
                            if(this.id == 0 || this.id == 2) // if is soldier
                            {
                                _gold.transform.localPosition = new Vector3(0, 0.4f, -0.2f);
                            }
                            else // if is Mammouth
                            {
                                _gold.transform.localPosition = new Vector3(0, 0.55f, 0f);
                            }
                        }
                        this.to = this.from;
                        this.from = pick.id;
                        this.roadGo = 6;
                        this.transform.LookAt(GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].transform.position);
                        return;
                    }
                    else if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp <= 0)
                    {
                        GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].teamId = this.teamId;
                        GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp += this.heal;
                    }
                    else
                    {
                        GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp -= this.dame;
                        if (GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp < 0)
                        {
                            GamePlayController.Instance.playerContain.buildingCtrl.towerList[this.to].Hp = 0;
                        }
                    }
                }
                if (!this.isDead)
                {
                    this.isDead = true;
                    if(GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, this.id] == null)
                    {
                        Debug.LogError("Null UnitGrid");
                    }
                    GamePlayController.Instance.playerContain.unitCtrl.unitGrid[this.teamId, this.id].Push(this);
                    GamePlayController.Instance.playerContain.unitCtrl.allyList.Remove(this);
                    
                }
                SimplePool2.Despawn(this.gameObject);

            }
        }
        //else
        //{
        //    SimplePool2.Despawn(this.gameObject);
        //}
    }

    public void ResetData()
    {
        this.Hp = 1;
        this.isDead = false;
        this.roadGo = 0;
        this.gameObject.SetActive(true);
    }

    //Handle check trigger between unit;
    private void OnTriggerEnter(Collider other)
    {
        if(GamePlayController.Instance.playerContain.unitCtrl.componentDict.TryGetValue(other, out CharacterBase _unit))
        {
            if(!this.isDead && !_unit.isDead && _unit.from == this.to && _unit.to ==this.from && this.teamId != _unit.teamId)
            {
                this.Hp-=_unit.dame;
                _unit.Hp -= this.dame;
            }
        }
    }
    public void Die()
    {
        this.isDead = true;
        // handle anime
        DieAnim();
    }

    public virtual void DieAnim()
    {
        SimplePool2.Despawn(this.gameObject);
    }
}
