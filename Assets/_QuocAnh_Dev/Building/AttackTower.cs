using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : BuildingContain
{
    [PropertyOrder(-1)]
    [SerializeField] protected float speed;
    [PropertyOrder(-1)]
    [SerializeField] protected int dmg;
    [PropertyOrder(-1)]
    [SerializeField] protected float[] range;
    [HideInInspector] public float timeNow;
    public GameObject AOE;
    private float x;

    private void OnEnable()
    {
        base.OnEnable();
        CallChangeLevelTower();
    }
    private void Update()
    {
        if(timeNow < 0)
        {
            if (x < 0)
            {
                x = 0.1f;
                DoShotTarget();
            }
            else
            {
                x-= Time.deltaTime;
            }
        }
        else
        {
            timeNow -= Time.deltaTime;
        }
    }

    public virtual void DoShotTarget()
    {
        
    }
    public override void CallChangeLevelTower()
    {
        if (lvTowerList.Count > 0 && level != -1)
        {
            int oldLevel = 0;
            for (int i = 0; i < lvTowerList.Count; i++)
            {
                if (lvTowerList[i].activeSelf)
                {
                    oldLevel = i;
                }
                lvTowerList[i].SetActive(false);
            }
            // handle VFX
            /*
            if (level > oldLevel)
            {
                if (vfxUpTower != null)
                {
                    vfxUpTower.Play();
                }
            }
            else if (level < oldLevel)
            {
                if (vfxDownTower != null)
                {
                    vfxDownTower.Play();
                }
            }
            */
            lvTowerList[level].SetActive(true);
            Vector3 vectorA = this.range[Mathf.Clamp(this.level, 0, range.Length - 1)] * Vector3.one;
            Vector3 vectorC = new Vector3(vectorA.x / this.transform.localScale.x, vectorA.y / this.transform.localScale.y, vectorA.z / this.transform.localScale.z);
            AOE.transform.localScale = vectorC;
        }
    }
}
