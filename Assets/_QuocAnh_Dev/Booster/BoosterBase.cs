using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterBase : MonoBehaviour
{
    public int id;
    public BoosterType boosterName;
    public float cooldown = 60f;
    [HideInInspector] public float timer;
    public float duration;

    public virtual void CountDown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public bool canUse => timer <= 0;
    public void Active()
    {
        OnActive();
    }
    public abstract void OnActive();
}
