using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerGold : MonoBehaviour
{
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
