using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : AttackTower
{
    [SerializeField] private Transform bullet;

    public Transform target;
    public float bulletSpeed;
    private BuildingContain towTarget;
    public Transform[] rotates;
    public Transform[] stoneThrow;

    public override void DoShotTarget()
    {
        float near = this.range[this.level] * this.range[this.level];
        bool found = false;
        int hp = 0;
        foreach(var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if(this.teamId != item.teamId && item.Hp>=0 && !(item is GoldPack))
            {
                float x = item.transform.position.DistanceSqrt(this.transform.position);
                if (x < near)
                {
                    if (hp < item.Hp)
                    {
                        hp = item.Hp;
                        towTarget = item;
                        found = true;
                    }
                }
            }
        }
        if (found)
        {
            this.bullet.transform.position = this.transform.position + new Vector3(0,1,0);
            StartCoroutine(DoShot(towTarget));
            this.timeNow = speed;
        }
    }

    private IEnumerator DoShot(BuildingContain towTarget)
    {
        float t = 0;
        float _stepScale;
        float _progress;
        this.bullet.position = this.transform.position + new Vector3(0,1,0);
        this.bullet.gameObject.SetActive(true);
        Vector3 _startPosition;
        _startPosition = this.bullet.position;
        Vector3 _target = towTarget.transform.position;
        float distance = Vector3.Distance(_startPosition, _target);
        _stepScale = this.bulletSpeed/ distance;
        _progress = 0;
        foreach(var item in this.rotates)
        {
            item.LookAt(_target + Vector3.up);
            item.Rotate(0, 90f, 0);
        }
        foreach(var item in this.stoneThrow)
        {
            item.DOLocalRotate(new Vector3(0, 0, 120), 0.5f).OnComplete(() => item.DOLocalRotate(new Vector3(0, 0, 0), 0.8f));
        }
        // sound effect
        while (_progress <= 1)
        {
            t += Time.deltaTime;
            _progress = Mathf.Min(_progress + Time.deltaTime * _stepScale, 1.05f);
            float parabola = 1f - 4f * (_progress - 0.5f) * (_progress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(_startPosition, _target, _progress);
            nextPos.y += parabola * 3;
            this.bullet.LookAt(nextPos, this.transform.forward);
            this.bullet.position = nextPos;
            if(this.bullet.position.y == 0)
            {
                yield break;
            }
            yield return null;
        }
        if(towTarget.teamId != this.teamId)
        {
            towTarget.Hp -= this.dmg;
            if(towTarget.Hp < 0)
            {
                towTarget.Hp = 0;
            }
        }
        this.bullet.gameObject.SetActive(false);
    }
    private new void OnDisable()
    {
        StopAllCoroutines();
    }
}
