using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class GoldPack : BuildingContain
{
    public float currentScaleGold, maxScaleGold, maxHp;
    [SerializeField] private float minScaleGold = 1, maxRadius;
    private float currentRadius = 0;
    [SerializeField] private GameObject avatarGold;
    private SphereCollider colider;

    public void SetupGold()
    {
        if(maxScaleGold <= minScaleGold)
        {
            avatarGold.transform.localScale = new Vector3(minScaleGold, minScaleGold,minScaleGold);
        }
        else
        {
            avatarGold.transform.localScale = new Vector3(maxScaleGold, maxScaleGold, maxScaleGold);
        }
        if(this.transform.TryGetComponent(out colider))
        {
            if (currentRadius == 0)
            {
                currentRadius = colider.radius;
            }
            maxRadius = currentRadius*maxScaleGold;
        }
        this.colider.radius = maxRadius;
    }
    public override void UpdateTower()
    {
        if(this.Hp < lvPoint[0])
        {
            if (this.level != 0)
            {
                this.level = 0;
            }
        }
        else if(this.Hp<lvPoint[1])
        {
            if(this.level != 1)
            {
                this.level = 1;
            }
        }
        else if (this.Hp < lvPoint[2])
        {
            if(this.level != 2)
            {
                this.level = 2;
            }
        }
        if (this.Hp == 0)
        {
            this.Hp = -1;
            this.PostEvent(EventID.RESET_MAP);
            foreach(var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                if(item is ArmyTower army)
                {
                    Debug.LogError(army.id);
                    for (int i = army.gate.Count - 1; i >= 0; i--)
                    {
                        if (army.gate[i] == this.id)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(army, this);
                        }
                    }
                }
            }
            this.gameObject.SetActive(false);
        }
        ChangeScaleGold();
        if (colider)
        {
            this.colider.radius = currentRadius * currentScaleGold;
        }
    }

    private void ChangeScaleGold()
    {
        currentScaleGold = (this.Hp * maxScaleGold / maxHp);
        if (currentScaleGold <= minScaleGold)
        {
            currentScaleGold = minScaleGold;
        }
        this.avatarGold.transform.localScale = new Vector3(currentScaleGold, currentScaleGold, currentScaleGold);

    }
}
