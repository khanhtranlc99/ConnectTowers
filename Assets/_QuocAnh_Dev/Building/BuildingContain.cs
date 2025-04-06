using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class BuildingContain : MonoBehaviour
{
    public int id;
    public BuildingType buildingType;
    public int level;
    [SerializeField] private int _teamId = -1;
    [HideInInspector]
    public int teamId
    {
        get =>_teamId;
        set
        {
            _teamId = value;
            if(_teamId != -1)
            {
                foreach(var item in changeMeshList)
                {
                    item.material.mainTexture = ConfigData.Instance.texture[teamId+1];
                }
            }
            else
            {
                foreach(var item in changeMeshList)
                {
                    item.material.mainTexture = ConfigData.Instance.texture[0];
                }
            }
            UpdateTeam();
        }
    }
    [SerializeField] private int _hp = 1;
    [HideInInspector]
    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            if(_hp>= lvPoint[2])
            {
                _hp=lvPoint[2];
                textHp.text = "<size=3>MAX";
                isMax = true;
            }
            else
            {
                isMax = false;
                textHp.text = _hp.ToString();
                textHp.transform.DOScale(1, 0.1f).From(1.2f);
            }
            
            UpdateTower();
        }
    }

    public List<MeshRenderer> changeMeshList = new List<MeshRenderer>();
    public List<GameObject> lvTowerList = new List<GameObject>();
    public float timeChangeLevelTower = 0.2f;



    public bool isMax;
    public int priority;
    public int[] lvPoint;

    public List<int> listCanGo = new List<int>();
    public TMP_Text textHp;
    // White dot and black dot

    public virtual void Awake() 
    {
        this.textHp = this.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public virtual void UpdateTower()
    {
        if(Hp < lvPoint[0])
        {
            if (level != 0)
            {
                level = 0;
                CallChangeLevelTower();
                
            }
        }
        else if (Hp < lvPoint[1])
        {
            if (level != 1)
            {
                level = 1;
                CallChangeLevelTower();
            }
        }
        else if (Hp <= lvPoint[2])
        {
            if (level != 2)
            {
                level = 2;
                CallChangeLevelTower();
            }
        }
    }

    protected virtual void HandleLoadData()
    {
        // LoadData from SO
        Debug.LogError("Finish Handle " + buildingType);
    }

    public virtual void InitTower()
    {
        if(this.Hp == 0)
        {
            this.Hp = 5;
        }
        if (this.Hp < lvPoint[0])
        {
            this.level = 0;
        }
        else if(this.Hp < lvPoint[1])
        {
            this.level = 1;
        }
        else if(this.Hp <= lvPoint[2])
        {
            this.level = 2;
        }
        this.textHp.text = this.Hp.ToString();
        if(this.teamId != -1)
        {
            if(GamePlayController.Instance.playerDatas.Count < this.teamId+1 || GamePlayController.Instance.playerDatas[this.teamId] == null)
            {
                this.teamId = -1;
            }
        }
        if(this.teamId != -1)
        {
            if (changeMeshList.Count > 0)
            {
                foreach(var item in changeMeshList)
                {
                    item.material.mainTexture = ConfigData.Instance.texture[this.teamId+1];
                }
            }
        }
    }

    public virtual void CallChangeLevelTower() { }
    
    public virtual void UpdateTeam()
    {
        //check list mesh to change color
        if(changeMeshList.Count>0 && this.teamId>-1)
        {
            foreach(var item in changeMeshList)
            {
                item.materials[0].mainTexture = ConfigData.Instance.texture[teamId + 1];
            }
        }
    }
    protected void OnDisable()
    {
        this.textHp.gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        this.textHp.gameObject.SetActive(true); 
    }
}
