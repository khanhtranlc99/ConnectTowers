using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : AttackTower
{
    [SerializeField] private LayerMask layer;
    private LayerMask _layer;
    [SerializeField] private Transform bullet;

    private Collider colTarget;
    private BuildingContain towTarget;
    public Transform[] arrow;

    public override void DoShotTarget()
    {
        if(this.teamId != -1)
        {
            this._layer = layer & ~(1 << ConfigData.Instance.unitLayer[this.teamId]);
        }
        else
        {
            this._layer = this.layer;
        }
        Collider[] hitColliders = new Collider[20];
        int numColliders = Physics.OverlapSphereNonAlloc(this.transform.position, range[this.level], hitColliders, this._layer);
        float near = range[this.level] * range[this.level];
        bool found = false;
        if(numColliders > 0)
        {
            for(int i= 0; i < numColliders; i++)
            {
                if (GamePlayController.Instance.playerContain.unitCtrl.componentDict.ContainsKey(hitColliders[i]))
                {
                    if (GamePlayController.Instance.playerContain.unitCtrl.componentDict[hitColliders[i]].isDead)
                    {
                        continue;
                    }
                }
                float x = hitColliders[i].transform.position.DistanceSqrt(this.transform.position);
                if (x < near && this.teamId != GamePlayController.Instance.playerContain.unitCtrl.componentDict[hitColliders[i]].teamId)
                {
                    near = x;
                    this.colTarget = hitColliders[i];
                    found = true;
                }
            }
            if (found)
            {
                this.bullet.transform.position = this.transform.position + new Vector3(0,1,0);
                this.bullet.gameObject.SetActive(true);
                StartCoroutine(DoShot(this.colTarget.transform, colTarget));
                this.timeNow = speed;
            }
        }
        else
        {
            foreach(var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                if(item.teamId != this.teamId && item.Hp>=0 && !(item is GoldPack))
                {
                    float x = item.transform.position.DistanceSqrt(this.transform.position);
                    if (x < near)
                    {
                        near = x;
                        towTarget = item;
                        found = true;
                    }
                }
            }
            if (found)
            {
                this.bullet.transform.position = this.transform.position + new Vector3(0, 1, 0);
                this.bullet.gameObject.SetActive(true);
                StartCoroutine(DoShot(towTarget));
                this.timeNow = speed;
            }
        }
    }

    private IEnumerator DoShot(BuildingContain towTarget)
    {
        // sound effect
        float t = 0;
        Vector3 start = this.bullet.position;
        Vector3 end = towTarget.transform.position + Vector3.up*3f;
        foreach(var item in arrow)
        {
            item.LookAt(end);
        }
        while (t < 1f)
        {
            t+= Time.deltaTime;
            this.bullet.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        towTarget.Hp -= this.dmg;
        if(towTarget.Hp < 0)
        {
            towTarget.Hp = 0;
        }
        this.bullet.gameObject.SetActive(false);
    }

    private IEnumerator DoShot(Transform _transform, Collider colTarget)
    {
        // sound effect;
        float t = 0;
        Vector3 start = this.bullet.position;
        CharacterBase _unit = GamePlayController.Instance.playerContain.unitCtrl.componentDict[colTarget];
        foreach(var item in arrow)
        {
            item.LookAt(_transform.position);
        }
        while(t < 1f)
        {
            if (!_unit.isDead)
            {
                t += Time.deltaTime * 5f;
                this.bullet.position = Vector3.Lerp(start, _transform.position, t);
                yield return null;
            }
            else
            {
                this.bullet.gameObject.SetActive(false);
                yield break;
            }
        }
        GamePlayController.Instance.playerContain.unitCtrl.componentDict[colTarget].Hp -= this.dmg;
        this.bullet.gameObject.SetActive(false);
    }
    private new void OnDisable()
    {
        StopAllCoroutines();
    }
}
