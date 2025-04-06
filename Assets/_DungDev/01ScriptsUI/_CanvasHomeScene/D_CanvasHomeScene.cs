using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CanvasHomeScene : MonoBehaviour
{
    public Transform canvasDynamic;

    public void SetSateThis(bool state)
    {
        this.gameObject.SetActive(state);
    }
    public void SetStateCanvasDynamic(bool state)
    {
        canvasDynamic.gameObject.SetActive(state);
    }
}
