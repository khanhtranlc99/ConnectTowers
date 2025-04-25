using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAutoComponents : MonoBehaviour
{
    private void Reset()
    {
        this.LoadComponent();   
    }
    public virtual void LoadComponent()
    {
        //FOR override
    }
}
